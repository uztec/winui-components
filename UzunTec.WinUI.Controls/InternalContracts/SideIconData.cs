using System.Drawing;

namespace UzunTec.WinUI.Controls.InternalContracts
{
    internal class SideIconData
    {
        public Image image;
        public Image imageHovered;
        public RectangleF rect;
        public bool hovered;
        public bool visible;
        public Image GetImage()
        {
            return (imageHovered == null || !hovered) ? image : imageHovered;
        }
    }
}
