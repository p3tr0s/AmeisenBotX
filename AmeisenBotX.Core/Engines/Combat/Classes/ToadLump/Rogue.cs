﻿using AmeisenBotX.Core.Engines.Character.Comparators;
using AmeisenBotX.Core.Engines.Character.Talents.Objects;
using AmeisenBotX.Core.Fsm;
using AmeisenBotX.Wow.Objects.Enums;

namespace AmeisenBotX.Core.Engines.Combat.Classes.ToadLump
{
    public class Rogue : BasicCombatClass
    {
        public Rogue(AmeisenBotInterfaces bot, AmeisenBotFsm stateMachine) : base(bot, stateMachine)
        {
            /*
            MyAuraManager.BuffsToKeepActive = new Dictionary<string, CastFunction>()
            {
                { battleShoutSpell, () => TryCastSpell(battleShoutSpell, 0, true) }
            };

            TargetAuraManager.DebuffsToKeepActive = new Dictionary<string, CastFunction>()
            {
                { hamstringSpell, () => Bot.Target.Type == WowObjectType.Player && TryCastSpell(hamstringSpell, Bot.NewBot.TargetGuid, true) },
                { rendSpell, () => Bot.Target.Type == WowObjectType.Player && Bot.Player.Rage > 75 && TryCastSpell(rendSpell, Bot.NewBot.TargetGuid, true) }
            };

            TargetInterruptManager.InterruptSpells = new()
            {
                { 0, (x) => TryCastSpellWarrior(intimidatingShoutSpell, berserkerStanceSpell, x.Guid, true) },
                { 1, (x) => TryCastSpellWarrior(intimidatingShoutSpell, battleStanceSpell, x.Guid, true) }
            };

            HeroicStrikeEvent = new(TimeSpan.FromSeconds(2));
            */
        }

        public override string Description => "Control low level rogues for grinding purposes";

        public override string Displayname => "Rogue - Low Level";

        public override bool HandlesMovement => false;

        public override bool IsMelee => true;

        public override IItemComparator ItemComparator { get; set; } = new BasicStrengthComparator(new() { WowArmorType.LEATHER }, new() { WowWeaponType.ONEHANDED_SWORDS, WowWeaponType.ONEHANDED_MACES, WowWeaponType.DAGGERS });

        public override WowRole Role => WowRole.Dps;

        public override TalentTree Talents { get; } = new()
        {
            //todo make these accurate to rogue
            Tree1 = new()
            {
                { 1, new(1, 1, 3) },
                { 3, new(1, 3, 2) },
                { 5, new(1, 5, 2) },
                { 6, new(1, 6, 3) },
                { 9, new(1, 9, 2) },
                { 10, new(1, 10, 3) },
                { 11, new(1, 11, 3) },
            },
            Tree2 = new()
            {
                { 1, new(2, 1, 3) },
                { 3, new(2, 3, 5) },
                { 5, new(2, 5, 5) },
                { 6, new(2, 6, 3) },
                { 10, new(2, 10, 5) },
                { 13, new(2, 13, 3) },
                { 14, new(2, 14, 1) },
                { 16, new(2, 16, 1) },
                { 17, new(2, 17, 5) },
                { 18, new(2, 18, 3) },
                { 19, new(2, 19, 1) },
                { 20, new(2, 20, 2) },
                { 22, new(2, 22, 5) },
                { 23, new(2, 23, 1) },
                { 24, new(2, 24, 1) },
                { 25, new(2, 25, 3) },
                { 26, new(2, 26, 5) },
                { 27, new(2, 27, 1) },
            },
            Tree3 = new(),
        };

        public override bool UseAutoAttacks => true;

        public override string Version => "0.1.0";

        //todo: decide what this value does and whether it should be true
        public override bool WalkBehindEnemy => false;

        public override WowClass WowClass => WowClass.Rogue;

        public override void Execute()
        {
            base.Execute();

            if (SelectTarget(TargetManagerDps))
            {
                if (Bot.Target != null)
                {
                    double distanceToTarget = Bot.Target.Position.GetDistance(Bot.Player.Position);

                    if ((Bot.Player.IsDazed
                        || Bot.Player.IsConfused
                        || Bot.Player.IsPossessed
                        || Bot.Player.IsFleeing)
                        && TryCastSpell(heroicFurySpell, 0))
                    {
                        return;
                    }

                    if (distanceToTarget > 5.0)
                    {
                        return;
                    }
                    else
                    {
                        if (TryCastSpellRogue(eviscerateSpell, Bot.Wow.TargetGuid, needsEnergy: true, needsCombopoints: true, requiredCombopoints: 5))
                        {
                            return;
                        }
                        if (TryCastSpellRogue(sinisterStrikeSpell, Bot.Wow.TargetGuid, needsEnergy: true))
                        {
                            return;
                        }
                    }
                }
            }
        }

        public override void OutOfCombatExecute()
        {
            base.OutOfCombatExecute();
        }
    }
}