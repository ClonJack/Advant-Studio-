using Runtime.Save.ConcreteModel.Player;
using Runtime.Views.Business.Player.Models;
using Runtime.Views.Business.Player.View;
using UniRx;

namespace Runtime.Views.Business.Player.Presentor
{
    public class ConcretePlayerPresenter
    {
        private ConcreteReactivePlayerModel _reactivePlayer;
        private ConcretePlayerModel _concretePlayerModel;
        private readonly PlayerView _playerView;
        public ConcreteReactivePlayerModel ReactivePlayer => _reactivePlayer;

        public ConcretePlayerPresenter(ConcretePlayerModel concretePlayerModel, PlayerView playerView)
        {
            _concretePlayerModel = concretePlayerModel;
            _playerView = playerView;

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
            _reactivePlayer.Balance.ObserveEveryValueChanged(x => x.Value).Subscribe(balance =>
            {
                if (balance < 0)
                {
                    _reactivePlayer.Balance.Value = 0;
                    return;
                }

                _playerView.RepaintBalance(_reactivePlayer.Balance.Value);
            }).AddTo(_playerView);
        }
    }
}