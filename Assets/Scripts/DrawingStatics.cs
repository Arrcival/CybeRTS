using UnityEngine;

namespace Assets.Scripts
{
    public static class DrawingStatics
    {
        private static readonly Shader _LineShader = Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply");
        public static Material LineMaterial => new Material(_LineShader);

        public static LineRenderer DrawCircle(this LineRenderer line, float radius, float lineWidth, Vector3 pos)
        {
            var segments = 360;
            line.useWorldSpace = false;
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.positionCount = segments + 1;

            var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
            var points = new Vector3[pointCount];

            for (int i = 0; i < pointCount; i++)
            {
                var rad = Mathf.Deg2Rad * (i * 360f / segments);
                points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
            }

            line.SetPositions(points);
            //line.material = LineMaterial;
            //line.transform.Translate(pos);
            line.transform.eulerAngles = Vector3.right * 90f;

            return line;
        }

        #region Set LineRenderer Colors
        public static void SetGradientLine(this LineRenderer lineRenderer, Color startingColor)
        {
            SetGradientLine(lineRenderer, startingColor, startingColor, 1f);
        }

        public static void SetGradientLine(this LineRenderer lineRenderer, Color startingColor, Color endingColor)
        {
            SetGradientLine(lineRenderer, startingColor, endingColor, 1f);
        }

        public static void SetGradientLine(this LineRenderer lineRenderer, Color startingColor, Color endingColor, float alpha)
        {
            //lineRenderer.startColor = startingColor;
            //lineRenderer.endColor = endingColor;
        
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(startingColor, 0.0f), new GradientColorKey(endingColor, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
            lineRenderer.colorGradient = gradient;
        }

        #endregion

    }
}
