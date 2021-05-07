using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Statics;

public class Node : MonoBehaviour
{
    public float FirewallDefense = 5;
    public float PacketsPerSecond = 1f;

    public Color Color = Color.white;

    // Link, Node, PacketEntryBlocked
    List<(Link, Node, bool)> NeighboorNodes = new List<(Link, Node, bool)>();

    DrawingNode drawable;
    public int ID;

    public List<(float, Packet, TransfertType)> PacketsHistory = new List<(float, Packet, TransfertType)>();
    public List<Packet> PacketsToTreat = new List<Packet>();
    float currentWorkingTime = 0f;

    [ReadOnly] public bool IsSelectedNode = false;


    Cores cores;
    public float FinalRadius
    {
        get
        {
            if (drawable != null)
                return FirewallDefense * drawable.SizeCoefficient;
            return FirewallDefense;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        drawable = GetComponent<DrawingNode>();
        cores = new Cores(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.hasChanged)
        {
            UpdateVisualNeighboorLines();
            transform.hasChanged = false;
        }

        if(PacketsToTreat.Count > 0)
        {
            Packet packetToTreat = PacketsToTreat[0]; // Always the first one
            currentWorkingTime += Time.deltaTime;
            if(currentWorkingTime >= 1 / PacketsPerSecond)
            {
                PacketsToTreat.Remove(packetToTreat);
                if (packetToTreat.EndingNode.ID == ID)
                {
                    packetToTreat.OnReceipt();
                    PacketsHistory.Add((GameManager.TimeSinceStart, packetToTreat, TransfertType.RECEIPT));
                } else
                {
                    (Link, Node, bool) group = NeighboorNodes.FirstOrDefault(e => e.Item2.ID == packetToTreat.GetNextNodeID(ID));
                    if(group.Item1 != null && group.Item2 != null)
                    {
                        if(group.Item1.Enabled)
                        {
                            // Active Link
                            group.Item2.ReceivePacket(group.Item1.name, packetToTreat);
                        }
                    } // else the packet die without any other effect

                    PacketsHistory.Add((GameManager.TimeSinceStart, packetToTreat, TransfertType.TRANSFERT));
                }

                currentWorkingTime -= 1 / PacketsPerSecond;
            }
        }
    }

    public void ReceivePacket(string linkName, Packet newPacket)
    {
        if(!NeighboorNodes.Exists(e => e.Item1.name == linkName && !e.Item3))
            PacketsToTreat.Add(newPacket);
    }



    public void TryLinkingToNeighboorNode(Node otherNode, GameObject parentLink, float distanceMax)
    {
        if(!NeighboorNodes.Exists(x => x.Item2.ID == otherNode.ID))
        {
            Vector2 ourPosition = transform.position;
            Vector2 theirPosition = otherNode.transform.position;
            float distance = Vector2.Distance(ourPosition, theirPosition);
            if (distance <= distanceMax)
            {
                GameObject gameObject = Instantiate(Statics.LineGameobject, parentLink.transform);
                gameObject.name = $"Link {ID}-{otherNode.ID}";
                Link link = gameObject.GetComponent<Link>();
                link.node1 = this;
                link.node2 = otherNode;

                NeighboorNodes.Add((link, otherNode, true));
                otherNode.NeighboorNodes.Add((link, this, true));
            }
        }
    }

    #region Update Visuals for node changes

    void UpdateVisualNeighboorLines()
    {
        for(int i = 0; i < NeighboorNodes.Count; i++)
            NeighboorNodes[i].Item1.UpdateVisuals();
    }
    void UpdateVisuals()
    {
        if (drawable != null)
            drawable.UpdateVisuals();
    }

    private void OnValidate()
    {
        UpdateVisualNeighboorLines();
        UpdateVisuals();
    }
    #endregion
}
