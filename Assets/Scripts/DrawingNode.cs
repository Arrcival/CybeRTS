using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Node), typeof(LineRenderer), typeof(CircleCollider2D))]
public class DrawingNode : MonoBehaviour, ILineDrawable
{

    LineRenderer lineRenderer;
    Node node;

    public float SizeCoefficient = 0.2f;
    public float LineWidth = .05f;

    CircleCollider2D collider2D;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        node = GetComponent<Node>();
        collider2D = GetComponent<CircleCollider2D>();
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        if(lineRenderer != null)
        {
            lineRenderer.DrawCircle(node.FirewallDefense * SizeCoefficient, LineWidth, transform.position);
            lineRenderer.SetGradientLine(node.Color);
        }
        if(collider2D != null)
            collider2D.radius = node.FinalRadius + LineWidth;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        UpdateVisuals();
    }
}
