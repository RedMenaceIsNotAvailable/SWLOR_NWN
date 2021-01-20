﻿//using Random = SWLOR.Game.Server.Service.Random;
using System.Collections.Generic;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Core.NWScript.Enum.VisualEffect;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.Service;
using SWLOR.Game.Server.Service.AbilityService;
using static SWLOR.Game.Server.Core.NWScript.NWScript;

namespace SWLOR.Game.Server.Feature.AbilityDefinition
{
    public class ForceDrainAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<Feat, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            ForceDrain1(builder);
            ForceDrain2(builder);
            ForceDrain3(builder);
            ForceDrain4(builder);
            ForceDrain5(builder);

            return builder.Build();
        }

        private static void ForceDrain1(AbilityBuilder builder)
        {
            builder.Create(Feat.ForceLeap1, PerkType.ForceDrain)
                .Name("Force Drain 1")
                .HasRecastDelay(RecastGroup.ForceDrain, 60f)
                .HasActivationDelay(2.0f)
                .RequirementFP(2)
                .IsConcentrationAbility(StatusEffectType.ForceDrain1)
                .DisplaysVisualEffectWhenActivating();
        }

        private static void ForceDrain2(AbilityBuilder builder)
        {
            builder.Create(Feat.ForceLeap1, PerkType.ForceDrain)
                .Name("Force Drain II")
                .HasRecastDelay(RecastGroup.ForceDrain, 60f)
                .HasActivationDelay(2.0f)
                .RequirementFP(3)
                .IsConcentrationAbility(StatusEffectType.ForceDrain2)
                .DisplaysVisualEffectWhenActivating();
        }

        private static void ForceDrain3(AbilityBuilder builder)
        {
            builder.Create(Feat.ForceLeap1, PerkType.ForceDrain)
                .Name("Force Drain III")
                .HasRecastDelay(RecastGroup.ForceDrain, 60f)
                .HasActivationDelay(2.0f)
                .RequirementFP(4)
                .IsConcentrationAbility(StatusEffectType.ForceDrain3)
                .DisplaysVisualEffectWhenActivating();
        }

        private static void ForceDrain4(AbilityBuilder builder)
        {
            builder.Create(Feat.ForceLeap1, PerkType.ForceDrain)
                .Name("Force Drain IV")
                .HasRecastDelay(RecastGroup.ForceDrain, 60f)
                .HasActivationDelay(4.0f)
                .RequirementFP(5)
                .IsConcentrationAbility(StatusEffectType.ForceDrain4)
                .DisplaysVisualEffectWhenActivating();
        }

        private static void ForceDrain5(AbilityBuilder builder)
        {
            builder.Create(Feat.ForceLeap1, PerkType.ForceDrain)
                .Name("Force Drain V")
                .HasRecastDelay(RecastGroup.ForceDrain, 60f)
                .HasActivationDelay(4.0f)
                .RequirementFP(6)
                .IsConcentrationAbility(StatusEffectType.ForceDrain5)
                .DisplaysVisualEffectWhenActivating();
        }
    }
}