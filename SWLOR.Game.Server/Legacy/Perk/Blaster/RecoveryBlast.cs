﻿using System.Linq;
using SWLOR.Game.Server.Core.NWScript;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Core.NWScript.Enum.VisualEffect;
using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.GameObject;
using SWLOR.Game.Server.Legacy.Service;
using PerkType = SWLOR.Game.Server.Legacy.Enumeration.PerkType;

namespace SWLOR.Game.Server.Legacy.Perk.Blaster
{
    public class RecoveryBlast: IPerkHandler
    {
        public PerkType PerkType => PerkType.RecoveryBlast;

        public string CanCastSpell(NWCreature oPC, NWObject oTarget, int spellTier)
        {
            if (oPC.RightHand.CustomItemType != CustomItemType.BlasterRifle)
                return "Must be equipped with a blaster rifle to use that ability.";

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
            return baseCooldownTime;
        }

        public int? CooldownCategoryID(NWCreature creature, int? baseCooldownCategoryID, int spellTier)
        {
            return baseCooldownCategoryID;
        }

        public void OnImpact(NWCreature creature, NWObject target, int perkLevel, int spellTier)
        {
            // Mark the player as performing a recovery blast.
            // This is later picked up in the OnApplyDamage event to reduce all damage to 0.
            creature.SetLocalInt("RECOVERY_BLAST_ACTIVE", 1);

            var members = creature.PartyMembers.Where(x => NWScript.GetDistanceBetween(x, target) <= 10.0f);
            var luck = PerkService.GetCreaturePerkLevel(creature, PerkType.Lucky);

            foreach (var member in members)
            {
                HealTarget(member, perkLevel, luck);
            }
        }

        private void HealTarget(NWCreature member, int level, int luck)
        {
            int amount;
            
            switch (level)
            {
                case 1:
                    amount = SWLOR.Game.Server.Service.Random.D12(1);
                    break;
                case 2:
                    amount = SWLOR.Game.Server.Service.Random.D8(2);
                    break;
                case 3:
                    amount = SWLOR.Game.Server.Service.Random.D8(3);
                    break;
                case 4:
                    amount = SWLOR.Game.Server.Service.Random.D8(4);
                    break;
                case 5:
                    amount = SWLOR.Game.Server.Service.Random.D8(5);
                    break;
                case 6:
                    amount = SWLOR.Game.Server.Service.Random.D8(6);
                    break;
                default: return;
            }

            if (SWLOR.Game.Server.Service.Random.D100(1) <= luck)
            {
                amount *= 2;
            }

            NWScript.ApplyEffectToObject(DurationType.Instant, NWScript.EffectHeal(amount), member);
            NWScript.ApplyEffectToObject(DurationType.Instant, NWScript.EffectVisualEffect(VisualEffect.Vfx_Imp_Healing_S), member);
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
