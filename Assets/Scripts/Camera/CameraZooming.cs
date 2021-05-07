using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CameraZooming : MonoBehaviour
{
    Camera camera;

    public float Increment = .1f;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO
        float input = Input.GetAxis("Mouse ScrollWheel");
        if (input != 0f)
        {
            camera.orthographicSize -= Increment * input;
        }
    }
}
