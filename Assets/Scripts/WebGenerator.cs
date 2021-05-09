using System;
using System.Linq;
using Assets.Scripts.Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public enum WebType
    {
        SQUARE, HEXAGON, TREE, DIAMOND
    }

    public class WebGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _Node;
        [SerializeField] private GameObject _LinkHolder;

        public float DistanceMaxForLinks = 2f;
    
        public float DistanceBetweenNodes = 2f;    
        public int NodesWidth = 15;
        public int NodesHeight = 15;
        [SerializeField] private WebType _Type = WebType.SQUARE;


        //public int MainLineSize = 3;
        //public float DistanceBetweenCircles = 1f;

        // Start is called before the first frame update
        void Start()
        {
            Vector3[] positions;
            switch (_Type)
            {
                case WebType.SQUARE:
                    SquareWeb();
                    break;
                case WebType.HEXAGON:
                    HexagonWeb();
                    break;
                case WebType.TREE:
                    SquareWeb();
                    break;
                case WebType.DIAMOND:
                    SquareWeb();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Destroy(this);
        }

        private void SquareWeb()
        {
            if (NodesHeight < 1 || NodesWidth < 1)
                throw new Exception("Height and Width of the web must be both greater than 0");

            Vector3[] nodePositions = new Vector3[NodesHeight * NodesHeight];
            for (int i = 0; i < NodesHeight; i++)
            for (int j = 0; j < NodesWidth; j++)
                nodePositions[NodesWidth * i + j] = new Vector3(j * DistanceBetweenNodes, i * DistanceBetweenNodes);

            Node[] nodes = new Node[nodePositions.Length];
            for (int i = 0; i < nodePositions.Length; i++)
            {
                GameObject goNode = Instantiate(_Node, gameObject.transform);
                goNode.transform.position = nodePositions[i];
                goNode.name = $"Node {i}";
                Node node = goNode.GetComponent<Node>();
                node.ID = i;
                node.FirewallDefense = Random.Range(4, 9);
                nodes[i] = node;
            }

            for (int i = 0; i < nodes.Length; i++)
            for (int j = 0; j < nodes.Length; j++)
                if (i != j)
                    nodes[i].TryLinkingToNeighborNode(nodes[j], _LinkHolder, DistanceMaxForLinks);
        }

        private void HexagonWeb()
        {
            Node[] nodes = new Node[NodesHeight * NodesWidth];

            Vector2[] hexa = CreateHexagon(new Vector2(0, 0), 0);
            for (int i = 0; i < hexa.Length; i++)
            {
                GameObject goNode = Instantiate(_Node, gameObject.transform);
                goNode.transform.position = hexa[i];
                goNode.gameObject.name = $"Node {i}";
                Node node = goNode.GetComponent<Node>();
                node.ID = i;
                node.FirewallDefense = 4;
                nodes[i] = node;
                Debug.Log(i + " " + nodes.Length);
                if (i > 0)
                    node.TryLinkingToNeighborNode(nodes[i - 1], _LinkHolder, DistanceMaxForLinks);
                if (i == nodes.Length - 1)
                    node.TryLinkingToNeighborNode(nodes[0], _LinkHolder, DistanceBetweenNodes);
            }
        }

        private Vector2[] CreateHexagon(Vector2 startingPoint, float baseAngle)
        {
            Vector2[] positions = new Vector2[6];
            positions[0] = startingPoint;
            Vector2 baseVector = Statics.DegreeToVector2(baseAngle).normalized;
            Vector2 lastNode = startingPoint + baseVector  * DistanceBetweenNodes;

            positions[1] = lastNode;
            for (int i = 2; i < 6; ++i)
            {
                positions[i] = lastNode + (baseVector * DistanceBetweenNodes).Rotate(60 * (i - 1)).normalized * DistanceBetweenNodes;
                lastNode = positions[i];
            }

            return positions;
        }

        private void HexagonAttempt()
        {
            /** Hexagons from nothing
        int Circles = MainLineSize - 2;
        int NodeAmount = MainLineSize - 1;
        for(int i = NodeAmount - 1; i >= 2; i--)
            NodeAmount += i;
        NodeAmount *= 2;
        NodeAmount += MainLineSize; // Middle line**/

            /** Hexagons from circles
        int incrementalCircles = 1;
        int tempCircleSize = FirstCircleAmount;
        for(int i = 1; i <= CirclesAmount; i++)
        {
            //circles
            for(int j = 1; j <= tempCircleSize; j++)
            {
                //node on each circle, must have a distance and an angle
                float currentAngle = 360 / tempCircleSize * j;
                Vector3 point = Statics.DegreeToVector2(currentAngle).normalized * (DistanceBetweenCircles * i * (1f + i * CoeffPercentageIncrease));

                NodePositions[incrementalCircles] = point;
                incrementalCircles++;
            }
            tempCircleSize *= 2;
        }**/
        }
    }
}
