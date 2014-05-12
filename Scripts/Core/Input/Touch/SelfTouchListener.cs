using UnityEngine;

namespace Assets.Scripts.Core.Input.Touch
{
    public abstract class SelfTouchListener : TouchListener
    {
        protected new void OnTap(Vector3 position, GameObject go)
        {
            if (go == this)
            {
                OnTap(position);
            }
        }

        protected new void OnDoubleTap(Vector3 position, GameObject go)
        {
            if (go == this)
            {
                OnDoubleTap(position);
            }
        }

        protected new void OnLongTap(Vector3 position, GameObject go)
        {
            if (go == this)
            {
                OnLongTap(position);
            }
        }

        protected new void OnSwipe(Vector3 position, GameObject go)
        {
            if (go == this)
            {
                OnSwipe(position);
            }
        }

        protected new void OnScale(Vector3 position1, Vector3 position2, GameObject gameObject1, GameObject gameObject2)
        {
            if (gameObject1 == this || gameObject2 == this)
            {
                OnScale(position1, position2);
            }
        }

        protected virtual void OnTap(Vector3 position)
        {
        }

        protected virtual void OnDoubleTap(Vector3 position)
        {
        }

        protected virtual void OnLongTap(Vector3 position)
        {
        }

        protected virtual void OnSwipe(Vector3 position)
        {
        }

        protected virtual void OnScale(Vector3 position1, Vector3 position2)
        {
        }
    }
}