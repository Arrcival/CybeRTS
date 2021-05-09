using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Link), typeof(LineRenderer))]
    public class DrawingLink : MonoBehaviour, ILineDrawable
    {
        public float CoefWidth = .05f;

        Link _Link;
        LineRenderer _LineRenderer;
        public void UpdateVisuals()
        {
            if(_Link != null)
            {
                Vector3[] points = new Vector3[2];
                Vector3 direction = _Link.Node2.transform.position - _Link.Node1.transform.position;


                points[0] = _Link.Node1.transform.position + Vector3.ClampMagnitude(direction, _Link.Node1.FinalRadius);
                points[1] = _Link.Node2.transform.position + Vector3.ClampMagnitude(-direction, _Link.Node2.FinalRadius);

                _LineRenderer.startWidth = _Link.Width * CoefWidth;
                _LineRenderer.endWidth = _Link.Width * CoefWidth;
                _LineRenderer.SetPositions(points);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            _Link = GetComponent<Link>();
            _LineRenderer = GetComponent<LineRenderer>();
            _LineRenderer.useWorldSpace = false;

            UpdateVisuals();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
