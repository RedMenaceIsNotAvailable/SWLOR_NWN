﻿using SWLOR.Game.Server.Core;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Legacy.GameObject;
using SWLOR.Game.Server.Legacy.Item.Contracts;
using SWLOR.Game.Server.Legacy.Service;
using SWLOR.Game.Server.Legacy.ValueObject;
using static SWLOR.Game.Server.Core.NWScript.NWScript;

namespace SWLOR.Game.Server.Legacy.Item
{
    public class SlugShake: IActionItem
    {
        public string CustomKey => null;

        public CustomData StartUseItem(NWCreature user, NWItem item, NWObject target, Location targetLocation)
        {
            return null;
        }

        public void ApplyEffects(NWCreature user, NWItem item, NWObject target, Location targetLocation, CustomData customData)
        {
            var hp = user.MaxHP;

            // Restores HP to max
            ApplyEffectToObject(DurationType.Instant, EffectHeal(hp), user);

            // But reduces one random attribute by 50 for 2 minutes.
            var stat = SWLOR.Game.Server.Service.Random.D6(1)-1;
            var effect = EffectAbilityDecrease(stat, 50);
            ApplyEffectToObject(DurationType.Temporary, effect, user, 120f);
        }

        public float Seconds(NWCreature user, NWItem item, NWObject target, Location targetLocation, CustomData customData)
        {
            return 1f;
        }

        public bool FaceTarget()
        {
            return false;
        }

        public Animation AnimationID()
        {
            return 0;
        }

        public float MaxDistance(NWCreature user, NWItem item, NWObject target, Location targetLocation)
        {
            return 0;
        }

        public bool ReducesItemCharge(NWCreature user, NWItem item, NWObject target, Location targetLocation, CustomData customData)
        {
            return true;
        }

        public string IsValidTarget(NWCreature user, NWItem item, NWObject target, Location targetLocation)
        {
            return null;
        }

        public bool AllowLocationTarget()
        {
            return false;
        }
    }
}
