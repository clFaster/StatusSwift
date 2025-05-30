namespace StatusSwift.Behaviors;

public partial class PulseAnimationBehavior : Behavior<Border>
{
    private Animation? _animation;
    private Border? _border;
    private bool _isAnimating;

    // ReSharper disable once MemberCanBePrivate.Global - Could be set from XAML
    public double Duration { get; set; } = 2000;

    // ReSharper disable once MemberCanBePrivate.Global - Could be set from XAML
    public double Scale { get; set; } = 1.2;

    // ReSharper disable once MemberCanBePrivate.Global - Could be set from XAML
    public double MinScale { get; set; } = 0.5;

    protected override void OnAttachedTo(Border border)
    {
        base.OnAttachedTo(border);
        _border = border;
        Start();
    }

    protected override void OnDetachingFrom(Border border)
    {
        Stop();
        _border = null;
        base.OnDetachingFrom(border);
    }

    private void Start()
    {
        if (_isAnimating || _border == null) return;

        InitAnimation().Commit(
            _border,
            "PulseAnimation",
            length: (uint)Duration,
            easing: Easing.Linear,
            repeat: () => true);

        _isAnimating = true;
    }

    private Animation InitAnimation()
    {
        _animation = new Animation
        {
            // First part - grow
            {
                0,
                0.7,
                new Animation(
                    v =>
                    {
                        if (_border != null) _border.Scale = v;
                    },
                    MinScale,
                    Scale,
                    Easing.SinInOut)
            },

            // Second part - shrink
            {
                0.7,
                1.0,
                new Animation(
                    v =>
                    {
                        if (_border != null) _border.Scale = v;
                    },
                    Scale,
                    MinScale,
                    Easing.SinInOut)
            }
        };
        return _animation;
    }

    private void Stop()
    {
        if (!_isAnimating || _border == null)
            return;

        _border.AbortAnimation("PulseAnimation");
        _border.Scale = MinScale;
        _isAnimating = false;
    }
}