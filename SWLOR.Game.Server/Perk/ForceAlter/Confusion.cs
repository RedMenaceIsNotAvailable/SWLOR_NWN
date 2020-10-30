﻿using System;
using SWLOR.Game.Server.Core.NWScript;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.GameObject;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Core.NWScript.Enum.VisualEffect;
using SWLOR.Game.Server.Service.Legacy;

namespace SWLOR.Game.Server.Perk.ForceAlter
{
    public class Confusion : IPerkHandler
    {
        public PerkType PerkType => PerkType.Confusion;
        public string CanCastSpell(NWCreature oPC, NWObject oTarget, int spellTier)
        {
            switch (spellTier)
            {                
                case 1:
                    if (!oTarget.IsCreature)
                        return "This ability can only be used on living creatures.";
                    NWCreature targetCreature = oTarget.Object;
                    if (targetCreature.RacialType == RacialType.Robot)
                        return "This ability cannot be used on droids.";
                    break;
                case 2:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(spellTier));
            }

            return string.Empty;
        }

        public int FPCost(NWCreature oPC, int baseFPCost, int spellTier)
        {
            return baseFPCost;
        }

        public float CastingTime(NWCreature oPC, float baseCastingTime, int spellTier)
        {
            return baseCastingTime;
        }

        public float CooldownTime(NWCreature oPC, float baseCooldownTime, int spellTier)
        {
            if (spellTier == 1) return 300; // 5 minutes
            else if (spellTier == 2) return 1800; // 30 minutes

            return baseCooldownTime;
        }

        public int? CooldownCategoryID(NWCreature creature, int? baseCooldownCategoryID, int spellTier)
        {
            return baseCooldownCategoryID;
        }

        public void OnImpact(NWCreature creature, NWObject target, int perkLevel, int spellTier)
        {
            ApplyEffect(creature, target, spellTier);
        }

        public void OnPurchased(NWCreature creature, int newLevel)
        {
        }

        public void OnRemoved(NWCreature creature)
        {
        }

        public void OnItemEquipped(NWCreature creature, NWItem oItem)
        {
        }

        public void OnItemUnequipped(NWCreature creature, NWItem oItem)
        {
        }

        public void OnCustomEnmityRule(NWCreature creature, int amount)
        {
        }

        public bool IsHostile()
        {
            return false;
        }

        public void OnConcentrationTick(NWCreature creature, NWObject target, int spellTier, int tick)
        {
        }

        private void ApplyEffect(NWCreature creature, NWObject target, int spellTier)
        {
            var radiusSize = RadiusSize.Small;            

            var confusionEffect = NWScript.EffectConfused();

            // Handle effects for differing spellTier values
            switch (spellTier)
            {
                case 1:
                    if ((creature.Wisdom > NWScript.GetAbilityModifier(AbilityType.Wisdom, target) || creature == target) && NWScript.GetDistanceBetween(creature.Object, target) <= radiusSize)
                    {
                        creature.AssignCommand(() =>
                        {
                            NWScript.ApplyEffectToObject(DurationType.Temporary, confusionEffect, target, 6.1f);
                            // Play VFX
                            NWScript.ApplyEffectToObject(DurationType.Instant, NWScript.EffectVisualEffect(VisualEffect.Vfx_Imp_Confusion_S), target);
                        });
                        if (!creature.IsPlayer)
                        {
                            SkillService.RegisterPCToNPCForSkill(creature.Object, target, SkillType.ForceAlter);
                        }
                    }
                    else
                    {
                        creature.SendMessage("Confusion failed.");
                    }
                    break;
                case 2:
                    NWCreature targetCreature = NWScript.GetFirstObjectInShape(Shape.Sphere, radiusSize, creature.Location, true, ObjectType.Creature);
                    while (targetCreature.IsValid)
                    {
                        if (targetCreature.RacialType == RacialType.Robot || NWScript.GetIsReactionTypeHostile(targetCreature, creature) == false)
                        {
                            // Do nothing against droids or non-hostile creatures, skip object
                            targetCreature = NWScript.GetNextObjectInShape(Shape.Sphere, radiusSize, creature.Location, true, ObjectType.Creature);
                            continue;
                        }

                        if (creature.Wisdom > targetCreature.Wisdom)
                        {
                            var targetCreatureCopy = targetCreature; // Closure can modify the iteration variable so we copy it first.
                            creature.AssignCommand(() =>
                            {
                                NWScript.ApplyEffectToObject(DurationType.Temporary, confusionEffect, targetCreatureCopy, 6.1f);
                                // Play VFX
                                NWScript.ApplyEffectToObject(DurationType.Instant, NWScript.EffectVisualEffect(VisualEffect.Vfx_Imp_Confusion_S), targetCreatureCopy);
                            });

                            if (!creature.IsPlayer)
                            {
                                SkillService.RegisterPCToNPCForSkill(creature.Object, targetCreature, SkillType.ForceAlter);
                            }
                        }
                        else
                        {
                            creature.SendMessage("Confusion failed.");
                        }

                        targetCreature = NWScript.GetNextObjectInShape(Shape.Sphere, radiusSize, creature.Location, true, ObjectType.Creature);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(spellTier));
            }
        }
    }
}
