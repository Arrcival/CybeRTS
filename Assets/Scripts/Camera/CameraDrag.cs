using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraDrag : MonoBehaviour
    {
        //private Vector3 ResetCamera;
        private Vector3 _Origin;
        private Vector3 _Difference;
        private bool _Drag = false;
        void Start()
        {
            //ResetCamera = Camera.main.transform.position;
        }
        void LateUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                _Difference = (UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition)) - UnityEngine.Camera.main.transform.position;
                if (_Drag == false)
                {
                    _Drag = true;
                    _Origin = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else
            {
                _Drag = false;
            }
            if (_Drag == true)
            {
                UnityEngine.Camera.main.transform.position = _Origin - _Difference;
            }
            /*RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
        
        if (Input.GetMouseButton(1))
        {
            Camera.main.transform.position = ResetCamera;
        }*/
        }


    }
}