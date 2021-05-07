using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebGenerator : MonoBehaviour
{
    public GameObject Node;
    Vector3[] NodePositions;
    public GameObject LinkHolder;

    public float DistanceMaxForLinks = 2f;
    
    public float DistanceBetweenNodes = 2f;    
    public int NodesWidth = 15;
    public int NodesHeight = 15;


    //public int MainLineSize = 3;
    //public float DistanceBetweenCircles = 1f;





    // Start is called before the first frame update
    void Start()
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

        Vector3[] NodePositions = new Vector3[NodesHeight * NodesHeight];
        for (int i = 0; i < NodesHeight; i++)
            for (int j = 0; j < NodesWidth; j++)
                NodePositions[NodesWidth * i + j] = new Vector3(j * DistanceBetweenNodes, i * DistanceBetweenNodes);

        if (NodePositions != null && NodePositions.Length > 0)
        {
            Node[] nodes = new Node[NodePositions.Length];
            for(int i = 0; i < NodePositions.Length; i++)
            {
                GameObject goNode = Instantiate(Node, gameObject.transform);
                goNode.transform.position = NodePositions[i];
                goNode.name = $"Node {i}";
                Node node = goNode.GetComponent<Node>();
                node.ID = i;
                node.FirewallDefense = Random.Range(4, 9);
                nodes[i] = node;
            }

            for (int i = 0; i < nodes.Length; i++)
                for(int j = 0; j < nodes.Length; j++)
                    if(i != j)
                        nodes[i].TryLinkingToNeighboorNode(nodes[j], LinkHolder, DistanceMaxForLinks);
        }
        Destroy(this);
    }

}
