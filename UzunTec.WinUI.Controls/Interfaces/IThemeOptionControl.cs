namespace UzunTec.WinUI.Controls.Interfaces
{
    public interface IThemeOptionControl : IThemeControlWithTextBackground
    {
        bool Checked { get; set; }
        object Value { get; set; }
    }
}