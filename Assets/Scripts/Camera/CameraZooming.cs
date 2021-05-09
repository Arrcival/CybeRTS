using UnityEngine;

namespace Assets.Scripts.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraZooming : MonoBehaviour
    {
        private UnityEngine.Camera _Camera;

        public float Increment = .1f;
        // Start is called before the first frame update
        void Start()
        {
            _Camera = GetComponent<UnityEngine.Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            // TODO
            float input = Input.GetAxis("Mouse ScrollWheel");
            if (input != 0f)
            {
                _Camera.orthographicSize -= Increment * input;
            }
        }
    }
}
