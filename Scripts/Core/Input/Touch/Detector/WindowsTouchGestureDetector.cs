using UnityEngine;

namespace Assets.Scripts.Core.Input.Touch.Detector
{
    public class WindowsTouchGestureDetector : ITouchGestureDetector
    {
        private const float DoubleTapTimeout = 0.2f;
        private const float LongTapTimeout = 0.5f;

        private bool _doubleTap;
        private bool _down;
        private float _lastTapTime;
        private bool _longTap;
        private TouchResult _result;
        private Vector3 _startPosition;
        private bool _swipe;
        private bool _tap;

        private float _tapStartTime;
        private bool _tapUnknown;
        private float _tapUnknownTime;

        public void OnUpdate()
        {
            _swipe = _down && Vector3.Distance(_startPosition, UnityEngine.Input.mousePosition) > 15;
            _tapUnknown = !_tap && _tapUnknown;
            _tap = _tapUnknown && Time.time - _tapUnknownTime > DoubleTapTimeout;
            _doubleTap = false;
            _longTap = false;

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _tapStartTime = Time.time;
                _startPosition = UnityEngine.Input.mousePosition;
                _down = true;
            }
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                _doubleTap = Time.time - _lastTapTime < DoubleTapTimeout;
                _longTap = !_swipe && Time.time - _tapStartTime >= LongTapTimeout;
                _tapUnknown = !(_swipe || _doubleTap || _longTap);
                _tapUnknownTime = _tapUnknown ? Time.time : 0;
                _lastTapTime = Time.time;
                _down = false;
            }
            if (_tap || _longTap || _doubleTap)
            {
                _result = new TouchResult(_startPosition);
            }
            if (_swipe)
            {
                _result = new TouchResult(UnityEngine.Input.mousePosition);
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
            return false;
        }

        public TouchResult[] GetResult()
        {
            return new[] {_result};
        }
    }
}