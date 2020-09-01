// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// ManagedFrameData.cs : 30.8.2020
// MIT license

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Lupas.FrameSource
{
    public class ManagedFrameData : IFrameData
    {
        public int W { get; private set; }
        public int H { get; private set; }
        public int Stride { get; private set; }
        public byte[] Map { get; private set; }
        public dynamic DynamicData => Map;

        public void CopyFrom(IntPtr src, int h, int stride)
        {
            var posStride = Math.Abs(stride);
            for (int y = 0; y < h; y++)
                Marshal.Copy(IntPtr.Add(src, y * stride), Map, y * posStride, posStride);
        }

        public void ResizeIfRequired(int w, int h, int stride)
        {
            if (Map != null) if (w == this.W && h == this.H && Math.Abs(stride) == this.Stride) return;

            this.W = w;
            this.H = h;
            this.Stride = Math.Abs(stride);
            Map = new byte[H * Stride];
        }

        public Bitmap ToBitmap()
        {
            if (Map == null) throw new NullReferenceException();

            var pf = FrameDataUtilities.Format(this);
            Bitmap bmp = new Bitmap(W, H, pf);

            var data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, pf);

            for (int y = 0; y < H; y++)
                Marshal.Copy(Map, y * Math.Abs(Stride), IntPtr.Add(data.Scan0, y * data.Stride), data.Stride);

            bmp.UnlockBits(data);

            if (pf is System.Drawing.Imaging.PixelFormat.Format8bppIndexed) bmp.SetGrayPallete();
            return bmp;
        }
    }
}
