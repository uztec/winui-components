using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace UzunTec.WinUI.Controls
{
    internal class FontFamilyManager
    {
        private readonly PrivateFontCollection fontCollection;
        private readonly SortedList<string, FontFamily> fontList;

        public FontFamilyManager()
        {
            this.fontCollection = new PrivateFontCollection();
            this.fontList = new SortedList<string, FontFamily>();
            this.InstallFont("segoe mdl2", Properties.Resources.SegMDL2);
            this.InstallFont("roboto thin", Properties.Resources.Roboto_Thin);
            this.InstallFont("segoe ui", Properties.Resources.segoeui);

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
            int length = resource.Length;
            IntPtr data = Marshal.AllocCoTaskMem(length);
            Marshal.Copy(resource, 0, data, length);
            this.fontCollection.AddMemoryFont(data, length);
            this.fontList.Add(fontFamily.ToLower(), this.fontCollection.Families[this.fontCollection.Families.Length - 1]);
        }
    }
}
