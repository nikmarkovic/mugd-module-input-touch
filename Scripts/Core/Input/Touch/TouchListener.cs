using Assets.Scripts.Core.Input.Touch.Detector;
using UnityEngine;

namespace Assets.Scripts.Core.Input.Touch
{
    public abstract class TouchListener : MonoBehaviour
    {
        private ITouchGestureDetector _touch;

        private void Awake()
        {
#if UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8 
      		touch = new AndroidTouchGestureDetector();
		#else
            _touch = new WindowsTouchGestureDetector();
#endif
        }

        private void Update()
        {
            _touch.OnUpdate();
            OnUpdate();

            if (_touch.IsTap())
            {
                var result = _touch.GetResult()[0];
                OnTap(result.Position, result.GameObject);
            }
            else if (_touch.IsDoubleTap())
            {
                var result = _touch.GetResult()[0];
                OnDoubleTap(result.Position, result.GameObject);
            }
            else if (_touch.IsLongTap())
            {
                var result = _touch.GetResult()[0];
                OnLongTap(result.Position, result.GameObject);
            }
            else if (_touch.IsSwipe())
            {
                var result = _touch.GetResult()[0];
                OnSwipe(result.Position, result.GameObject);
            }
            else if (_touch.IsScale())
            {
                var result1 = _touch.GetResult()[0];
                var result2 = _touch.GetResult()[1];
                OnScale(result1.Position, result2.Position, result1.GameObject, result2.GameObject);
            }
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnTap(Vector3 position, GameObject go)
        {
        }

        protected virtual void OnDoubleTap(Vector3 position, GameObject go)
        {
        }

        protected virtual void OnLongTap(Vector3 position, GameObject go)
        {
        }

        protected virtual void OnSwipe(Vector3 position, GameObject go)
        {
        }

        protected virtual void OnScale(Vector3 position1, Vector3 position2, GameObject gameObject1,
            GameObject gameObject2)
        {
        }
    }
}