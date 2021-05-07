using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    public Node node1;
    public Node node2;

    public float Width = 1f;

    public bool Enabled = true;

    DrawingLink drawable;
    private void Start()
    {
        transform.position = Vector3.zero;
        drawable = GetComponent<DrawingLink>();
        if (drawable != null)
            drawable.UpdateVisuals();
    }

    private void OnValidate()
    {
        if(drawable != null)
            drawable.UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        drawable.UpdateVisuals();
    }

}
