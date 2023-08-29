namespace UzunTec.WinUI.Utils
{
    public static class ControlExtensions
    {
        public static Color GetParentColor(this Control ctrl)
        {
            Control parent = ctrl.Parent;
            while (parent.BackColor.A == 0 && parent.Parent != null)
            {
                parent = parent.Parent;
            }
            return parent.BackColor;
        }
    }
}
