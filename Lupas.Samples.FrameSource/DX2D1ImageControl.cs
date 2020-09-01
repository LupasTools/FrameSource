// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// DX2D1ImageControl.cs : 30.8.2020
// MIT license

using SharpDX;
using SharpDX.Direct2D1;
using System;

namespace Lupas.Samples.FrameSource
{
    internal class DX2D1ImageControl : DX2D1Control
    {
        Bitmap image;
        BitmapRenderTarget bgraImageRenderer;
        SolidColorBrush whiteBrush;
        PixelFormat formatA8 = new PixelFormat(SharpDX.DXGI.Format.A8_UNorm, AlphaMode.Straight);
        PixelFormat formatBGRA = new PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, AlphaMode.Ignore);

        protected override void DXCreateResources(RenderTarget rt)
        {
            whiteBrush = new SolidColorBrush(rt, Color.White);
        }

        protected override void DXFreeResources()
        {
            bgraImageRenderer?.Dispose();
            image?.Dispose();
            whiteBrush?.Dispose();
        }
        protected override void Render(RenderTarget rt)
        {
            if (image == null)
            {
                base.Render(rt);
                return;
            }
            rt.Clear(this.SceneColorBrush.Color);

            rt.Transform = Matrix3x2.Identity;

            Size2 imageSize = this.image.PixelSize;

            double scale = Math.Min((double)(ClientSize.Width - ImagePadding) / imageSize.Width, (double)(ClientSize.Height - ImagePadding) / imageSize.Height);
            int imageWidth = (int)(imageSize.Width * scale);
            int imageHeight = (int)(imageSize.Height * scale);

            var rcBounds = new RectangleF(0, 0, imageSize.Width, imageSize.Height);
            var rcImage = new RectangleF((ClientSize.Width - imageWidth) / 2, (ClientSize.Height - imageHeight) / 2, imageWidth, imageHeight);

            bool isBGRAImage = image.PixelFormat.Format == SharpDX.DXGI.Format.B8G8R8A8_UNorm;

            if (!isBGRAImage)
            {
                var sz = new Size2F(imageSize.Width, imageSize.Height);
                var rc = new RectangleF(0, 0, imageSize.Width, imageSize.Height);

                if (bgraImageRenderer == null || bgraImageRenderer.Size != sz)
                {
                    SharpDX.Utilities.Dispose(ref bgraImageRenderer);
                    bgraImageRenderer = new BitmapRenderTarget(RenderTarget2D, CompatibleRenderTargetOptions.None, sz);
                }

                bgraImageRenderer.BeginDraw();
                bgraImageRenderer.Clear(Color.Black);
                bgraImageRenderer.AntialiasMode = AntialiasMode.Aliased;
                bgraImageRenderer.FillOpacityMask(this.image, whiteBrush, OpacityMaskContent.Graphics, rc, rc);
                bgraImageRenderer.EndDraw();
            }

            rt.DrawBitmap(isBGRAImage ? image : bgraImageRenderer.Bitmap, rcImage, 1, BitmapInterpolationMode.NearestNeighbor, rcBounds);

            if (ImagePadding != 0) rt.DrawRectangle(rcImage, whiteBrush, 1);
        }

        internal void LoadImage(byte[] map, int width, int height, int stride)
        {
            prepareImage(width, height, stride);
            if (map != null && map.Length != 0) image.CopyFromMemory(map, stride);

            Invalidate();
        }

        internal void LoadImage(IntPtr data, int width, int height, int stride)
        {
            prepareImage(width, height, stride);
            if (data != IntPtr.Zero) image.CopyFromMemory(data, stride);

            Invalidate();
        }

        private void prepareImage(int width, int height, int stride)
        {
            var sz = new Size2F(width, height);

            var pf = (stride / (width == 0 ? 1 : width)) == 4 ? formatBGRA : formatA8;
            if (image == null || image.PixelFormat.Format != pf.Format || image.Size != sz)
            {
                SharpDX.Utilities.Dispose(ref image);
                image = new Bitmap(RenderTarget2D, new Size2(width, height), new BitmapProperties(pf, 96F, 96F));
            }
        }
    }
}
