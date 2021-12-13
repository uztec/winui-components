using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace UzunTec.WinUI.Controls.Themes

{
    public class Palette
    {
        private readonly SortedList<ColorVariant, Lazy<Brush>> brushes;

        public Palette()
        {
            this.brushes = new SortedList<ColorVariant, Lazy<Brush>>
            {
                {ColorVariant.Primary, new Lazy<Brush>(() =>  new LinearGradientBrush(ThemeConstants.DefaultControlRect, PrimaryColorDark, PrimaryColorLight, LinearGradientMode.Vertical)) },
                {ColorVariant.Secondary, new Lazy<Brush>(() =>  new LinearGradientBrush(ThemeConstants.DefaultControlRect, SecondaryColorDark, SecondaryColorLight, LinearGradientMode.Vertical))},
                {ColorVariant.Info, new Lazy<Brush>(() =>  new LinearGradientBrush(ThemeConstants.DefaultControlRect, SuccessColorDark, SuccessColorLight, LinearGradientMode.Vertical))},
                {ColorVariant.Success, new Lazy<Brush>(() =>  new LinearGradientBrush(ThemeConstants.DefaultControlRect, PrimaryColorDark, ColorLight, LinearGradientMode.Vertical))},
                {ColorVariant.Warning, new Lazy<Brush>(() =>  new LinearGradientBrush(ThemeConstants.DefaultControlRect, PrimaryColorDark, PrimaryColorLight, LinearGradientMode.Vertical))},
                {ColorVariant.Danger, new Lazy<Brush>(() =>  new LinearGradientBrush(ThemeConstants.DefaultControlRect, PrimaryColorDark, PrimaryColorLight, LinearGradientMode.Vertical))},
                {ColorVariant.Light, new Lazy<Brush>(() =>  new LinearGradientBrush(ThemeConstants.DefaultControlRect, PrimaryColorDark, PrimaryColorLight, LinearGradientMode.Vertical))},
            };
        }

        public Color GetColor(ColorVariant variant, bool dark = true)
        {
            return Color.Red;   // TODO:
        }


        private static Brush GetSecondaryBrush()
        {
            throw new NotImplementedException();
        }

        public Color PrimaryColorDark { get; set; }
        public Color PrimaryColorLight { get; set; }
        public Color SecondaryColorDark { get; set; }
        public Color SecondaryColorLight { get; set; }
        public Color SuccessColorDark { get; set; }
        public Color SuccessColorLight { get; set; }
        public Color InfoColorDark { get; set; }
        public Color InfoColorLight { get; set; }
        public Color WarningColorDark { get; set; }
        public Color WarningColorLight { get; set; }
        public Color DangerColorDark { get; set; }
        public Color DangerColorLight { get; set; }
        public Color LightColor { get; set; }


    }
}
