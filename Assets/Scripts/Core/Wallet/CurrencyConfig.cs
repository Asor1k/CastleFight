using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Core.Wallet
{
    [CreateAssetMenu(fileName = "CurrencyConfig", menuName = "Config/CurrencyConfig", order = 0)]
    public class CurrencyConfig : ScriptableObject
    {
        [SerializeField] private Currency currency;
        [SerializeField] private int value;

        public KeyValuePair<Currency, int> GetConfig => new KeyValuePair<Currency, int>(currency, value);
    }
}