using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sedric
{
    public class ControlAnimator : IDisposable
    {
        private DispatcherTimer _timer;
        private double _targetValue;
        private TimeSpan _duration;
        private TimeSpan _frameRate;
        private int _currentTick;
        private int _totalTicks;
        private Easing _easing;
        private StyledProperty<double> _property;
        private Control _control;
        private bool _isReverse;

        public static readonly AttachedProperty<double> WidthProperty = AvaloniaProperty.RegisterAttached<Border, Control, double>("Width");

        public ControlAnimator(double targetValue, TimeSpan duration, Easing easing = null)
        {
            _frameRate = TimeSpan.FromSeconds(1 / 120.0);
            _targetValue = targetValue;
            _duration = duration;

            if (easing == null)
                _easing = new QuarticEaseIn();
            else
                _easing = easing;

            _totalTicks = (int)(_duration.TotalSeconds / _frameRate.TotalSeconds);

            _timer = new DispatcherTimer()
            {
                Interval = _frameRate
            };

            _timer.Tick += OnTimerTick;
        }


        public void StartAnimation(Control control, StyledProperty<double> property)
        {
            _control = control;
            _property = property;
            _isReverse = _control.GetValue(_property) > _targetValue;
            _currentTick = 0;
            _timer.Start();

        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            _currentTick++;
            
            var currentValue = _control.GetValue(_property);

            if (_currentTick > _totalTicks)
            {
                _timer.Stop();
                return;
            }

            var percentage = (float)_currentTick / _totalTicks;
            var easingPercentage = _easing.Ease(percentage);
            var finalValue = 0.0;

            if (_isReverse)
            {
                finalValue = currentValue - currentValue * easingPercentage;

                if (finalValue < _targetValue)
                    finalValue = _targetValue;
            }
            else
            {
                finalValue = _targetValue * easingPercentage;
            }

            _control.SetValue(_property, finalValue);
        }

        public void Dispose()
        {

        }
    }
}
