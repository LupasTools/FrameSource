// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// UnmanagedFrameData.cs : 30.8.2020
// MIT license

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Lupas.FrameSource
{
    public class UnmanagedFrameData : IFrameData, IDisposable
    {
        public int W { get; private set; }
        public int H { get; private set; }
        public int Stride { get; private set; }
        public IntPtr DataPtr { get; private set; } = IntPtr.Zero;
        public dynamic DynamicData => DataPtr;

        public void ResizeIfRequired(int w, int h, int stride)
        {
            if (DataPtr != IntPtr.Zero) if (w == this.W && h == this.H && Math.Abs(stride) == this.Stride) return;

            this.W = w;
            this.H = h;
            this.Stride = Math.Abs(stride);
            Dispose();
            DataPtr = Marshal.AllocHGlobal(H * Math.Abs(Stride));
        }

        public void CopyFrom(IntPtr src, int h, int stride)
        {
            var posStride = Math.Abs(stride);
            for (int y = 0; y < h; y++)
                FrameDataUtilities.CopyMemory(IntPtr.Add(DataPtr, y * posStride), IntPtr.Add(src, y * stride), (uint)posStride);
        }

        public System.Drawing.Bitmap ToBitmap()
        {
            if (DataPtr == null || DataPtr == IntPtr.Zero) throw new NullReferenceException();

            var pf = FrameDataUtilities.Format(this);
            Bitmap bmp = new Bitmap(W, H, Stride, pf, DataPtr);

            if (pf is System.Drawing.Imaging.PixelFormat.Format8bppIndexed) bmp.SetGrayPallete();
            return bmp;
        }

        public void Dispose()
        {
            if (DataPtr != IntPtr.Zero) Marshal.FreeHGlobal(DataPtr);
            DataPtr = IntPtr.Zero;
        }
    }
}
