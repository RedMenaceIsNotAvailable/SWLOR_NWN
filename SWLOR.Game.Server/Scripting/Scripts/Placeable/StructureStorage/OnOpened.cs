﻿using System;
using NWN;
using SWLOR.Game.Server.Data.Entity;
using SWLOR.Game.Server.GameObject;
using SWLOR.Game.Server.Scripting.Contracts;
using SWLOR.Game.Server.Service;

namespace SWLOR.Game.Server.Scripting.Scripts.Placeable.StructureStorage
{
    public class OnOpened : IScript
    {
        public void SubscribeEvents()
        {
        }

        public void UnsubscribeEvents()
        {
        }

        public void Main()
        {
            NWPlaceable chest = (NWGameObject.OBJECT_SELF);
            Guid structureID = new Guid(chest.GetLocalString("PC_BASE_STRUCTURE_ID"));
            var structure = DataService.Single<PCBaseStructure>(x => x.ID == structureID);

            var items = DataService.Where<PCBaseStructureItem>(x => x.PCBaseStructureID == structure.ID);
            foreach (var item in items)
            {
                SerializationService.DeserializeItem(item.ItemObject, chest);
            }

            chest.IsUseable = false;
        }
    }
}
