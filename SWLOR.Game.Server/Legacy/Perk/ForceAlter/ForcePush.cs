﻿using SWLOR.Game.Server.Core.NWScript;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Core.NWScript.Enum.Creature;
using SWLOR.Game.Server.Core.NWScript.Enum.VisualEffect;
using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.GameObject;
using SWLOR.Game.Server.Legacy.Service;
using PerkType = SWLOR.Game.Server.Legacy.Enumeration.PerkType;
using SkillType = SWLOR.Game.Server.Legacy.Enumeration.SkillType;

namespace SWLOR.Game.Server.Legacy.Perk.ForceAlter
{
    public class ForcePush: IPerkHandler
    {
        public PerkType PerkType => PerkType.ForcePush;
        public string CanCastSpell(NWCreature oPC, NWObject oTarget, int spellTier)
        {
            var size = NWScript.GetCreatureSize(oTarget);
            var maxSize = CreatureSize.Invalid;
            switch (spellTier)
            {
                case 1:
                    maxSize = CreatureSize.Small;
                    break;
                case 2:
                    maxSize = CreatureSize.Medium;
                    break;
                case 3:
                    maxSize = CreatureSize.Large;
                    break;
                case 4:
                    maxSize = CreatureSize.Huge;
                    break;
            }

            if (size > maxSize)
                return "Your target is too large to force push.";

            return string.Empty;
        }
        
        public int FPCost(NWCreature oPC, int baseFPCost, int spellTier)
        {
            switch (spellTier)
            {
                case 1: return 4;
                case 2: return 6;
                case 3: return 8;
                case 4: return 10;
            }

            return baseFPCost;
        }

        public float CastingTime(NWCreature oPC, float baseCastingTime, int spellTier)
        {
            return baseCastingTime;
        }

        public float CooldownTime(NWCreature oPC, float baseCooldownTime, int spellTier)
        {
            return baseCooldownTime;
        }

        public int? CooldownCategoryID(NWCreature creature, int? baseCooldownCategoryID, int spellTier)
        {
            return baseCooldownCategoryID;
        }

        public void OnImpact(NWCreature creature, NWObject target, int perkLevel, int spellTier)
        {
            var duration = 0.0f;

            switch (spellTier)
            {
                case 1:
                    duration = 6f;
                    break;
                case 2:
                    duration = 12f;
                    break;
                case 3:
                    duration = 18f;
                    break;
                case 4:
                    duration = 24f;
                    break;
            }

            var result = CombatService.CalculateAbilityResistance(creature, target.Object, SkillType.ForceAlter, ForceBalanceType.Universal);


            // Resisted - Only apply slow for six seconds
            if (result.IsResisted)
            {
                NWScript.ApplyEffectToObject(DurationType.Temporary, NWScript.EffectSlow(), target, 6.0f);
            }

            // Not resisted - Apply knockdown for the specified duration
            else
            {
                // Check lucky chance.
                var luck = PerkService.GetCreaturePerkLevel(creature, PerkType.Lucky);
                if (SWLOR.Game.Server.Service.Random.D100(1) <= luck)
                {
                    duration *= 2;
                    creature.SendMessage("Lucky Force Push!");
                }

                NWScript.ApplyEffectToObject(DurationType.Temporary, AbilityService.EffectKnockdown(target, duration), target, duration);
            }

            if (creature.IsPlayer)
            {
                SkillService.RegisterPCToAllCombatTargetsForSkill(creature.Object, SkillType.ForceAlter, target.Object);
            }
            
            NWScript.ApplyEffectToObject(DurationType.Instant, NWScript.EffectVisualEffect(VisualEffect.Vfx_Com_Blood_Spark_Small), target);
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

        public void OnConcentrationTick(NWCreature creature, NWObject target, int perkLevel, int tick)
        {
            
        }
    }
}
