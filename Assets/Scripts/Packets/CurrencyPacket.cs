using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Helpers.Statics;

namespace Assets.Scripts.Packets
{
    public class CurrencyPacket : Packet
    {
        float moneyAmount;
        Currency currency;
        public CurrencyPacket(List<Node> path, float size, float amount, Currency cur) : base(path, size)
        {
            moneyAmount = amount;
            currency = cur;
        }
        public override string GetNameType()
        {
            return "Currency";
        }

        public override void OnReceive(Node nodeReceiving)
        {
            if(nodeReceiving.IsBankNode)
            {
                switch(currency)
                {
                    case (Currency.CURRENCY1):
                        ClientPlayer.Currency1 += moneyAmount;
                        break;
                    case (Currency.CURRENCY2):
                        ClientPlayer.Currency2 += moneyAmount;
                        break;
                    case (Currency.CURRENCY3):
                        ClientPlayer.Currency3 += moneyAmount;
                        break;
                    case (Currency.CURRENCY4):
                        ClientPlayer.Currency4 += moneyAmount;
                        break;
                    case (Currency.CURRENCY5):
                        ClientPlayer.Currency5 += moneyAmount;
                        break;
                    default:
                        break;
                }
                UIManager.UpdateCurrencies();
            }
        }
    }
}

