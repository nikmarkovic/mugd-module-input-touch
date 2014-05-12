using UnityEngine;

namespace Assets.Scripts.Core.Input.Touch.Detector
{
    public class AndroidTouchGestureDetector : ITouchGestureDetector
    {
        private const float DoubleTapTimeout = 0.2f;
        private const float LongTapTimeout = 0.5f;

        private bool _doubleTap;
        private bool _down;
        private bool _down2;

        private float _lastTapTime;
        private bool _longTap;
        private TouchResult _result1;
        private TouchResult _result2;
        private bool _scale;
        private Vector3 _startPosition;
        private Vector3 _startPosition2;
        private bool _swipe;
        private bool _swipe2;
        private bool _tap;
        private float _tapStartTime;
        private bool _tapUnknown;
        private float _tapUnknownTime;

        public void OnUpdate()
        {
            _tapUnknown = !_tap && _tapUnknown;
            _tap = _tapUnknown && Time.time - _tapUnknownTime > DoubleTapTimeout;
            _doubleTap = false;
            _longTap = false;
            _scale = UnityEngine.Input.touchCount > 1 && (_swipe || _swipe2);

            if (UnityEngine.Input.touchCount > 0)
            {
                var touch = UnityEngine.Input.GetTouch(0);
                _swipe = !_scale && _down && Vector3.Distance(_startPosition, touch.position) > 15;

                if (UnityEngine.Input.touchCount > 1)
                {
                    var touch2 = UnityEngine.Input.GetTouch(1);
                    _swipe2 = _down2 && Vector3.Distance(_startPosition2, touch2.position) > 15;

                    if (touch2.phase == TouchPhase.Began)
                    {
                        _down2 = true;
                        _startPosition2 = touch2.position;
                    }
                    if (touch2.phase == TouchPhase.Ended)
                    {
                        _down2 = false;
                        _swipe2 = false;
                    }
                    if (_scale)
                    {
                        _result1 = new TouchResult(touch.position);
                        _result2 = new TouchResult(touch2.position);
                    }
                }

                if (touch.phase == TouchPhase.Began)
                {
                    _down = true;
                    _tapStartTime = Time.time;
                    _startPosition = touch.position;
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    _down = false;
                    _doubleTap = Time.time - _lastTapTime < DoubleTapTimeout;
                    _longTap = !_swipe && !_scale && Time.time - _tapStartTime >= LongTapTimeout;
                    _tapUnknown = !(_swipe || _scale || _doubleTap || _longTap);
                    _tapUnknownTime = _tapUnknown ? Time.time : 0;
                    _lastTapTime = Time.time;
                    _swipe = false;
                }
                if (_swipe)
                {
                    _result1 = new TouchResult(touch.position);
                }
            }

            if (_tap || _longTap || _doubleTap)
            {
                _result1 = new TouchResult(_startPosition);
            }
        }

        public bool IsTap()
        {
            return _tap;
        }

        public bool IsDoubleTap()
        {
            return _doubleTap;
        }

        public bool IsLongTap()
        {
            return _longTap;
        }

        public bool IsSwipe()
        {
            return _swipe;
        }

        public bool IsScale()
        {
            return _scale;
        }

        public TouchResult[] GetResult()
        {
            return new[] {_result1, _result2};
        }
    }
}