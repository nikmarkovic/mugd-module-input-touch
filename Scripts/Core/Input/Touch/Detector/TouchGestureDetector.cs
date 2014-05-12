namespace Assets.Scripts.Core.Input.Touch.Detector
{
    public interface ITouchGestureDetector
    {
        void OnUpdate();

        bool IsTap();

        bool IsDoubleTap();

        bool IsLongTap();

        bool IsSwipe();

        bool IsScale();

        TouchResult[] GetResult();
    }
}