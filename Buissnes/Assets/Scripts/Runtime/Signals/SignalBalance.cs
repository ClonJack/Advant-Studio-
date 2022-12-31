using Runtime.Interfaces;

namespace Runtime.Signals
{
    public struct SignalBalance : ISignal
    {
        public float Money;
        public SignalBalance(float money)
        {
            Money = money;
        }
    }
}