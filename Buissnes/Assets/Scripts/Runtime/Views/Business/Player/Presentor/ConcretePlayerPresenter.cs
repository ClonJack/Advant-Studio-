using Runtime.Save;
using Runtime.Save.ConcreteModel.Player;
using Runtime.Views.Business.Player.Models;
using Runtime.Views.Business.Player.View;
using UniRx;
using Zenject;

namespace Runtime.Views.Business.Player.Presentor
{
    public class ConcretePlayerPresenter
    {
        private ConcreteReactivePlayerModel _reactivePlayer;

        private ConcretePlayerModel _concretePlayerModel;
        private readonly PlayerInfo _playerInfo;
        private readonly SignalBus _signalBus;

        public ConcreteReactivePlayerModel ReactivePlayer => _reactivePlayer;
        public ConcretePlayerModel ConcretePlayer => _concretePlayerModel;

        public ConcretePlayerPresenter(ConcretePlayerModel concretePlayerModel, PlayerInfo playerInfo,
            SignalBus signalBus)
        {
            _concretePlayerModel = concretePlayerModel;
            _playerInfo = playerInfo;
            _signalBus = signalBus;

            _reactivePlayer = new ConcreteReactivePlayerModel(concretePlayerModel);
        }

        public void StartHandler()
        {
            Balance();
        }


        public void OnUpdateBalance(float money)
        {
            _reactivePlayer.Balance.Value += money;
            _concretePlayerModel.Balance = _reactivePlayer.Balance.Value;
        }

        private void Balance()
        {
            _reactivePlayer.Balance.ObserveEveryValueChanged(x => x.Value).Subscribe(x =>
            {
                if (_reactivePlayer.Balance.Value < 0)
                {
                    _reactivePlayer.Balance.Value = 0;
                    return;
                }
                _playerInfo.RepaintBalance(_reactivePlayer.Balance.Value);
             //   GameSaver.PlayerSaver.Save(_concretePlayerModel);
            }).AddTo(_playerInfo);
        }
    }
}