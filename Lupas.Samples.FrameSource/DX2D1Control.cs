// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// DX2D1Control.cs : 30.8.2020
// MIT license

using System;
using System.Windows.Forms;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX;
using System.ComponentModel;

namespace Lupas.Samples.FrameSource
{
    public partial class DX2D1Control : UserControl
    {
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ImagePadding { get; set; }

        protected SharpDX.Direct2D1.Factory Factory2D;
        protected WindowRenderTarget RenderTarget2D;
        protected SolidColorBrush SceneColorBrush;

        public DX2D1Control()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        private void DX2Control_Load(object sender, EventArgs e)
        {
            Factory2D = new SharpDX.Direct2D1.Factory();

            HwndRenderTargetProperties properties = new HwndRenderTargetProperties(){
                Hwnd = Handle,
                PixelSize = new SharpDX.Size2(Width, Height),
                PresentOptions = PresentOptions.None};

            RenderTarget2D = new WindowRenderTarget(Factory2D, new RenderTargetProperties(new PixelFormat(Format.Unknown, SharpDX.Direct2D1.AlphaMode.Premultiplied)), properties);

            RenderTarget2D.AntialiasMode = AntialiasMode.PerPrimitive;

            SceneColorBrush = new SolidColorBrush(RenderTarget2D, SharpDX.Color.FromBgra(BackColor.ToArgb()));
            DXCreateResources(RenderTarget2D);

            Disposed += DX2Control_Disposed;
            BackColorChanged += DX2Control_BackColorChanged;
        }

        private void DX2Control_Disposed(object sender, EventArgs e)
        {
            DXFreeResources();
            SharpDX.Utilities.Dispose(ref SceneColorBrush);
            SharpDX.Utilities.Dispose(ref RenderTarget2D);
            SharpDX.Utilities.Dispose(ref Factory2D);
        }

        private void DX2Control_Paint(object sender, PaintEventArgs e)
        {
            RenderTarget2D.BeginDraw();
            Render(RenderTarget2D);
            RenderTarget2D.EndDraw();
        }
            
        private void DX2Control_Resize(object sender, EventArgs e)
        {
            RenderTarget2D?.Resize(new SharpDX.Size2(ClientSize.Width, ClientSize.Height));
            Invalidate();
        }

        protected virtual void DXCreateResources(RenderTarget rt) { }
        protected virtual void DXFreeResources() { }
        protected virtual void Render(RenderTarget rt)
        {
            RenderTarget2D.Clear(SceneColorBrush.Color);
            var width = ClientSize.Width;
            var height = ClientSize.Height;
            float indent = ImagePadding;
            float rcRad = indent * 1.5f;
            using (var rcG = new RoundedRectangleGeometry(Factory2D, new RoundedRectangle() { RadiusX = rcRad, RadiusY = rcRad, Rect = new RectangleF(indent, indent, width - indent * 2, height - indent * 2) }))
            using (var sBr = new SolidColorBrush(RenderTarget2D, Color.Black))
                RenderTarget2D.FillGeometry(rcG, sBr, null);
        }

        private void DX2Control_BackColorChanged(object sender, EventArgs e)
        {
            SceneColorBrush.Color = SharpDX.Color.FromBgra(BackColor.ToArgb());
        }  
    }
}
