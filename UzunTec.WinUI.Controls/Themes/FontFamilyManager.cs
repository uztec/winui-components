using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace UzunTec.WinUI.Controls.Themes
{
    internal class FontFamilyManager
    {
        private readonly PrivateFontCollection fontCollection;
        private readonly SortedList<string, FontFamily> fontList;
        private readonly SortedList<string, byte[]> fontListTemp;

        public FontFamilyManager()
        {
            this.fontCollection = new PrivateFontCollection();
            this.fontList = new SortedList<string, FontFamily>();
            this.fontListTemp = new SortedList<string, byte[]>();
            this.InstallFont("Segoe MDL2", Properties.Resources.SegMDL2);
            this.InstallFont("Roboto", Properties.Resources.Roboto_Regular);
            this.InstallFont("Roboto Medium", Properties.Resources.Roboto_Medium);
            this.InstallFont("Roboto Light", Properties.Resources.Roboto_Light);
            this.InstallFont("Roboto Thin", Properties.Resources.Roboto_Thin);
            this.InstallFont("Segoe UI", Properties.Resources.segoeui);
            this.InstallFont("Segoe UI Light", Properties.Resources.segoeuil);
            this.InstallFont("Segoe UI SemiLight", Properties.Resources.segoeuisl);
            this.InstallFont("Segoe UI SemiBold", Properties.Resources.seguisb);
            this.ProcessFonts();
        }

        internal FontFamily GetFamily(string familyName)
        {
            if (!this.fontList.TryGetValue(familyName.ToLower(), out FontFamily family))
            {
                family = new Font(familyName, 10).FontFamily;
            }
            return family;
        }

        private void InstallFont(string fontFamily, byte[] resource)
        {
            this.fontListTemp.Add(fontFamily, resource);
        }

        private void ProcessFonts()
        {
            foreach (string fontFamily in this.fontListTemp.Keys)
            {
                byte[] resource = this.fontListTemp[fontFamily];
                int length = resource.Length;
                IntPtr data = Marshal.AllocCoTaskMem(length);
                Marshal.Copy(resource, 0, data, length);
                this.fontCollection.AddMemoryFont(data, length);
                this.fontList.Add(fontFamily.ToLower(), this.fontCollection.Families[this.fontCollection.Families.Length - 1]);
            }
        }
    }
}
