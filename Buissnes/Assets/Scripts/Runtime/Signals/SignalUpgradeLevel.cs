using Runtime.Interfaces;
namespace Runtime.Signals
{
    public  struct SignalUpgradeLevel : ISignal
    {
        public float Price;
        public SignalUpgradeLevel(float price )
        {
            Price = price;
        }
    }
}