﻿using AmeisenBotX.Core.Data.Enums;
using AmeisenBotX.Core.Data.Objects.Structs;
using AmeisenBotX.Core.Data.Objects.WowObject;
using AmeisenBotX.Core.Movement.Pathfinding.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmeisenBotX.Core.Data
{
    public class ObjectManager : IObjectManager
    {
        private readonly object queryLock = new object();

        private List<ulong> partymemberGuids;
        private List<WowObject> wowObjects;

        public ObjectManager(WowInterface wowInterface)
        {
            WowInterface = wowInterface;

            IsWorldLoaded = true;
            wowObjects = new List<WowObject>();
            PartymemberGuids = new List<ulong>();
        }

        public event ObjectUpdateComplete OnObjectUpdateComplete;

        public CameraInfo Camera { get; private set; }

        public string GameState { get; private set; }

        public bool IsWorldLoaded { get; private set; }

        public WowUnit LastTarget { get; private set; }

        public ulong LastTargetGuid { get; private set; }

        public MapId MapId { get; private set; }

        public ulong PartyleaderGuid { get; private set; }

        public List<ulong> PartymemberGuids
        {
            get { lock (queryLock) { return partymemberGuids.ToList(); } }
            private set { lock (queryLock) { partymemberGuids = value; } }
        }

        public List<WowUnit> Partymembers { get; set; }

        public WowUnit Pet { get; private set; }

        public ulong PetGuid { get; private set; }

        public WowPlayer Player { get; private set; }

        public IntPtr PlayerBase { get; private set; }

        public ulong PlayerGuid { get; private set; }

        public WowUnit Target { get; private set; }

        public ulong TargetGuid { get; private set; }

        public List<WowObject> WowObjects
        {
            get { lock (queryLock) { return wowObjects; } }
            set { lock (queryLock) { wowObjects = value; } }
        }

        public int ZoneId { get; private set; }

        public string ZoneName { get; private set; }

        public string ZoneSubName { get; private set; }

        private IntPtr CurrentObjectManager { get; set; }

        private WowInterface WowInterface { get; }

        public WowUnit Partyleader { get; private set; }

        public WowGameobject GetClosestWowGameobjectByDisplayId(List<int> displayIds)
        {
            return WowObjects.OfType<WowGameobject>()
                .Where(e => displayIds.Contains(e.DisplayId))
                .OrderBy(e => e.Position.GetDistance(WowInterface.ObjectManager.Player.Position))
                .FirstOrDefault();
        }

        public WowUnit GetClosestWowUnitByDisplayId(List<int> displayIds, bool onlyQuestgiver = true)
        {
            return WowObjects.OfType<WowUnit>()
                .Where(e => (e.IsQuestgiver || !onlyQuestgiver) && displayIds.Contains(e.DisplayId))
                .OrderBy(e => e.Position.GetDistance(WowInterface.ObjectManager.Player.Position))
                .FirstOrDefault();
        }

        public List<WowUnit> GetEnemiesTargetingPartymembers(Vector3 position, double distance)
        {
            lock (queryLock)
            {
                return wowObjects.OfType<WowUnit>()
                  .Where(e => e != null
                    && e.Guid != PlayerGuid
                    && !e.IsDead
                    && !e.IsNotAttackable
                    && PartymemberGuids.Contains(e.TargetGuid)
                    && e.Position.GetDistance(position) < distance)
                  .ToList();
            }
        }

        public List<WowDynobject> GetNearAoeSpells()
        {
            // TODO: recognize spells that harm me
            return new List<WowDynobject>();
        }

        public List<T> GetNearEnemies<T>(Vector3 position, double distance) where T : WowUnit
        {
            lock (queryLock)
            {
                return wowObjects
                       .ToList()
                       .OfType<T>()
                       .Where(e => e != null
                                && e.Guid != PlayerGuid
                                && !e.IsDead
                                && !e.IsNotAttackable
                                && WowInterface.HookManager.GetUnitReaction(Player, e) != WowUnitReaction.Friendly
                                && e.Position.GetDistance(position) < distance)
                       .ToList();
            }
        }

        public List<T> GetNearFriends<T>(Vector3 position, double distance) where T : WowUnit
        {
            lock (queryLock)
            {
                return wowObjects
                       .ToList()
                       .OfType<T>()
                       .Where(e => e != null
                                && e.Guid != PlayerGuid
                                && !e.IsDead
                                && !e.IsNotAttackable
                                && WowInterface.HookManager.GetUnitReaction(Player, e) == WowUnitReaction.Friendly
                                && e.Position.GetDistance(position) < distance)
                       .ToList();
            }
        }

        public List<WowPlayer> GetNearPartymembers(Vector3 position, double distance)
        {
            lock (queryLock)
            {
                return wowObjects.OfType<WowPlayer>()
                  .Where(e => e != null
                           && e.Guid != PlayerGuid
                           && !e.IsDead
                           && !e.IsNotAttackable
                           && PartymemberGuids.Contains(e.Guid)
                           && e.Position.GetDistance(position) < distance)
                  .ToList();
            }
        }

        public T GetWowObjectByGuid<T>(ulong guid) where T : WowObject
        {
            lock (queryLock) { return wowObjects.OfType<T>().FirstOrDefault(e => e.Guid == guid); }
        }

        public WowPlayer GetWowPlayerByName(string playername, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            lock (queryLock) { return wowObjects.OfType<WowPlayer>().FirstOrDefault(e => e.Name.Equals(playername.ToUpper(), stringComparison)); }
        }

        public bool RefreshIsWorldLoaded()
        {
            if (WowInterface.XMemory.Read(WowInterface.OffsetList.IsWorldLoaded, out int isWorldLoaded))
            {
                IsWorldLoaded = isWorldLoaded == 1;
                return IsWorldLoaded;
            }

            return false;
        }

        public void UpdateObject<T>(T wowObject) where T : WowObject
        {
            if (wowObject == null) { return; }
            UpdateObject(wowObject.Type, wowObject.BaseAddress);
        }

        public void UpdateObject(WowObjectType wowObjectType, IntPtr baseAddress)
        {
            lock (queryLock)
            {
                if (wowObjects.Count > 0)
                {
                    wowObjects.RemoveAll(e => e.BaseAddress == baseAddress);
                    WowObject obj = wowObjectType switch
                    {
                        WowObjectType.Dynobject => ReadWowDynobject(baseAddress, wowObjectType),
                        WowObjectType.Gameobject => ReadWowGameobject(baseAddress, wowObjectType),
                        WowObjectType.Player => ReadWowPlayer(baseAddress, wowObjectType),
                        WowObjectType.Unit => ReadWowUnit(baseAddress, wowObjectType),
                        WowObjectType.Corpse => ReadWowCorpse(baseAddress, wowObjectType),
                        WowObjectType.Container => ReadWowContainer(baseAddress, wowObjectType),
                        WowObjectType.Item => ReadWowItem(baseAddress, wowObjectType),
                        _ => ReadWowObject(baseAddress, wowObjectType),
                    };
                    wowObjects.Add(obj);
                }
            }
        }

        public void UpdateWowObjects()
        {
            lock (queryLock)
            {
                IsWorldLoaded = UpdateGlobalVar<int>(WowInterface.OffsetList.IsWorldLoaded) == 1;

                if (!IsWorldLoaded) { return; }

                PlayerGuid = UpdateGlobalVar<ulong>(WowInterface.OffsetList.PlayerGuid);
                TargetGuid = UpdateGlobalVar<ulong>(WowInterface.OffsetList.TargetGuid);
                LastTargetGuid = UpdateGlobalVar<ulong>(WowInterface.OffsetList.LastTargetGuid);
                PetGuid = UpdateGlobalVar<ulong>(WowInterface.OffsetList.PetGuid);
                PlayerBase = UpdateGlobalVar<IntPtr>(WowInterface.OffsetList.PlayerBase);
                MapId = UpdateGlobalVar<MapId>(WowInterface.OffsetList.MapId);
                ZoneId = UpdateGlobalVar<int>(WowInterface.OffsetList.ZoneId);

                if (WowInterface.XMemory.Read(WowInterface.OffsetList.CameraPointer, out IntPtr cameraPointer)
                    && WowInterface.XMemory.Read(IntPtr.Add(cameraPointer, WowInterface.OffsetList.CameraOffset.ToInt32()), out cameraPointer))
                {
                    Camera = UpdateGlobalVar<CameraInfo>(cameraPointer);
                }

                if (WowInterface.XMemory.Read(WowInterface.OffsetList.ZoneText, out IntPtr zoneNamePointer))
                {
                    ZoneName = UpdateGlobalVarString(zoneNamePointer);
                }

                if (WowInterface.XMemory.Read(WowInterface.OffsetList.ZoneSubText, out IntPtr zoneSubNamePointer))
                {
                    ZoneSubName = UpdateGlobalVarString(zoneSubNamePointer);
                }

                GameState = UpdateGlobalVarString(WowInterface.OffsetList.GameState);

                wowObjects.Clear();

                // get the current objectmanager
                // better not cache it until we switched map
                WowInterface.XMemory.Read(WowInterface.OffsetList.ClientConnection, out IntPtr clientConnection);
                WowInterface.XMemory.Read(IntPtr.Add(clientConnection, WowInterface.OffsetList.CurrentObjectManager.ToInt32()), out IntPtr currentObjectManager);
                CurrentObjectManager = currentObjectManager;

                // read the first object
                WowInterface.XMemory.Read(IntPtr.Add(CurrentObjectManager, WowInterface.OffsetList.FirstObject.ToInt32()), out IntPtr activeObjectBaseAddress);
                WowInterface.XMemory.Read(IntPtr.Add(activeObjectBaseAddress, WowInterface.OffsetList.WowObjectType.ToInt32()), out int activeObjectType);

                while (IsWorldLoaded && activeObjectType <= 7 && activeObjectType > 0)
                {
                    WowObjectType wowObjectType = (WowObjectType)activeObjectType;
                    WowObject obj = wowObjectType switch
                    {
                        WowObjectType.Container => ReadWowContainer(activeObjectBaseAddress, wowObjectType),
                        WowObjectType.Corpse => ReadWowCorpse(activeObjectBaseAddress, wowObjectType),
                        WowObjectType.Item => ReadWowItem(activeObjectBaseAddress, wowObjectType),
                        WowObjectType.Dynobject => ReadWowDynobject(activeObjectBaseAddress, wowObjectType),
                        WowObjectType.Gameobject => ReadWowGameobject(activeObjectBaseAddress, wowObjectType),
                        WowObjectType.Player => ReadWowPlayer(activeObjectBaseAddress, wowObjectType),
                        WowObjectType.Unit => ReadWowUnit(activeObjectBaseAddress, wowObjectType),
                        _ => ReadWowObject(activeObjectBaseAddress, wowObjectType),
                    };

                    if (obj != null)
                    {
                        wowObjects.Add(obj);

                        // set the global unit properties if a guid matches it
                        if (obj.Guid == TargetGuid) { Target = (WowUnit)obj; }
                        if (obj.Guid == PetGuid) { Pet = (WowUnit)obj; }
                        if (obj.Guid == LastTargetGuid) { LastTarget = (WowUnit)obj; }
                        if (obj.Guid == PartyleaderGuid) { Partyleader = (WowUnit)obj; }
                    }

                    WowInterface.XMemory.Read(IntPtr.Add(activeObjectBaseAddress, WowInterface.OffsetList.NextObject.ToInt32()), out activeObjectBaseAddress);
                    WowInterface.XMemory.Read(IntPtr.Add(activeObjectBaseAddress, WowInterface.OffsetList.WowObjectType.ToInt32()), out activeObjectType);
                }

                // read the party/raid leaders guid and if there is one, the group too
                PartyleaderGuid = ReadLeaderGuid();

                if (PartyleaderGuid > 0)
                {
                    PartymemberGuids = ReadPartymemberGuids();
                }

                Partymembers = wowObjects.OfType<WowUnit>().Where(e => e.Guid == PlayerGuid || PartymemberGuids.Contains(e.Guid)).ToList();
            }

            OnObjectUpdateComplete?.Invoke(WowObjects);
        }

        private ulong ReadLeaderGuid()
        {
            if (WowInterface.XMemory.Read(WowInterface.OffsetList.RaidLeader, out ulong partyleaderGuid))
            {
                if (partyleaderGuid == 0
                    && WowInterface.XMemory.Read(WowInterface.OffsetList.PartyLeader, out partyleaderGuid))
                {
                    return partyleaderGuid;
                }

                return partyleaderGuid;
            }

            return 0;
        }

        private List<ulong> ReadPartymemberGuids()
        {
            List<ulong> partymemberGuids = new List<ulong>();

            if (WowInterface.XMemory.Read(WowInterface.OffsetList.PartyLeader, out ulong partyLeader)
                && partyLeader != 0
                && WowInterface.XMemory.Read(WowInterface.OffsetList.PartyPlayerGuids, out RawPartyGuids partyMembers))
            {
                partymemberGuids.AddRange(partyMembers.AsList());
            }

            if (WowInterface.XMemory.Read(WowInterface.OffsetList.RaidLeader, out ulong raidLeader)
                && raidLeader != 0
                && WowInterface.XMemory.Read(WowInterface.OffsetList.RaidGroupStart, out RawRaidStruct raidStruct))
            {
                List<IntPtr> raidPointers = raidStruct.GetGuids();

                for (int i = 0; i < raidPointers.Count; ++i)
                {
                    if (WowInterface.XMemory.Read(raidPointers[i], out ulong guid)
                        && guid != 0)
                    {
                        partymemberGuids.Add(guid);
                    }
                }
            }

            return partymemberGuids.Where(e => e != 0).Distinct().ToList();
        }

        private string ReadPlayerName(ulong guid)
        {
            if (WowInterface.BotCache.TryGetUnitName(guid, out string cachedName))
            {
                return cachedName;
            }

            uint shortGuid;
            uint offset;

            WowInterface.XMemory.Read(IntPtr.Add(WowInterface.OffsetList.NameStore, WowInterface.OffsetList.NameMask.ToInt32()), out uint nameMask);
            WowInterface.XMemory.Read(IntPtr.Add(WowInterface.OffsetList.NameStore, WowInterface.OffsetList.NameBase.ToInt32()), out uint nameBase);

            shortGuid = (uint)guid & 0xfffffff;
            offset = 12 * (nameMask & shortGuid);

            WowInterface.XMemory.Read(new IntPtr(nameBase + offset + 8), out uint current);
            WowInterface.XMemory.Read(new IntPtr(nameBase + offset), out offset);

            if ((current & 0x1) == 0x1)
            {
                return string.Empty;
            }

            WowInterface.XMemory.Read(new IntPtr(current), out uint testGuid);

            while (testGuid != shortGuid)
            {
                WowInterface.XMemory.Read(new IntPtr(current + offset + 4), out current);

                if ((current & 0x1) == 0x1)
                {
                    return string.Empty;
                }

                WowInterface.XMemory.Read(new IntPtr(current), out testGuid);
            }

            WowInterface.XMemory.ReadString(IntPtr.Add(new IntPtr(current), WowInterface.OffsetList.NameString.ToInt32()), Encoding.UTF8, out string name, 16);

            if (name.Length > 0)
            {
                WowInterface.BotCache.CacheName(guid, name);
            }

            return name;
        }

        private string ReadUnitName(IntPtr activeObject, ulong guid)
        {
            if (WowInterface.BotCache.TryGetUnitName(guid, out string cachedName))
            {
                return cachedName;
            }

            if (WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.WowUnitName1.ToInt32()), out IntPtr objName)
                && WowInterface.XMemory.Read(IntPtr.Add(objName, WowInterface.OffsetList.WowUnitName2.ToInt32()), out objName)
                && WowInterface.XMemory.ReadString(objName, Encoding.UTF8, out string name))
            {
                if (name.Length > 0)
                {
                    WowInterface.BotCache.CacheName(guid, name);
                }

                return name;
            }

            return "unknown";
        }

        private WowContainer ReadWowContainer(IntPtr activeObject, WowObjectType wowObjectType)
        {
            if (WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.WowObjectDescriptor.ToInt32()), out IntPtr descriptorAddress)
                && WowInterface.XMemory.ReadStruct(IntPtr.Add(activeObject, WowInterface.OffsetList.WowObjectPosition.ToInt32()), out Vector3 position))
            {
                WowContainer container = new WowContainer(activeObject, wowObjectType)
                {
                    DescriptorAddress = descriptorAddress,
                    Position = position
                };

                return container.UpdateRawWowContainer(WowInterface.XMemory);
            }

            return null;
        }

        private WowCorpse ReadWowCorpse(IntPtr activeObject, WowObjectType wowObjectType)
        {
            if (WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.WowObjectDescriptor.ToInt32()), out IntPtr descriptorAddress)
                && WowInterface.XMemory.ReadStruct(IntPtr.Add(activeObject, WowInterface.OffsetList.WowObjectPosition.ToInt32()), out Vector3 position))
            {
                WowCorpse corpse = new WowCorpse(activeObject, wowObjectType)
                {
                    DescriptorAddress = descriptorAddress,
                    Position = position
                };

                return corpse.UpdateRawWowCorpse(WowInterface.XMemory);
            }

            return null;
        }

        private WowDynobject ReadWowDynobject(IntPtr activeObject, WowObjectType wowObjectType)
        {
            if (WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.WowObjectDescriptor.ToInt32()), out IntPtr descriptorAddress)
                && WowInterface.XMemory.ReadStruct(IntPtr.Add(activeObject, WowInterface.OffsetList.WowObjectPosition.ToInt32()), out Vector3 position))
            {
                WowDynobject dynObject = new WowDynobject(activeObject, wowObjectType)
                {
                    DescriptorAddress = descriptorAddress,
                    Position = position
                };

                return dynObject.UpdateRawWowDynobject(WowInterface.XMemory);
            }

            return null;
        }

        private WowGameobject ReadWowGameobject(IntPtr activeObject, WowObjectType wowObjectType)
        {
            if (WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.WowObjectDescriptor.ToInt32()), out IntPtr descriptorAddress)
                && WowInterface.XMemory.ReadStruct(IntPtr.Add(activeObject, WowInterface.OffsetList.WowGameobjectPosition.ToInt32()), out Vector3 position))
            {
                WowGameobject dynObject = new WowGameobject(activeObject, wowObjectType)
                {
                    DescriptorAddress = descriptorAddress,
                    Position = position
                };

                return dynObject.UpdateRawWowGameobject(WowInterface.XMemory);
            }

            return null;
        }

        private WowItem ReadWowItem(IntPtr activeObject, WowObjectType wowObjectType)
        {
            if (WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.WowObjectDescriptor.ToInt32()), out IntPtr descriptorAddress)
                && WowInterface.XMemory.ReadStruct(IntPtr.Add(activeObject, WowInterface.OffsetList.WowObjectPosition.ToInt32()), out Vector3 position))
            {
                WowItem item = new WowItem(activeObject, wowObjectType)
                {
                    DescriptorAddress = descriptorAddress,
                    Position = position
                };

                return item.UpdateRawWowItem(WowInterface.XMemory);
            }

            return null;
        }

        private WowObject ReadWowObject(IntPtr activeObject, WowObjectType wowObjectType)
        {
            if (WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.WowObjectDescriptor.ToInt32()), out IntPtr descriptorAddress)
                && WowInterface.XMemory.ReadStruct(IntPtr.Add(activeObject, WowInterface.OffsetList.WowObjectPosition.ToInt32()), out Vector3 position))
            {
                WowObject obj = new WowObject(activeObject, wowObjectType)
                {
                    DescriptorAddress = descriptorAddress,
                    Position = position
                };

                return obj.UpdateRawWowObject(WowInterface.XMemory);
            }

            return null;
        }

        private WowPlayer ReadWowPlayer(IntPtr activeObject, WowObjectType wowObjectType)
        {
            if (WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.WowObjectDescriptor.ToInt32()), out IntPtr descriptorAddress)
                && WowInterface.XMemory.ReadStruct(IntPtr.Add(activeObject, WowInterface.OffsetList.WowUnitPosition.ToInt32()), out Vector3 position)
                && WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.WowUnitRotation.ToInt32()), out float rotation)
                && WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.WowUnitIsAutoAttacking.ToInt32()), out int isAutoAttacking)
                && WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.CurrentlyCastingSpellId.ToInt32()), out int castingId)
                && WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.CurrentlyChannelingSpellId.ToInt32()), out int channelingId))
            {
                WowPlayer player = new WowPlayer(activeObject, wowObjectType)
                {
                    DescriptorAddress = descriptorAddress,
                    Position = position,
                    Rotation = rotation,
                    IsAutoAttacking = isAutoAttacking == 1,
                    CurrentlyCastingSpellId = castingId,
                    CurrentlyChannelingSpellId = channelingId
                };

                // first read the descriptor, then lookup the Name by GUID
                player.UpdateRawWowPlayer(WowInterface.XMemory);
                player.Name = ReadPlayerName(player.Guid);

                if (player.Guid == PlayerGuid)
                {
                    if (WowInterface.XMemory.Read(WowInterface.OffsetList.ComboPoints, out byte comboPoints))
                    {
                        player.ComboPoints = comboPoints;
                    }

                    Player = player;
                }

                // if (player.Guid == TargetGuid
                //     || player.Guid == PlayerGuid
                //     || PartymemberGuids.Contains(player.Guid))
                // {
                player.Auras = WowInterface.HookManager.GetUnitAuras(player);
                // }

                return player;
            }

            return null;
        }

        private WowUnit ReadWowUnit(IntPtr activeObject, WowObjectType wowObjectType)
        {
            if (WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.WowObjectDescriptor.ToInt32()), out IntPtr descriptorAddress)
                && WowInterface.XMemory.ReadStruct(IntPtr.Add(activeObject, WowInterface.OffsetList.WowUnitPosition.ToInt32()), out Vector3 position)
                && WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.WowUnitRotation.ToInt32()), out float rotation)
                && WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.WowUnitIsAutoAttacking.ToInt32()), out int isAutoAttacking)
                && WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.CurrentlyCastingSpellId.ToInt32()), out int castingId)
                && WowInterface.XMemory.Read(IntPtr.Add(activeObject, WowInterface.OffsetList.CurrentlyChannelingSpellId.ToInt32()), out int channelingId))
            {
                WowUnit unit = new WowUnit(activeObject, wowObjectType)
                {
                    DescriptorAddress = descriptorAddress,
                    Position = position,
                    Rotation = rotation,
                    IsAutoAttacking = isAutoAttacking == 1,
                    CurrentlyCastingSpellId = castingId,
                    CurrentlyChannelingSpellId = channelingId
                };

                // First read the descriptor, then lookup the Name by GUID
                unit.UpdateRawWowUnit(WowInterface.XMemory);
                unit.Name = ReadUnitName(activeObject, unit.Guid);

                if (unit.Guid == TargetGuid)
                {
                    unit.Auras = WowInterface.HookManager.GetUnitAuras(unit);
                }

                return unit;
            }

            return null;
        }

        private T UpdateGlobalVar<T>(IntPtr address) where T : unmanaged
        {
            return WowInterface.XMemory.Read(address, out T v) ? v : default;
        }

        private string UpdateGlobalVarString(IntPtr address, int maxLenght = 128)
        {
            return WowInterface.XMemory.ReadString(address, Encoding.UTF8, out string v, maxLenght) ? v : string.Empty;
        }
    }
}