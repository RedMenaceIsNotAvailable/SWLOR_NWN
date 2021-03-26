﻿using System.Collections.Generic;
using SWLOR.Game.Server.Enumeration;

namespace SWLOR.Game.Server.Service.SpaceService
{
    // Player-only delegates
    public delegate void ShipModuleEquippedDelegate(uint creature, ShipStatus shipStatus);
    public delegate void ShipModuleUnequippedDelegate(uint creature, ShipStatus shipStatus);

    // Player & NPC delegates
    public delegate float ShipModuleCalculateRecastDelegate(uint creature, ShipStatus shipStatus);
    public delegate int ShipModuleCalculateCapacitorDelegate(uint creature, ShipStatus shipStatus);
    public delegate void ShipModuleActivatedDelegate(uint activator, ShipStatus activatorShipStatus, uint target, ShipStatus targetShipStatus);

    public class ShipModuleDetail
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public bool IsPassive { get; set; }
        public bool RequiresTarget { get; set; }
        public ShipModulePowerType PowerType { get; set; }
        public Dictionary<PerkType, int> RequiredPerks { get; set; }
        public ShipModuleCalculateCapacitorDelegate CalculateCapacitorAction { get; set; }
        public ShipModuleCalculateRecastDelegate CalculateRecastAction { get; set; }
        public ShipModuleEquippedDelegate ModuleEquippedAction { get; set; }
        public ShipModuleUnequippedDelegate ModuleUnequippedAction { get; set; }
        public ShipModuleActivatedDelegate ModuleActivatedAction { get; set; }

        public ShipModuleDetail()
        {
            IsPassive = true;
            RequiredPerks = new Dictionary<PerkType, int>();
        }
    }
}
