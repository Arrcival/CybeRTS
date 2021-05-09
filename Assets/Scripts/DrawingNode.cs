using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Node), typeof(LineRenderer), typeof(CircleCollider2D))]
    public class DrawingNode : MonoBehaviour, ILineDrawable
    {
        private LineRenderer _LineRenderer;
        private Node _Node;

        public float SizeCoefficient = 0.2f;
        public float LineWidth = .05f;

        private CircleCollider2D _Collider2D;
        // Start is called before the first frame update
        void Start()
        {
            _LineRenderer = GetComponent<LineRenderer>();
            _Node = GetComponent<Node>();
            _Collider2D = GetComponent<CircleCollider2D>();
            UpdateVisuals();
        }

        public void UpdateVisuals()
        {
            if(_LineRenderer != null)
            {
                _LineRenderer.DrawCircle(_Node.FirewallDefense * SizeCoefficient, LineWidth, transform.position);
                _LineRenderer.SetGradientLine(_Node.Color);
            }
            if(_Collider2D != null)
                _Collider2D.radius = _Node.FinalRadius + LineWidth;

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
}
