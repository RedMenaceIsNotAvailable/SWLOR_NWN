﻿//using Random = SWLOR.Game.Server.Service.Random;
using System.Collections.Generic;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Core.NWScript.Enum.Creature;
using SWLOR.Game.Server.Core.Bioware;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.Service;
using SWLOR.Game.Server.Service.AbilityService;
using static SWLOR.Game.Server.Core.NWScript.NWScript;
using SWLOR.Game.Server.Core.NWScript.Enum.VisualEffect;

namespace SWLOR.Game.Server.Feature.AbilityDefinition
{
    public class ForceLeapAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<Feat, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            ForceLeap1(builder);
            ForceLeap2(builder);
            ForceLeap3(builder);

            return builder.Build();
        }

        private static string Validation(uint activator, uint target, int level)
        {
            var weapon = GetItemInSlot(InventorySlot.RightHand);

            if (Item.VibrobladeBaseItemTypes.Contains(GetBaseItemType(weapon))
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

        private static void ImpactAction(uint activator, uint target, int level)
        {
            var damage = 0;
            // If activator is in stealth mode, force them out of stealth mode.
            if (GetActionMode(activator, ActionMode.Stealth) == true)
                SetActionMode(activator, ActionMode.Stealth, false);

            switch (level)
            {
                case 1:
                    damage = d4();
                    break;
                case 2:
                    damage = d6();
                    break;
                case 3:
                    damage = d4();
                    break;
                default:
                    break;
            }

            ClearAllActions();
            ApplyEffectToObject(DurationType.Instant, EffectDisappearAppear(GetLocation(target)), activator, 2f);

            DelayCommand(2f, () =>
            {                
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Sonic), target);
                ApplyEffectToObject(DurationType.Temporary, EffectStunned(), target, 2f);
            });

            Enmity.ModifyEnmityOnAll(activator, 1);
            CombatPoint.AddCombatPointToAllTagged(activator, SkillType.Force, 3);
        }

        private static void ForceLeap1(AbilityBuilder builder)
        {
            builder.Create(Feat.ForceLeap1, PerkType.ForceLeap)
                .Name("Force Leap I")
                .HasRecastDelay(RecastGroup.ForceLeap, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(3)
                .IsCastedAbility()
                .HasCustomValidation((activator, target, level) =>
                {
                    return Validation(activator, target, level);
                })
                .HasImpactAction((activator, target, level) =>
                {
                    ImpactAction(activator, target, level);
                });
        }
        private static void ForceLeap2(AbilityBuilder builder)
        {
            builder.Create(Feat.ForceLeap2, PerkType.ForceLeap)
                .Name("Force Leap II")
                .HasRecastDelay(RecastGroup.ForceLeap, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(4)
                .IsCastedAbility()
                .HasCustomValidation((activator, target, level) =>
                {
                    return Validation(activator, target, level);
                })
                .HasImpactAction((activator, target, level) =>
                {
                    ImpactAction(activator, target, level);
                });
        }
        private static void ForceLeap3(AbilityBuilder builder)
        {
            builder.Create(Feat.ForceLeap3, PerkType.ForceLeap)
                .Name("Force Leap III")
                .HasRecastDelay(RecastGroup.ForceLeap, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(5)
                .IsCastedAbility()
                .HasCustomValidation((activator, target, level) =>
                {
                    return Validation(activator, target, level);
                })
                .HasImpactAction((activator, target, level) =>
                {
                    ImpactAction(activator, target, level);
                });
        }
    }
}