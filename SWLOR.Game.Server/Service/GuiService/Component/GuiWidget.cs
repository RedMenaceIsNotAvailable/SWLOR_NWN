﻿using System;
using System.Collections.Generic;
using SWLOR.Game.Server.Core;
using SWLOR.Game.Server.Core.Beamdog;
using static SWLOR.Game.Server.Core.NWScript.NWScript;

namespace SWLOR.Game.Server.Service.GuiService.Component
{
    public abstract class GuiWidget
    {
        private string Id { get; set; }
        private float Width { get; set; }
        private float Height { get; set; }
        private float AspectRatio { get; set; }
        private float Margin { get; set; }
        private float Padding { get; set; }
        private List<GuiDrawList> DrawLists { get; set; }

        private bool IsEnabled { get; set; }
        private string IsEnabledBindName { get; set; }
        private bool IsEnableBound => !string.IsNullOrWhiteSpace(IsEnabledBindName);
        
        private bool IsVisible { get; set; }
        private string IsVisibleBindName { get; set; }
        private bool IsVisibleBound => !string.IsNullOrWhiteSpace(IsVisibleBindName);
        
        private string Tooltip { get; set; }
        private string TooltipBindName { get; set; }
        private bool IsTooltipBound => !string.IsNullOrWhiteSpace(TooltipBindName);
        
        private GuiColor? Color { get; set; }
        private string ColorBindName { get; set; }
        private bool IsColorBound => !string.IsNullOrWhiteSpace(ColorBindName);

        public abstract Json BuildElement();

        public GuiWidget SetId(string id)
        {
            Id = id;
            return this;
        }

        public GuiWidget SetWidth(float width)
        {
            Width = width;
            return this;
        }

        public GuiWidget SetHeight(float height)
        {
            Height = height;
            return this;
        }

        public GuiWidget SetAspectRatio(float aspectRatio)
        {
            AspectRatio = aspectRatio;
            return this;
        }

        public GuiWidget SetMargin(float margin)
        {
            Margin = margin;
            return this;
        }

        public GuiWidget SetPadding(float padding)
        {
            Padding = padding;
            return this;
        }

        public GuiWidget SetIsEnabled(bool isEnabled)
        {
            IsEnabled = isEnabled;
            return this;
        }

        public GuiWidget BindIsEnabled(string bindName)
        {
            IsEnabledBindName = bindName;
            return this;
        }


        public GuiWidget SetIsVisible(bool isVisible)
        {
            IsVisible = isVisible;
            return this;
        }

        public GuiWidget BindIsVisible(string bindName)
        {
            IsVisibleBindName = bindName;
            return this;
        }

        public GuiWidget SetTooltip(string tooltip)
        {
            Tooltip = tooltip;
            return this;
        }

        public GuiWidget BindTooltip(string bindName)
        {
            TooltipBindName = bindName;
            return this;
        }

        public GuiWidget SetColor(GuiColor color)
        {
            Color = color;
            return this;
        }

        public GuiWidget SetColor(int red, int green, int blue, int alpha = 255)
        {
            Color = new GuiColor(red, green, blue, alpha);
            return this;
        }

        public GuiWidget BindColor(string bindName)
        {
            ColorBindName = bindName;
            return this;
        }

        public GuiWidget AddDrawList(Action<GuiDrawList> drawList)
        {
            var newDrawList = new GuiDrawList(this);
            DrawLists.Add(newDrawList);
            drawList(newDrawList);
            return this;
        }

        protected GuiWidget()
        {
            IsEnabled = true;
            IsVisible = true;
            DrawLists = new List<GuiDrawList>();
        }

        public virtual Json ToJson()
        {
            var element = BuildElement();

            // Nui Id
            if (!string.IsNullOrWhiteSpace(Id))
            {
                element = Nui.Id(element, Id);
            }

            // Width
            if (Width > 0f)
            {
                element = Nui.Width(element, Width);
            }

            // Height
            if (Height > 0f)
            {
                element = Nui.Height(element, Height);
            }

            // Aspect Ratio
            if (AspectRatio > 0f)
            {
                element = Nui.Aspect(element, AspectRatio);
            }

            // Is Enabled (Can be bound)
            if (IsEnableBound)
            {
                var binding = Nui.Bind(IsEnabledBindName);
                element = Nui.Enabled(element, binding);
            }
            else
            {
                element = Nui.Enabled(element, JsonBool(IsEnabled));
            }

            // Is Visible (Can be bound)
            if (IsVisibleBound)
            {
                var binding = Nui.Bind(IsVisibleBindName);
                element = Nui.Visible(element, binding);
            }
            else
            {
                element = Nui.Visible(element, JsonBool(IsVisible));
            }

            // Margin
            if (Margin > 0f)
            {
                element = Nui.Margin(element, Margin);
            }

            // Padding
            if (Padding > 0f)
            {
                element = Nui.Padding(element, Padding);
            }

            // Tooltip (Can be bound)
            if (IsTooltipBound)
            {
                var binding = Nui.Bind(TooltipBindName);
                element = Nui.Tooltip(element, binding);
            }
            else if(!string.IsNullOrWhiteSpace(Tooltip))
            {
                element = Nui.Tooltip(element, JsonString(Tooltip));
            }

            // Color
            if (IsColorBound)
            {
                var binding = Nui.Bind(ColorBindName);
                element = Nui.StyleForegroundColor(element, binding);
            }
            else if (Color != null)
            {
                element = Nui.StyleForegroundColor(element, Color.ToJson());
            }

            return element;
        }
    }
}
