﻿//using Random = SWLOR.Game.Server.Service.Random;

using System.Collections.Generic;
using SWLOR.Game.Server.Core;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.Service;
using SWLOR.Game.Server.Service.AbilityService;
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

            if (Item.LightsaberBaseItemTypes.Contains(GetBaseItemType(weapon))
                && (GetBaseItemType((GetItemInSlot(InventorySlot.LeftHand))) == Core.NWScript.Enum.Item.BaseItem.SmallShield ||
                    GetBaseItemType((GetItemInSlot(InventorySlot.LeftHand))) == Core.NWScript.Enum.Item.BaseItem.LargeShield ||
                    GetBaseItemType((GetItemInSlot(InventorySlot.LeftHand))) == Core.NWScript.Enum.Item.BaseItem.TowerShield ||
                    GetBaseItemType((GetItemInSlot(InventorySlot.LeftHand))) == Core.NWScript.Enum.Item.BaseItem.Invalid))
            {
                return "This is a one-handed ability.";
            }
            else
                return string.Empty;
        }

        private static void ImpactAction(uint activator, uint target, int level, Location targetLocation)
        {
            var dmg = 0.0f;
            var inflictBreach = false;
            // If activator is in stealth mode, force them out of stealth mode.
            if (GetActionMode(activator, ActionMode.Stealth) == true)
                SetActionMode(activator, ActionMode.Stealth, false);

            switch (level)
            {
                case 1:
                    dmg = 6.5f;
                    if (d2() == 1) inflictBreach = true;
                    break;
                case 2:
                    dmg = 8.0f;
                    if (d4() > 1) inflictBreach = true;
                    break;
                case 3:
                    dmg = 11.5f;
                    inflictBreach = true;
                    break;
                default:
                    break;
            }

            var willpower = GetAbilityModifier(AbilityType.Willpower, activator);
            var defense = Combat.CalculateDefense(target);
            var vitality = GetAbilityModifier(AbilityType.Vitality, target);
            var damage = Combat.CalculateDamage(dmg, willpower, defense, vitality, false);
            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Slashing), target);
            if (inflictBreach) ApplyEffectToObject(DurationType.Temporary, EffectACDecrease(2), target, 60f);

            Enmity.ModifyEnmityOnAll(activator, 1);
            CombatPoint.AddCombatPointToAllTagged(activator, SkillType.Force, 3);
        }

        private static void SaberStrike1(AbilityBuilder builder)
        {
            builder.Create(FeatType.SaberStrike1, PerkType.SaberStrike)
                .Name("Saber Strike I")
                .HasRecastDelay(RecastGroup.SaberStrike, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(3)
                .IsCastedAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void SaberStrike2(AbilityBuilder builder)
        {
            builder.Create(FeatType.SaberStrike2, PerkType.SaberStrike)
                .Name("Saber Strike II")
                .HasRecastDelay(RecastGroup.SaberStrike, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(5)
                .IsCastedAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void SaberStrike3(AbilityBuilder builder)
        {
            builder.Create(FeatType.SaberStrike3, PerkType.SaberStrike)
                .Name("Saber Strike III")
                .HasRecastDelay(RecastGroup.SaberStrike, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(8)
                .IsCastedAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
    }
}