﻿using AmeisenBotX.BehaviorTree;
using AmeisenBotX.BehaviorTree.Enums;
using AmeisenBotX.BehaviorTree.Objects;
using AmeisenBotX.Common.Math;
using AmeisenBotX.Common.Utils;
using AmeisenBotX.Core.Data.Objects;
using AmeisenBotX.Core.Engines.Movement.Enums;
using AmeisenBotX.Core.Fsm.Enums;
using AmeisenBotX.Core.Fsm.States.StaticDeathRoutes;
using AmeisenBotX.Wow.Objects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmeisenBotX.Core.Fsm.States
{
    public class StateGhost : BasicState
    {
        private readonly List<IStaticDeathRoute> StaticDeathRoutes = new()
        {
            new ForgeOfSoulsDeathRoute(),
            new PitOfSaronDeathRoute()
        };

        private ulong playerToFollowGuid = 0;

        public StateGhost(AmeisenBotFsm stateMachine, AmeisenBotConfig config, AmeisenBotInterfaces bot) : base(stateMachine, config, bot)
        {
            Selector dungeonSelector = new
            (
                () => Config.DungeonUsePartyMode,
                new Selector
                (
                    () => Bot.Dungeon.TryGetProfileByMapId(StateMachine.GetState<StateDead>().LastDiedMap) != null,
                    new Leaf(RunToDungeonProfileEntry),
                    new Selector
                    (
                        () => IsUnitToFollowNear(out playerToFollowGuid),
                        new Leaf(FollowNearestUnit),
                        new Leaf(RunToCorpsePositionAndSearchForPortals)
                    )
                ),
                new Selector
                (
                    () => Bot.Dungeon.Profile.WorldEntry != default,
                    new Leaf(RunToDungeonEntry),
                    new Leaf(RunToCorpsePositionAndSearchForPortals)
                )
            );

            BehaviorTree = new
            (
                new Selector
                (
                    () => Bot.Objects.MapId.IsBattlegroundMap(),
                    new Leaf(() =>
                    {
                        Bot.Movement.StopMovement();
                        return BehaviorTreeStatus.Ongoing;
                    }),
                    new Selector
                    (
                        () => CanUseStaticPaths(),
                        new Leaf(FollowStaticPath),
                        new Selector
                        (
                            () => StateMachine.GetState<StateDead>().LastDiedMap.IsDungeonMap(),
                            dungeonSelector,
                            new Leaf(RunToCorpseAndRetrieveIt)
                        )
                    )
                )
            );
        }

        private AmeisenBotBehaviorTree BehaviorTree { get; }

        private Vector3 CorpsePosition { get; set; }

        private IEnumerable<IWowGameobject> NearPortals { get; set; }

        private IWowPlayer PlayerToFollow => Bot.GetWowObjectByGuid<IWowPlayer>(playerToFollowGuid);

        private bool SearchedStaticRoutes { get; set; }

        private IStaticDeathRoute StaticRoute { get; set; }

        public override void Enter()
        {
        }

        public override void Execute()
        {
            if (Bot.Player.Health > 1)
            {
                StateMachine.SetState(BotState.Idle);
                return;
            }

            NearPortals = Bot.Objects.WowObjects
                .OfType<IWowGameobject>()
                .Where(e => e.DisplayId == (int)WowGameobjectDisplayId.UtgardeKeepDungeonPortalNormal
                         || e.DisplayId == (int)WowGameobjectDisplayId.UtgardeKeepDungeonPortalHeroic);

            if (Bot.Memory.Read(Bot.Offsets.CorpsePosition, out Vector3 corpsePosition))
            {
                CorpsePosition = corpsePosition;
            }

            BehaviorTree.Tick();
        }

        public override void Leave()
        {
            // Bot.Player.IsGhost = false;
            SearchedStaticRoutes = false;
            StaticRoute = null;
        }

        /// <summary>
        /// This method searches for static death routes, this is needed when pathfinding
        /// cannot find a good route from the graveyard to th dungeon entry. For example
        /// the ICC dungeons are only reachable by flying, its easier to use static routes.
        /// </summary>
        /// <returns>True when a static path can be used, false if not</returns>
        private bool CanUseStaticPaths()
        {
            if (!SearchedStaticRoutes)
            {
                SearchedStaticRoutes = true;

                Vector3 endPosition = Bot.Dungeon.Profile != null ? Bot.Dungeon.Profile.WorldEntry : CorpsePosition;
                IStaticDeathRoute staticRoute = StaticDeathRoutes.FirstOrDefault(e => e.IsUseable(Bot.Objects.MapId, Bot.Player.Position, endPosition));

                if (staticRoute != null)
                {
                    StaticRoute = staticRoute;
                    StaticRoute.Init(Bot.Player.Position);
                }
                else
                {
                    staticRoute = StaticDeathRoutes.FirstOrDefault(e => e.IsUseable(Bot.Objects.MapId, Bot.Player.Position, CorpsePosition));

                    if (staticRoute != null)
                    {
                        StaticRoute = staticRoute;
                        StaticRoute.Init(Bot.Player.Position);
                    }
                }
            }

            return StaticRoute != null;
        }

        private BehaviorTreeStatus FollowNearestUnit()
        {
            if (Bot.Player.Position.GetDistance(PlayerToFollow.Position) > Config.MinFollowDistance)
            {
                Bot.Movement.SetMovementAction(MovementAction.Move, PlayerToFollow.Position);
            }

            return BehaviorTreeStatus.Ongoing;
        }

        private BehaviorTreeStatus FollowStaticPath()
        {
            Vector3 nextPosition = StaticRoute.GetNextPoint(Bot.Player.Position);

            if (nextPosition != Vector3.Zero)
            {
                Bot.Movement.SetMovementAction(MovementAction.DirectMove, nextPosition);
                return BehaviorTreeStatus.Ongoing;
            }
            else
            {
                // we should be in the dungeon now
                return BehaviorTreeStatus.Ongoing;
            }
        }

        /// <summary>
        /// Check whether the unit is out of range based on the config entries "MinFollowDistance" and "MaxFollowDistance".
        /// </summary>
        /// <param name="player">Player to check</param>
        /// <returns></returns>
        private bool IsUnitOutOfRange(IWowPlayer player)
        {
            double distance = player.Position.GetDistance(Bot.Player.Position);
            return distance < Config.MinFollowDistance || distance > Config.MaxFollowDistance;
        }

        /// <summary>
        /// This method tries to find anyone that we can follow based on the config entries set.
        /// </summary>
        /// <param name="guid">Guid of the found entity</param>
        /// <returns>True when a valid unit has been found, false if not</returns>
        private bool IsUnitToFollowNear(out ulong guid)
        {
            IEnumerable<IWowPlayer> wowPlayers = Bot.Objects.WowObjects.OfType<IWowPlayer>();
            guid = 0;

            if (wowPlayers.Any())
            {
                if (Config.FollowSpecificCharacter)
                {
                    IWowPlayer specificPlayer = wowPlayers.FirstOrDefault(p => Bot.Db.GetUnitName(p, out string name) && name == Config.SpecificCharacterToFollow && !IsUnitOutOfRange(p));

                    if (specificPlayer != null)
                    {
                        guid = specificPlayer.Guid;
                    }
                }

                // check the group/raid leader
                if (guid == 0 && Config.FollowGroupLeader)
                {
                    IWowPlayer groupLeader = wowPlayers.FirstOrDefault(p => Bot.Db.GetUnitName(p, out string name) && name == Config.SpecificCharacterToFollow && !IsUnitOutOfRange(p));

                    if (groupLeader != null)
                    {
                        guid = groupLeader.Guid;
                    }
                }

                // check the group members
                if (guid == 0 && Config.FollowGroupMembers)
                {
                    IWowPlayer groupMember = wowPlayers.FirstOrDefault(p => Bot.Db.GetUnitName(p, out string name) && name == Config.SpecificCharacterToFollow && !IsUnitOutOfRange(p));

                    if (groupMember != null)
                    {
                        guid = groupMember.Guid;
                    }
                }
            }

            return guid != 0;
        }

        private BehaviorTreeStatus RunToAndExecute(Vector3 position, Action action, double distance = 20.0)
        {
            if (Bot.Player.Position.GetDistance(position) > distance)
            {
                Bot.Movement.SetMovementAction(MovementAction.Move, position);
                return BehaviorTreeStatus.Ongoing;
            }
            else
            {
                action();
                return BehaviorTreeStatus.Success;
            }
        }

        private BehaviorTreeStatus RunToCorpseAndRetrieveIt()
        {
            if (Bot.Player.Position.GetDistance(CorpsePosition) > Config.GhostResurrectThreshold)
            {
                Bot.Movement.SetMovementAction(MovementAction.Move, CorpsePosition);
                return BehaviorTreeStatus.Ongoing;
            }
            else
            {
                Bot.Wow.LuaRetrieveCorpse();
                return BehaviorTreeStatus.Success;
            }
        }

        private BehaviorTreeStatus RunToCorpsePositionAndSearchForPortals()
        {
            // we need to uplift the corpse to ground level because
            // blizz decided its good to place the corpse -100000m
            // below the dungeon entry when a player dies inside
            Vector3 upliftedPosition = CorpsePosition;

            // TODO: get real ground level from maps
            upliftedPosition.Z = 0f;

            return RunToAndExecute(upliftedPosition, () => RunToNearestPortal());
        }

        private BehaviorTreeStatus RunToDungeonEntry()
        {
            return RunToAndExecute(Bot.Dungeon.Profile.WorldEntry, () => RunToNearestPortal());
        }

        private BehaviorTreeStatus RunToDungeonProfileEntry()
        {
            Vector3 position = Bot.Dungeon.TryGetProfileByMapId(StateMachine.GetState<StateDead>().LastDiedMap).WorldEntry;
            return RunToAndExecute(position, () => RunToNearestPortal());
        }

        private void RunToNearestPortal()
        {
            if (NearPortals.Any())
            {
                Bot.Movement.SetMovementAction(MovementAction.Move, BotUtils.MoveAhead(Bot.Player.Position, NearPortals.OrderBy(e => e.Position.GetDistance(Bot.Player.Position)).First().Position, 4f));
            }
        }
    }
}