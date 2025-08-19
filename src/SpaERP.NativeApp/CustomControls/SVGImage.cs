using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using Svg.Skia;
using Microsoft.Maui.Controls;

namespace SpaERP.NativeApp
{
    public partial class SVGImage : SKCanvasView
    {
        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create(nameof(Source), typeof(string), typeof(SVGImage), default(string), propertyChanged: OnSourceChanged);

        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create(nameof(Source), typeof(Color), typeof(SVGImage), default(string), propertyChanged: OnSourceChanged);

        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        private SKSvg? svg;

        private static void OnSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SVGImage svgImage && newValue is string path)
            {
                svgImage.LoadSvg(path);
            }
        }

        private void LoadSvg(string path)
        {
            svg = new SKSvg();

            using var stream = File.OpenRead(path);
            svg.Load(stream);

            InvalidateSurface();
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Transparent);

            if (svg != null)
            {
                var info = e.Info;
                var svgBounds = svg.Picture.CullRect;

                var scale = Math.Min(e.Info.Width / svg.Picture.CullRect.Width,
                                     e.Info.Height / svg.Picture.CullRect.Height);

                // Compute translation to center SVG
                float translateX = (info.Width - svgBounds.Width * scale) / 2f;
                float translateY = (info.Height - svgBounds.Height * scale) / 2f;

                canvas.Translate(translateX, translateY);
                canvas.Scale(scale);

                var paint = new SKPaint();
                if (Color != null)
                {
                    paint.ColorFilter = SKColorFilter.CreateBlendMode(Color.ToSKColor(), SKBlendMode.SrcIn);
                }

                canvas.DrawPicture(svg.Picture, paint);
            }
        }
    }
}
