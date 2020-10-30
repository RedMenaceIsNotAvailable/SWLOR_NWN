﻿using System.Linq;
using SWLOR.Game.Server.Core;
using SWLOR.Game.Server.Core.NWScript;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.GameObject;
using SWLOR.Game.Server.Item.Contracts;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Service.Legacy;
using SWLOR.Game.Server.ValueObject;

namespace SWLOR.Game.Server.Item
{
    public class MineralScanner : IActionItem
    {
        public string CustomKey => null;

        public CustomData StartUseItem(NWCreature user, NWItem item, NWObject target, Location targetLocation)
        {
            return null;
        }

        public void ApplyEffects(NWCreature user, NWItem item, NWObject target, Location targetLocation, CustomData customData)
        {
            var lootTableID = GetLootTable(targetLocation);
            if (lootTableID <= 0) return;

            NWArea area = NWScript.GetAreaFromLocation(targetLocation);
            var items = DataService.LootTableItem.GetAllByLootTableID(lootTableID)
                .OrderByDescending(o => o.Weight);
            var sector = BaseService.GetSectorOfLocation(targetLocation);
            var sectorName = "Unknown";

            switch (sector)
            {
                case "NW": sectorName = "Northwest"; break;
                case "NE": sectorName = "Northeast"; break;
                case "SW": sectorName = "Southwest"; break;
                case "SE": sectorName = "Southeast"; break;
            }

            user.SendMessage(area.Name + "(" + sectorName + ")");
            user.SendMessage("Scanning results: ");

            foreach (var lti in items)
            {
                var name = ItemService.GetNameByResref(lti.Resref);
                user.SendMessage(name + " [Density: " + lti.Weight + "]");
            }
            
            DurabilityService.RunItemDecay(user.Object, item, RandomService.RandomFloat(0.02f, 0.08f));
        }
        
        public float Seconds(NWCreature user, NWItem item, NWObject target, Location targetLocation, CustomData customData)
        {
            const float BaseScanningTime = 16.0f;
            var scanningTime = BaseScanningTime;

            if (user.IsPlayer)
            {
                var player = (user.Object);
                scanningTime = BaseScanningTime - BaseScanningTime * (PerkService.GetCreaturePerkLevel(player, PerkType.SpeedyResourceScanner) * 0.1f);

            }
            return scanningTime;
        }

        public bool FaceTarget()
        {
            return true;
        }

        public Animation AnimationID()
        {
            return Animation.LoopingGetMid;
        }

        public float MaxDistance(NWCreature user, NWItem item, NWObject target, Location targetLocation)
        {
            return 5.0f;
        }

        public bool ReducesItemCharge(NWCreature user, NWItem item, NWObject target, Location targetLocation, CustomData customData)
        {
            return false;
        }

        private int GetLootTable(Location targetLocation)
        {
            NWArea area = NWScript.GetAreaFromLocation(targetLocation);
            var dbArea = DataService.Area.GetByResref(area.Resref);
            var sector = BaseService.GetSectorOfLocation(targetLocation);
            var lootTableID = 0;

            switch (sector)
            {
                case "NW":
                    lootTableID = dbArea.NorthwestLootTableID ?? 0;
                    break;
                case "NE":
                    lootTableID = dbArea.NortheastLootTableID ?? 0;
                    break;
                case "SW":
                    lootTableID = dbArea.SouthwestLootTableID ?? 0;
                    break;
                case "SE":
                    lootTableID = dbArea.SoutheastLootTableID ?? 0;
                    break;
            }

            return lootTableID;
        }

        public string IsValidTarget(NWCreature user, NWItem item, NWObject target, Location targetLocation)
        {
            var lootTableID = GetLootTable(targetLocation);
            if (lootTableID <= 0) return "That location cannot be scanned.";
            
            return null;
        }

        public bool AllowLocationTarget()
        {
            return true;
        }
    }
}
