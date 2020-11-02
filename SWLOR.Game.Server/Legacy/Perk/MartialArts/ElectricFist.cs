﻿using SWLOR.Game.Server.Core.NWScript;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Core.NWScript.Enum.VisualEffect;
using SWLOR.Game.Server.Legacy.GameObject;
using SWLOR.Game.Server.Legacy.Service;
using PerkType = SWLOR.Game.Server.Legacy.Enumeration.PerkType;

namespace SWLOR.Game.Server.Legacy.Perk.MartialArts
{
    public class ElectricFist: IPerkHandler
    {
        public PerkType PerkType => PerkType.ElectricFist;

        public string CanCastSpell(NWCreature oPC, NWObject oTarget, int spellTier)
        {
            if (!oPC.RightHand.IsValid && !oPC.LeftHand.IsValid)
                return string.Empty;

            return "Must be equipped with a power glove in order to use that ability.";
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
            return baseCooldownTime;
        }

        public int? CooldownCategoryID(NWCreature creature, int? baseCooldownCategoryID, int spellTier)
        {
            return baseCooldownCategoryID;
        }

        public void OnImpact(NWCreature creature, NWObject target, int perkLevel, int spellTier)
        {
            int damage;
            float duration;

            switch (perkLevel)
            {
                case 1:
                    damage = SWLOR.Game.Server.Service.Random.D8(1);
                    duration = 3;
                    break;
                case 2:
                    damage = SWLOR.Game.Server.Service.Random.D8(2);
                    duration = 3;
                    break;
                case 3:
                    damage = SWLOR.Game.Server.Service.Random.D8(3);
                    duration = 3;
                    break;
                case 4:
                    damage = SWLOR.Game.Server.Service.Random.D8(3);
                    duration = 6;
                    break;
                case 5:
                    damage = SWLOR.Game.Server.Service.Random.D8(4);
                    duration = 6;
                    break;
                case 6:
                    damage = SWLOR.Game.Server.Service.Random.D8(5);
                    duration = 6;
                    break;
                case 7:
                    damage = SWLOR.Game.Server.Service.Random.D8(6);
                    duration = 6;
                    break;
                case 8:
                    damage = SWLOR.Game.Server.Service.Random.D8(7);
                    duration = 6;
                    break;
                case 9:
                    damage = SWLOR.Game.Server.Service.Random.D8(7);
                    duration = 9;
                    break;
                case 10:
                    damage = SWLOR.Game.Server.Service.Random.D8(8);
                    duration = 9;
                    break;
                default: return;
            }
            
            NWScript.ApplyEffectToObject(DurationType.Temporary, NWScript.EffectStunned(), target, duration);
            NWScript.ApplyEffectToObject(DurationType.Instant, NWScript.EffectDamage(damage, DamageType.Electrical), target);
            NWScript.ApplyEffectToObject(DurationType.Instant, NWScript.EffectVisualEffect(VisualEffect.Vfx_Imp_Sunstrike), target);
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
