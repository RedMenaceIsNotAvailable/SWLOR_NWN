﻿using SWLOR.Game.Server.Core.Beamdog;
using SWLOR.Game.Server.Feature.GuiDefinition.ViewModel;
using SWLOR.Game.Server.Service.GuiService;

namespace SWLOR.Game.Server.Feature.GuiDefinition
{
    public class MarketBuyDefinition : IGuiWindowDefinition
    {
        private readonly GuiWindowBuilder<MarketBuyViewModel> _builder = new();

        public GuiConstructedWindow BuildWindow()
        {
            _builder.CreateWindow(GuiWindowType.MarketBuying)
                .BindOnOpened(model => model.OnLoadWindow())
                .SetIsResizable(true)
                .SetInitialGeometry(0, 0, 545f, 295.5f)
                .SetTitle("Market Shop")

                .AddColumn(col =>
                {
                    col.AddRow(row =>
                    {
                        row.AddTextEdit()
                            .SetPlaceholder("Search")
                            .BindValue(model => model.SearchText);

                        row.AddButton()
                            .SetText("X")
                            .SetHeight(35f)
                            .SetWidth(35f)
                            .BindOnClicked(model => model.OnClickClearSearch());

                        row.AddButton()
                            .SetText("Search")
                            .SetHeight(35f)
                            .BindOnClicked(model => model.OnClickSearch());
                    });

                    col.AddRow(row =>
                    {
                        row.AddColumn(col2 =>
                        {
                            col2.AddRow(row2 =>
                            {
                                row2.AddLabel()
                                    .SetText("Weapon")
                                    .SetHorizontalAlign(NuiHorizontalAlign.Left)
                                    .SetVerticalAlign(NuiVerticalAlign.Top)
                                    .SetHeight(26f);
                            });

                            col2.AddRow(row2 =>
                            {
                                row2.AddList(template =>
                                {
                                    template.AddCell(cell =>
                                    {
                                        cell.AddToggleButton()
                                            .BindText(model => model.WeaponCategoryNames);
                                    });
                                })
                                    .BindRowCount(model => model.WeaponCategoryNames)
                                    .SetHeight(140f);
                            });

                            col2.AddRow(row2 =>
                            {
                                row2.AddLabel()
                                    .SetText("Armor")
                                    .SetHorizontalAlign(NuiHorizontalAlign.Left)
                                    .SetVerticalAlign(NuiVerticalAlign.Top)
                                    .SetHeight(26f);
                            });

                            col2.AddRow(row2 =>
                            {
                                row2.AddList(template =>
                                {
                                    template.AddCell(cell =>
                                    {
                                        cell.AddToggleButton()
                                            .BindText(model => model.ArmorCategoryNames);
                                    });
                                })
                                    .BindRowCount(model => model.ArmorCategoryNames)
                                    .SetHeight(140f);
                            });

                            col2.AddRow(row2 =>
                            {
                                row2.AddLabel()
                                    .SetText("Other")
                                    .SetHorizontalAlign(NuiHorizontalAlign.Left)
                                    .SetVerticalAlign(NuiVerticalAlign.Top)
                                    .SetHeight(26f);
                            });

                            col2.AddRow(row2 =>
                            {
                                row2.AddList(template =>
                                {
                                    template.AddCell(cell =>
                                    {
                                        cell.AddToggleButton()
                                            .BindText(model => model.OtherCategoryNames);
                                    });
                                })
                                    .BindRowCount(model => model.OtherCategoryNames)
                                    .SetHeight(140f);
                            });
                        })
                            .SetWidth(150f);

                        row.AddColumn(col2 =>
                        {
                            col2.AddRow(row2 =>
                            {
                                row2.AddList(template =>
                                {

                                    template.AddCell(cell =>
                                    {
                                        cell.SetWidth(40f);
                                        cell.SetIsVariable(false);

                                        cell.AddGroup(group =>
                                        {
                                            group.AddImage()
                                                .BindResref(model => model.ItemIconResrefs)
                                                .SetHorizontalAlign(NuiHorizontalAlign.Center)
                                                .SetVerticalAlign(NuiVerticalAlign.Top)
                                                .BindTooltip(model => model.ItemNames);
                                        });
                                    });

                                    template.AddCell(cell =>
                                    {
                                        cell.AddText()
                                            .BindText(model => model.ItemNames)
                                            .BindTooltip(model => model.ItemNames);
                                    });

                                    template.AddCell(cell =>
                                    {
                                        cell.SetWidth(80f);

                                        cell.AddLabel()
                                            .BindText(model => model.ItemMarkets)
                                            .SetHorizontalAlign(NuiHorizontalAlign.Left)
                                            .BindTooltip(model => model.ItemMarkets);
                                    });

                                    template.AddCell(cell =>
                                    {
                                        cell.SetIsVariable(false);
                                        cell.SetWidth(120f);

                                        cell.AddLabel()
                                            .BindText(model => model.ItemPriceNames);
                                    });

                                    template.AddCell(cell =>
                                    {
                                        cell.AddLabel()
                                            .BindText(model => model.ItemSellerNames)
                                            .BindTooltip(model => model.ItemSellerNames);
                                    });

                                    template.AddCell(cell =>
                                    {
                                        cell.AddButton()
                                            .SetText("Examine")
                                            .BindOnClicked(model => model.OnClickExamine());
                                    });

                                    template.AddCell(cell =>
                                    {
                                        cell.AddButton()
                                            .SetText("Buy")
                                            .BindOnClicked(model => model.OnClickBuy());
                                    });
                                })
                                    .BindRowCount(model => model.ItemMarkets)
                                    .SetRowHeight(40f);
                            });

                            col2.AddRow(row2 =>
                            {
                                row2.AddSpacer();
                                row2.AddButton()
                                    .SetText("<")
                                    .SetWidth(32f)
                                    .SetHeight(35f)
                                    .BindOnClicked(model => model.OnClickPreviousPage());

                                row2.AddComboBox()
                                    .BindOptions(model => model.PageNumbers)
                                    .BindSelectedIndex(model => model.SelectedPage);

                                row2.AddButton()
                                    .SetText(">")
                                    .SetWidth(32f)
                                    .SetHeight(35f)
                                    .BindOnClicked(model => model.OnClickNextPage());

                                row2.AddSpacer();
                            });
                        });
                    });


                });


            return _builder.Build();
        }
    }
}
