using UnityEngine;

namespace Assets.Scripts.Core.Input.Touch.Detector
{
    public struct TouchResult
    {
        private readonly GameObject _gameObject;
        private readonly Vector3 _position;

        public TouchResult(Vector3 position)
        {
            _position = position;
            var ray = Camera.main.ScreenPointToRay(position);
            RaycastHit hit;
            _gameObject = Physics.Raycast(ray, out hit) ? hit.collider.gameObject : null;
        }

        public Vector3 Position
        {
            get { return _position; }
        }

        public GameObject GameObject
        {
            get { return _gameObject; }
        }

        public static TouchResult NullResult()
        {
            var result = new TouchResult(Vector3.zero);
            return result;
        }
    }
}