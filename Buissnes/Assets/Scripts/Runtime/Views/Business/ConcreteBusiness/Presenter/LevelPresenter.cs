using Cysharp.Threading.Tasks;
using Runtime.Save.ConcreteModel.Business;
using Runtime.Views.Business.ConcreteBusiness.Models;
using Runtime.Views.Business.ConcreteBusiness.Views;
using Runtime.Views.Business.Player.Presentor;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;

namespace Runtime.Views.Business.ConcreteBusiness.Presenter
{
    public class LevelPresenter
    {
        private readonly ConcreteBusinessPresenters _concreteBusinessPresenters;
        private ConcretePlayerPresenter _playerPresenter => _concreteBusinessPresenters.ConcretePlayerPresenter;

        private ConcreteBusinessDataModel _businessModel =>
            _concreteBusinessPresenters.СoncreteBusinessDataModel;

        private ConcreteReactiveBusinessModel _reactiveBusinessModel =>
            _concreteBusinessPresenters.ReactiveBusinessModel;

        private BusinessView _businessView => _concreteBusinessPresenters.BusinessView;

        public LevelPresenter(ConcreteBusinessPresenters concreteBusinessPresenters)
        {
            _concreteBusinessPresenters = concreteBusinessPresenters;
        }

        public void Start()
        {
            _businessView.UpgradeLevelView.Icon.OnPointerClickAsObservable()
                .Subscribe((data => OnClickUpgradeLevel(data).Forget()))
                .AddTo(_businessView);

            _concreteBusinessPresenters.ReactiveBusinessModel.Level.ObserveEveryValueChanged(x => x.Value).Subscribe(
                    x =>
                    {
                        _concreteBusinessPresenters.СoncreteBusinessDataModel.Level =
                            _concreteBusinessPresenters.ReactiveBusinessModel.Level.Value;
                        _concreteBusinessPresenters.BusinessView.GeneralView.RepaintLevelInfo(x.ToString());
                        RepaintPriceLevel();
                    })
                .AddTo(_concreteBusinessPresenters.BusinessView);
        }

        private void RepaintPriceLevel()
        {
            var price = (_concreteBusinessPresenters.ReactiveBusinessModel.Level.Value + 1) *
                        _concreteBusinessPresenters.СoncreteBusinessDataModel.BaseCost;
            _concreteBusinessPresenters.BusinessView.UpgradeLevelView.RepaintPrice(price);
        }
        private async UniTaskVoid OnClickUpgradeLevel(PointerEventData x)
        {
            var priceLevel = (_reactiveBusinessModel.Level.Value + 1) * _businessModel.BaseCost;
            if (!(_playerPresenter.ReactivePlayer.Balance.Value >= priceLevel)) return;

            _playerPresenter.ReactivePlayer.Balance.Value -= priceLevel;
            _reactiveBusinessModel.Level.Value++;

            await _businessView.UpgradeLevelView.Select();
            _businessView.GeneralView.RepaintIncomeInfo($"{_concreteBusinessPresenters.GetIncoming()}");
            await _businessView.UpgradeLevelView.Deselect();
        }
    }
}