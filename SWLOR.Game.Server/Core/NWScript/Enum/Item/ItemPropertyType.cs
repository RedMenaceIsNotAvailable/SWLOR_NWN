namespace SWLOR.Game.Server.Core.NWScript.Enum.Item
{
    public enum ItemPropertyType
    {
        Invalid = -1,
        AbilityBonus = 0,
        ACBonus = 1,
        ACBonusVsAlignmentGroup = 2,
        ACBonusVsDamageType = 3,
        ACBonusVsRacialGroup = 4,
        ACBonusVsSpecificAlignment = 5,
        EnhancementBonus = 6,
        EnhancementBonusVsAlignmentGroup = 7,
        EnhancementBonusVsRacialGroup = 8,
        EnhancementBonusVsSpecificAlignement = 9,
        DecreasedEnhancementModifier = 10,
        BaseItemWeightReduction = 11,
        BonusFeat = 12,
        BonusSpellSlotOfLevelN = 13,
        CastSpell = 15,
        DamageBonus = 16,
        DamageBonusVsAlignmentGroup = 17,
        DamageBonusVsRacialGroup = 18,
        DamageBonusVsSpecificAlignment = 19,
        ImmunityDamageType = 20,
        DecreasedDamage = 21,
        DamageReduction = 22,
        DamageResistance = 23,
        DamageVulnerability = 24,
        Darkvision = 26,
        DecreasedAbilityScore = 27,
        DecreasedAC = 28,
        DecreasedSkillModifier = 29,
        EnhancedContainerReducedWeight = 32,
        ExtraMeleeDamageType = 33,
        ExtraRangedDamageType = 34,
        Haste = 35,
        HolyAvenger = 36,
        ImmunityMiscellaneous = 37,
        ImprovedEvasion = 38,
        SpellResistance = 39,
        SavingThrowBonus = 40,
        SavingThrowBonusSpecific = 41,
        Keen = 43,
        Light = 44,
        Mighty = 45,
        MindBlank = 46,
        NoDamage = 47,
        OnHitProperties = 48,
        DecreasedSavingThrows = 49,
        DecreasedSavingThrowsSpecific = 50,
        Regeneration = 51,
        SkillBonus = 52,
        ImmunitySpecificSpell = 53,
        ImmunitySpellSchool = 54,
        ThievesTools = 55,
        AttackBonus = 56,
        AttackBonusVsAlignmentGroup = 57,
        AttackBonusVsRacialGroup = 58,
        AttackBonusVsSpecificAlignment = 59,
        DecreasedAttackModifier = 60,
        UnlimitedAmmunition = 61,
        UseLimitationAlignmentGroup = 62,
        UseLimitationClass = 63,
        UseLimitationRacialType = 64,
        UseLimitationSpecificAlignment = 65,
        UseLimitationTileset = 66,
        RegenerationVampiric = 67,
        Trap = 70,
        TrueSeeing = 71,
        OnMonsterHit = 72,
        TurnResistance = 73,
        MassiveCriticals = 74,
        FreedomOfMovement = 75,
        Poison = 76, // no longer working, poison is now a onHit  subtype
        MonsterDamage = 77,
        ImmunitySpellsByLevel = 78,
        SpecialWalk = 79,
        HealersKit = 80,
        WeightIncrease = 81,
        OnHitCastSpell = 82,
        Visualeffect = 83,
        ArcaneSpellFailure = 84,
        Material = 85,
        Quality = 86,
        Additional = 87,

        // Custom Item Properties follow
        Control = 88,
        Craftsmanship = 89,
        HPBonus = 90,
        FPBonus = 91,
        STMBonus = 92,
        DMG = 93,
        Defense = 94,
        ProgressPenalty = 95,
        NPCHP = 96,
        NPCEP = 97,
        NPCSTM = 98,
        NPCLevel = 99,
        UseLimitationPerk = 100,
        ArmorEnhancement = 101,
        WeaponEnhancement = 102,
        PrimaryStat = 103,
        EnhancementLevel = 104,
        StructureBonus = 105,
        FoodBonus = 106,
        StructureEnhancement = 107,
        FoodEnhancement = 108,

        Evasion = 117,
        AbilityRecastReduction = 118,
    }
}