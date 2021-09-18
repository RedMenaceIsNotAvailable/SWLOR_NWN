﻿using System.Collections.Generic;
using SWLOR.Game.Server.Service.GuiService;
using SWLOR.Game.Server.Service.GuiService.Component;

namespace SWLOR.Game.Server.Feature.GuiDefinition
{
    public class TestWindowGuiDefinition: IGuiWindowListDefinition
    {
        private readonly GuiWindowBuilder _builder = new GuiWindowBuilder();

        public Dictionary<GuiWindowType, GuiWindow> BuildWindows()
        {
            _builder.CreateWindow(GuiWindowType.TestWindow)

                .AddColumn(col =>
                {
                    col.AddRow(row =>
                    {
                        row.AddButton()
                            .SetText("regular button");

                        row.AddButtonImage()
                            .SetResref("ife_animal");

                        row.AddToggleButton()
                            .SetText("toggle button");
                    });
                    col.AddRow(row =>
                    {
                        row.AddComboBox()
                            .BindSelectedIndex("selected_option")
                            .AddOption("option 1", 1)
                            .AddOption("option 2", 2)
                            .AddOption("option 3", 3);

                        row.AddColorPicker();

                    });
                });
                

            return _builder.Build();
        }
    }
}
