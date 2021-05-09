using Assets.Scripts.Cores;
using Assets.Scripts.Helpers;
using Assets.Scripts.Packets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Helpers.Statics;

namespace Assets.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {

        [SerializeField] public int PacketsShownInHistory = 5;
        [SerializeField] public Text NodeName;
        [SerializeField] public Text PacketHistory;
        [SerializeField] public Text CoresDisplay;

        Node _node;

        private void Start()
        {
            Statics.UIManager = this;
        }

        void UpdateUI()
        {
            NodeName.text = _node.name;
            UpdatePacketHistory();
            UpdateCores();
            SetVisibility(true);
        }

        void ClearUI()
        {
            NodeName.text = "";
            PacketHistory.text = "";
            CoresDisplay.text = "";
            SetVisibility(false);
        }

        void SetVisibility(bool enable)
        {
            NodeName.enabled = enable;
            PacketHistory.enabled = enable;
            CoresDisplay.enabled = enable;
        }

        public void UpdatePacketHistory()
        {
            PacketHistory.text = "";
            List<(float, Packet, TransferType)> packetHistory = new List<(float, Packet, TransferType)>(_node.PacketsHistory);
            packetHistory.Reverse();

            // We want to show the history packets from the last one

            for (int i = 0; i < Mathf.Min(PacketsShownInHistory, packetHistory.Count); i++)
            {
                double time = Math.Round((double)packetHistory[i].Item1, 2);
                switch (packetHistory[i].Item3)
                {
                    case (TransferType.SENT):
                        PacketHistory.text += $"{time}s : Packet sent" + Environment.NewLine;
                        break;
                    case (TransferType.RECEIVED):
                        PacketHistory.text += $"{time}s : Packet received !" + Environment.NewLine;
                        break;
                    case (TransferType.TRANSFER):
                        PacketHistory.text += $"{time}s : Packet transfered" + Environment.NewLine;
                        break;
                    default:
                        break;
                }
            }
        }

        public void UpdateCores()
        {
            CoresDisplay.text = "";
            // We want to show the history packets from the last one

            Core[] coreList = _node.Cores.GetCores();
            for (int i = 0; i < coreList.Length; i++)
            {
                if (coreList[i] is CoreEmpty)
                    CoresDisplay.text += "Empty" + Environment.NewLine;
                if (coreList[i] is CoreMining)
                    CoresDisplay.text += "Mining" + Environment.NewLine;
                if (coreList[i] is CoreUnavailable)
                    CoresDisplay.text += "UNAVAILABLE" + Environment.NewLine;
            }
        }

        public void SetNode(Node newNode)
        {
            _node = newNode;
            if (_node != null)
                UpdateUI();
            else
                ClearUI();
        }
    }
}
