using Runtime.Save.ConcreteModel.Player;
using UniRx;

namespace Runtime.Views.Business.Player.Models
{
    public class ConcreteReactivePlayerModel
    {
        public ReactiveProperty<float> Balance = new ReactiveProperty<float>();

        public ConcreteReactivePlayerModel(ConcretePlayerModel concretePlayerModel)
        {
            Balance.Value = concretePlayerModel.Balance;
        }
    }
}