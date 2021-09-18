﻿using SWLOR.Game.Server.Core;
using SWLOR.Game.Server.Core.Beamdog;

namespace SWLOR.Game.Server.Service.GuiService
{
    public class GuiGroup: GuiWidget
    {
        public GuiWidget Child { get; set; }
        public bool ShowBorder { get; set; }
        public NuiScrollbars Scrollbars { get; set; }

        public GuiGroup()
        {
            ShowBorder = true;
            Scrollbars = NuiScrollbars.Auto;
        }

        public override Json BuildElement()
        {
            var child = Child.ToJson();

            return Nui.Group(child, ShowBorder, Scrollbars);
        }
    }
}
