using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
using Assets.Scripts.Managers;
using Assets.Scripts.Packets;
using UnityEngine;
using static Assets.Scripts.Helpers.Statics;

namespace Assets.Scripts
{
    public class Node : MonoBehaviour
    {

        public float WidthDebug = 4f;
        public Color Color = Color.white;

        // Link, Node, PacketEntryBlocked
        private List<Neighbor> _NeighborNodes = new List<Neighbor>();

        DrawingNode _Drawable;
        public int ID;

        public List<(float, Packet, TransferType)> PacketsHistory = new List<(float, Packet, TransferType)>();
        public List<Packet> PacketsToTreat = new List<Packet>();
        private float _CurrentWorkingTime = 0f;

        public Cores.Cores Cores;

        [ReadOnly] public bool IsSelectedNode = false;
        [ReadOnly] public bool PlayerControlled = false;
        [ReadOnly] public float _HP = 0f;
        [ReadOnly] public float _HPmax = 0f;
        [ReadOnly] public float Defense = 0f;

        public bool IsBankNode = false; // Receives currency packets, is invicible (can not be captured)

        float TimeSinceLastHit = 0f;



        public float FinalRadius
        {
            get
            {
                if (_Drawable != null)
                    return WidthDebug * _Drawable.SizeCoefficient;
                return WidthDebug;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            _HPmax += RAW_HP;
            _HP = _HPmax;
            _Drawable = GetComponent<DrawingNode>();
            Cores = new Cores.Cores(this);


            // DEBUGGING
            if(ID == 0)
            {
                _NeighborNodes[0].Node.BecomeCurrencyNode();
                for (int i = 0; i < 100; i++)
                    PacketsToTreat.Add(new CurrencyPacket(new List<Node> { this, _NeighborNodes[0].Node }, 1f, 1 / Mathf.PI * Random.Range(0f, 1f), GetRandomCurrencyType()));
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            if(transform.hasChanged)
            {
                UpdateVisualNeighborLines();
                transform.hasChanged = false;
            }

            #region Treating Packets
            if (PacketsToTreat.Count > 0)
            {
                Packet packetToTreat = PacketsToTreat[0]; // Always the first one
                _CurrentWorkingTime += Time.deltaTime;
                if(_CurrentWorkingTime >= packetToTreat.Size / SIZE_TREATED_PER_SECONDS)
                {
                    PacketsToTreat.Remove(packetToTreat);
                    if (packetToTreat.EndingNode.ID == ID)
                    {
                        packetToTreat.OnReceive(this);
                        LogPacket(packetToTreat, TransferType.RECEIVED);
                    } else
                    {
                        Neighbor group = _NeighborNodes.FirstOrDefault(e => e.Node.ID == packetToTreat.GetNextNodeID(ID));
                        if(group != null)
                        {
                            if(group.Link.Enabled)
                            {
                                // Active Link
                                group.Node.ReceivePacket(group.Link.name, packetToTreat);
                            }
                        } // else the packet die without any other effect

                        if (packetToTreat.StartingNode.ID == ID)
                            LogPacket(packetToTreat, TransferType.SENT);
                        else
                            LogPacket(packetToTreat, TransferType.TRANSFER);
                    }
                    _CurrentWorkingTime -= packetToTreat.Size / SIZE_TREATED_PER_SECONDS;
                }
            }
            #endregion

            #region Passive regenration
            if (!PlayerControlled && !IsFullLife())
            {
                TimeSinceLastHit += Time.deltaTime;
                if (TimeSinceLastHit >= TIME_FOR_FULL_REGEN)
                {
                    _HP = _HPmax;
                    if(IsSelectedNode)
                        Statics.UIManager.UpdateNameAndHP();
                }
            }
            #endregion

        }

        #region About attacks
        public void DealDamage(float damageReceived, Packet attackingPacket)
        {
            if(!PlayerControlled && !IsBankNode)
            {
                float totalDamage = Mathf.Max(damageReceived - Defense, 0f);
                if (totalDamage > 0f)
                {
                    _HP -= totalDamage;
                    if (_HP <= 0)
                    {
                        _HP = _HPmax; // we don't care as you can't lose control ?
                        PlayerControlled = true;
                        attackingPacket.OnCapture();
                    }
                    if (IsSelectedNode)
                        Statics.UIManager.UpdateNameAndHP();
                }
                TimeSinceLastHit = 0f;
            }
            // else weird, shouldn't happen
        }
        bool IsFullLife()
        {
            return _HP == _HPmax;
        }
#endregion

        #region Packet methods
        public void ReceivePacket(string linkName, Packet newPacket)
        {
            if(_NeighborNodes.Exists(e => e.Link.name == linkName && !e.PacketEntryBlocked))
            {
                PacketsToTreat.Add(newPacket);
            }
        }

        public void LogPacket(Packet packet, TransferType type)
        {
            PacketsHistory.Add((GameManager.TimeSinceStart, packet, type));
            if(IsSelectedNode)
            {
                // Update UI !
                Statics.UIManager.UpdatePacketHistory();
            }
        }
        #endregion

        public void TryLinkingToNeighborNode(Node otherNode, GameObject parentLink, float distanceMax)
        {
            if (_NeighborNodes.Exists(x => x.Node.ID == otherNode.ID)) return;
            Vector2 ourPosition = transform.position;
            Vector2 theirPosition = otherNode.transform.position;
            float distance = Vector2.Distance(ourPosition, theirPosition);
            if (distance <= distanceMax)
            {
                GameObject gameObject = Instantiate(Statics.LineGameObject, parentLink.transform);
                gameObject.name = $"Link {ID}-{otherNode.ID}";
                Link link = gameObject.GetComponent<Link>();
                link.Node1 = this;
                link.Node2 = otherNode;

                _NeighborNodes.Add(new Neighbor(link, otherNode));
                otherNode._NeighborNodes.Add(new Neighbor(link, this));
            }
        }

        #region Neighbor class

        public class Neighbor
        {
            public Link Link;
            public Node Node;
            public bool PacketEntryBlocked;

            public Neighbor(Link aLink, Node anode, bool firewallBlocked = false)
            {
                Link = aLink; Node = anode; PacketEntryBlocked = firewallBlocked;
            }
        }

        #endregion


        #region Update Visuals for node changes

        void UpdateVisualNeighborLines()
        {
            for(int i = 0; i < _NeighborNodes.Count; i++)
                _NeighborNodes[i].Link.UpdateVisuals();
        }
        void UpdateVisuals()
        {
            if (_Drawable != null)
                _Drawable.UpdateVisuals();
        }

        private void OnValidate()
        {
            UpdateVisualNeighborLines();
            UpdateVisuals();
        }

        public void BecomeCurrencyNode()
        {
            IsBankNode = true;
            Color = Color.yellow;
            UpdateVisuals();
        }
        #endregion
    }
}
