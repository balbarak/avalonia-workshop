using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.Diagnostics;

namespace AvaloniaWorkshop.Controls;

public class HamburgerMenu : TemplatedControl
{
    private double _menuDesiredWidth;
    private double _menuTargetWidth;
    private TimeSpan _frameRate = TimeSpan.FromSeconds(1 / 60.0);
    private TimeSpan _animationDuration = TimeSpan.FromSeconds(1);
    private int _totalTicks;

    private Grid _container;
    private Border _menu;
    private bool _isAnimating;
    private int _currentTick = 0;

    private DispatcherTimer _timer;


    public static readonly StyledProperty<bool> IsOpenProperty = AvaloniaProperty.Register<HamburgerMenu, bool>(nameof(IsOpen), defaultValue: true, notifying: OnIsOpenChanged);

    public static readonly StyledProperty<object> ContentProperty = AvaloniaProperty.Register<HamburgerMenu, object>(nameof(Content), defaultValue: null);

    public static readonly StyledProperty<TimeSpan> AnimationDurationProperty = AvaloniaProperty.Register<HamburgerMenu, TimeSpan>(nameof(Content), defaultValue: TimeSpan.FromMicroseconds(200));

    public bool IsOpen { get => (bool)GetValue(IsOpenProperty); set => SetValue(IsOpenProperty, value); }

    public object Content { get => (bool)GetValue(ContentProperty); set => SetValue(ContentProperty, value); }

    public TimeSpan AnimationDuration { get => (TimeSpan)GetValue(AnimationDurationProperty); set => SetValue(AnimationDurationProperty, value); }

    public HamburgerMenu()
    {
        _timer = new DispatcherTimer
        {
            Interval = _frameRate
        };

        _totalTicks = (int)(_animationDuration.TotalSeconds / _frameRate.TotalSeconds);
        _timer.Tick += OnTimeTicked;

        //_timer.Tick += (sender, e) =>
        //{
        //    if (_menu == null)
        //        return;

        //    if (!IsOpen)
        //        return;

        //    _isAnimating = true;
        //    _currentTick++;

        //    if (_currentTick > totalTicks)
        //    {
        //        _timer.Stop();
        //        _isAnimating = false;
        //        return;
        //    }

        //    var percentage = (float)_currentTick / totalTicks;

        //    var easing = new QuarticEaseIn();

        //    var finalWidth = _menuDesiredWidth * easing.Ease(percentage);


        //    _menu.Width = finalWidth;

        //    Debug.WriteLine($"Current Tick: {_currentTick}");
        //};

    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _container = e.NameScope.Find<Grid>("PART_Container");
        _menu = e.NameScope.Find<Border>("PART_Menu");
    }

    public override void Render(DrawingContext context)
    {
        if (!_isAnimating)
        {
            _menuDesiredWidth = _container.ColumnDefinitions[0].ActualWidth;

            _currentTick = 0;

            _timer.Start();
        }




        base.Render(context);
    }

    public void OnIsOpenChanged()
    {
        if (IsOpen)
        {
            OpenWithAnimation();
        }
        else
        {
            CloseWithAnimation();
        }
    }

    private void OpenWithAnimation()
    {
        if (_isAnimating)
            return;

        _currentTick = 0;

        _timer.Start();
    }

    private void CloseWithAnimation()
    {
        if (_isAnimating)
            return;

        _currentTick = 0;

        _timer.Start();
    }


    private void OnTimeTicked(object sender, EventArgs e)
    {
        if (_menu == null)
            return;

        if (IsOpen)
            _menuTargetWidth = _menuDesiredWidth;
        else
        {
            _menuTargetWidth = 0;
        }

        _isAnimating = true;

        _currentTick++;

        if (_currentTick > _totalTicks)
        {
            _timer.Stop();
            _isAnimating = false;
            return;
        }

        var percentage = (float)_currentTick / _totalTicks;

        var easing = new QuarticEaseIn();
        var easingOut = new QuarticEaseInOut();

        double finalWidth;

        if (!IsOpen)
            finalWidth = _menuTargetWidth * easing.Ease(percentage);
        else
            finalWidth = _menuTargetWidth * easingOut.Ease(percentage);

        _menu.Width = finalWidth;

        Debug.WriteLine($"Target Width: {finalWidth}");
    }


    private static void OnIsOpenChanged(IAvaloniaObject arg1, bool arg2)
    {
        if (arg1 is HamburgerMenu hamburger && arg2)
        {
            hamburger.OnIsOpenChanged();
        }
    }
}
