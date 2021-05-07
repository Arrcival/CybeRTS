using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Link), typeof(LineRenderer))]
public class DrawingLink : MonoBehaviour, ILineDrawable
{
    public float CoeffWidth = .05f;

    Link link;
    LineRenderer lineRenderer;
    public void UpdateVisuals()
    {
        if(link != null)
        {
            Vector3[] points = new Vector3[2];
            Vector3 direction = link.node2.transform.position - link.node1.transform.position;


            points[0] = link.node1.transform.position + Vector3.ClampMagnitude(direction, link.node1.FinalRadius);
            points[1] = link.node2.transform.position + Vector3.ClampMagnitude(-direction, link.node2.FinalRadius);

            lineRenderer.startWidth = link.Width * CoeffWidth;
            lineRenderer.endWidth = link.Width * CoeffWidth;
            lineRenderer.SetPositions(points);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        link = GetComponent<Link>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;

        UpdateVisuals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
