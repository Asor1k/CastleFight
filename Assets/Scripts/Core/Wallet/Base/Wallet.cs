using System.Collections.Generic;

namespace CastleFight.Core.Wallet
{
    public class Wallet<T> : IWallet<T> where T : ICurrency
    {
        private Dictionary<T, int> balance;

        public Wallet()
        {
            balance = new Dictionary<T, int>();
        }

        public bool CanWithdraw(T currency, int amount)
        {
            if (currency != null && balance.TryGetValue(currency, out var value))
            {
                return value >= amount;
            }

            return false;
        }

        public void TopUp(T currency, int amount = 0)
        {
            if (balance.ContainsKey(currency))
            {
                balance[currency] += amount;
            }
            else
            {
                balance.Add(currency, amount);
            }
        }

        public bool Withdraw(T currency, int amount)
        {
            if (CanWithdraw(currency, amount))
            {
                balance[currency] -= amount;
                return true;
            }

            return false;
        }
    }
}