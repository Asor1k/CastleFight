namespace CastleFight.Core.Wallet
{
    public interface IWallet<T> where T : ICurrency
    {
        bool CanWithdraw(T currency, int amount);
        void TopUp(T currency, int amount = 0);
        bool Withdraw(T currency, int amount);
    }
}