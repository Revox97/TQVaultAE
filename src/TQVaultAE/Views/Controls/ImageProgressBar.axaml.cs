using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace TQVaultAE.Views.Controls
{
    public partial class ImageProgressBar : UserControl
    {
        public static readonly StyledProperty<Bitmap?> BackgroundImageProperty =
            AvaloniaProperty.Register<ImageProgressBar, Bitmap?>(nameof(BackgroundImage));

        public static readonly StyledProperty<Bitmap?> FillImageProperty =
            AvaloniaProperty.Register<ImageProgressBar, Bitmap?>(nameof(FillImage));

        public static readonly StyledProperty<double> ValueProperty =
            AvaloniaProperty.Register<ImageProgressBar, double>(nameof(Value), 0,
                coerce: (control, value) => Math.Clamp(value, ((ImageProgressBar)control).Minimum, ((ImageProgressBar)control).Maximum));

        public static readonly StyledProperty<double> MinimumProperty =
            AvaloniaProperty.Register<ImageProgressBar, double>(nameof(Minimum), 0);

        public static readonly StyledProperty<double> MaximumProperty =
            AvaloniaProperty.Register<ImageProgressBar, double>(nameof(Maximum), 100);

        public Bitmap? BackgroundImage
        {
            get => GetValue(BackgroundImageProperty);
            set => SetValue(BackgroundImageProperty, value);
        }

        public Bitmap? FillImage
        {
            get => GetValue(FillImageProperty);
            set => SetValue(FillImageProperty, value);
        }

        public double Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public double Minimum
        {
            get => GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public double Maximum
        {
            get => GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        static ImageProgressBar()
        {
            AffectsRender<ImageProgressBar>(BackgroundImageProperty, FillImageProperty, ValueProperty, MinimumProperty, MaximumProperty);
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            if (BackgroundImage is not null)
                context.DrawImage(BackgroundImage, new Rect(0, 0, Bounds.Width, Bounds.Height));

            if (FillImage is null)
                return;

            double range = Maximum - Minimum;

            if (range <= 0)
                return;

            double progress = Math.Clamp((Value - Minimum) / range, 0, 1);
            double fillWidth = Bounds.Width * progress;

            if (fillWidth <= 0)
                return;

            // Only the left part of the fill image is rendered.
            using (context.PushClip(new Rect(0, 0, fillWidth, Bounds.Height)))
            {
                context.DrawImage(FillImage, new Rect(0, 0, Bounds.Width, Bounds.Height));
            }
        }
    }
}