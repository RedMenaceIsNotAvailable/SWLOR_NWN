﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWLOR.Game.Server.Service.AnimationService
{
    /// <summary>
    /// Subset of VisualEffect
    /// </summary>
    public class AnimationEvent
    {
        private AnimationEvent(string value) { Value = value; }

        public string Value { get; set; }

        public string IdKey { get { return $"{Value}_id"; } }
        public string DurationKey { get { return $"{Value}_dur"; } }

        public static AnimationEvent CreatureOnAttacked { get { return new AnimationEvent("crea_attacked_vfx"); } }
        public static AnimationEvent CreatureOnPerceive { get { return new AnimationEvent("crea_perception_vfx"); } }
        public static AnimationEvent CreatureOnRoundEnd { get { return new AnimationEvent("crea_roundend_vfx"); } }
        public static AnimationEvent CreatureOnDamaged { get { return new AnimationEvent("crea_damaged_vfx"); } }
        public static AnimationEvent CreatureOnDeath { get { return new AnimationEvent("crea_death_vfx");  } }
    }
}
