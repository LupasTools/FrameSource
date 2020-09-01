// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// IFrameData.cs : 30.8.2020
// MIT license

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Lupas.FrameSource
{
    public interface IFrameData
    {
        int W { get; }
        int H { get; }
        int Stride { get; }
        Bitmap ToBitmap();
        void ResizeIfRequired(int w, int h, int stride);
        void CopyFrom(IntPtr src, int srcH, int srcStride);
        dynamic DynamicData { get; }
    }

    internal static class FrameDataUtilities
    {
        internal static int Channels(this IFrameData fd)
        {
            return fd.W == 0 ? 0 : Math.Abs(fd.Stride) / fd.W;
        }
        internal static PixelFormat Format(IFrameData fd)
        {
            switch (fd.Channels())
            {
                case 3: return PixelFormat.Format24bppRgb;
                case 4: return PixelFormat.Format32bppArgb;
                default: return PixelFormat.Format8bppIndexed;
            }
        }
        internal static void SetGrayPallete(this Bitmap bmp)
        {
            var pal = bmp.Palette;
            for (int n = 0; n < pal.Entries.Length; n++) pal.Entries[n] = System.Drawing.Color.FromArgb(n, n, n);
            bmp.Palette = pal;
        }

        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        internal static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);
    }
}
