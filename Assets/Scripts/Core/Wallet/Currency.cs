using UnityEngine;

namespace CastleFight.Core.Wallet
{
    [CreateAssetMenu(fileName = "Currency", menuName = "Config/Wallet/Currency", order = 0)]
    public class Currency : ScriptableObject, ICurrency
    {
        [SerializeField] private string currencyName;
    }
}