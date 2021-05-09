using UnityEngine;

namespace Assets.Scripts
{
    public class Link : MonoBehaviour
    {
        public Node Node1;
        public Node Node2;

        public float Width = 1f;

        public bool Enabled = true;

        private DrawingLink _Drawable;
        private void Start()
        {
            transform.position = Vector3.zero;
            _Drawable = GetComponent<DrawingLink>();
            if (_Drawable != null)
                _Drawable.UpdateVisuals();
        }

        private void OnValidate()
        {
            if(_Drawable != null)
                _Drawable.UpdateVisuals();
        }

        public void UpdateVisuals()
        {
            _Drawable.UpdateVisuals();
        }

    }
}
