﻿using System.Collections.Generic;
using SWLOR.Game.Server.Core;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Core.NWScript.Enum.Item;
using SWLOR.Game.Server.Service;
using SWLOR.Game.Server.Service.AbilityService;
using SWLOR.Game.Server.Service.CombatService;
using SWLOR.Game.Server.Service.PerkService;
using SWLOR.Game.Server.Service.SkillService;
using static SWLOR.Game.Server.Core.NWScript.NWScript;

namespace SWLOR.Game.Server.Feature.AbilityDefinition.OneHanded
{
    public class SaberStrikeAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            SaberStrike1(builder);
            SaberStrike2(builder);
            SaberStrike3(builder);

            return builder.Build();
        }

        private static string Validation(uint activator, uint target, int level, Location targetLocation)
        {
            var weapon = GetItemInSlot(InventorySlot.RightHand, activator);
            var offHand = GetItemInSlot(InventorySlot.LeftHand, activator);
            var rightHandType = GetBaseItemType(weapon);
            var leftHandType = GetBaseItemType(offHand);

            if (Item.LightsaberBaseItemTypes.Contains(rightHandType) ||
                Item.LightsaberBaseItemTypes.Contains(leftHandType))
            {
                return string.Empty;
            }
            else
                return "This is a lightsaber ability.";
        }

        private static void ImpactAction(uint activator, uint target, int level, Location targetLocation)
        {
            var dmg = 0.0f;
            var inflict = false;
            var breachTime = 0f;
            // If activator is in stealth mode, force them out of stealth mode.
            if (GetActionMode(activator, ActionMode.Stealth) == true)
                SetActionMode(activator, ActionMode.Stealth, false);

            switch (level)
            {
                case 1:
                    dmg = 6.5f;
                    if (d2() == 1) inflict = true;
                    breachTime = 30f;
                    break;
                case 2:
                    dmg = 8.0f;
                    if (d4() > 1) inflict = true;
                    breachTime = 60f;
                    break;
                case 3:
                    dmg = 11.5f;
                    inflict = true;
                    breachTime = 60f;
                    break;
                default:
                    break;
            }

            Enmity.ModifyEnmityOnAll(activator, 1);
            CombatPoint.AddCombatPoint(activator, target, SkillType.Force, 3);

            var willpower = GetAbilityModifier(AbilityType.Willpower, activator);
            var defense = Stat.GetDefense(target, CombatDamageType.Physical);
            var vitality = GetAbilityModifier(AbilityType.Vitality, target);
            var damage = Combat.CalculateDamage(dmg, willpower, defense, vitality, 0);
            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Slashing), target);
            if (inflict) ApplyEffectToObject(DurationType.Temporary, EffectACDecrease(2), target, breachTime);
        }

        private static void SaberStrike1(AbilityBuilder builder)
        {
            builder.Create(FeatType.SaberStrike1, PerkType.SaberStrike)
                .Name("Saber Strike I")
                .HasRecastDelay(RecastGroup.SaberStrike, 60f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(3)
                .IsCastedAbility()
                .IsHostileAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void SaberStrike2(AbilityBuilder builder)
        {
            builder.Create(FeatType.SaberStrike2, PerkType.SaberStrike)
                .Name("Saber Strike II")
                .HasRecastDelay(RecastGroup.SaberStrike, 60f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(5)
                .IsCastedAbility()
                .IsHostileAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void SaberStrike3(AbilityBuilder builder)
        {
            builder.Create(FeatType.SaberStrike3, PerkType.SaberStrike)
                .Name("Saber Strike III")
                .HasRecastDelay(RecastGroup.SaberStrike, 60f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(8)
                .IsCastedAbility()
                .IsHostileAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
    }
}