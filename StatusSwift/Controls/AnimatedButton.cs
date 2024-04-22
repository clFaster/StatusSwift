using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace StatusSwift.Controls;

public class AnimatedButton : SKCanvasView
{
    private bool isPressed = false;
    private SKColor normalColor = SKColors.Blue;
    private SKColor pressedColor = SKColors.LightBlue;

    public AnimatedButton()
    {
        EnableTouchEvents = true;

        PaintSurface += OnPaintSurface;
        Touch += OnTouch;
    }

    private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        SKCanvas canvas = e.Surface.Canvas;
        canvas.Clear();

        SKPaint paint = new SKPaint();
        paint.Color = isPressed ? pressedColor : normalColor;
        paint.IsAntialias = true;

        // Draw the button
        SKRect rect = new SKRect(0, 0, 100, 40);
        canvas.DrawRoundRect(rect, 10, 10, paint);
    }

    private void OnTouch(object sender, SKTouchEventArgs e)
    {
        switch (e.ActionType)
        {
            case SKTouchAction.Pressed:
                isPressed = true;
                InvalidateSurface();
                break;
            case SKTouchAction.Released:
                isPressed = false;
                InvalidateSurface();
                // You can add more logic here for handling button clicks
                break;
        }

        e.Handled = true;
    }
}