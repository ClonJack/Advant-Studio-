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

            UpgradeButtonLevel();
            RepaintLevel();
        }

        private void RepaintLevel()
        {
            _concreteBusinessPresenters.ReactiveBusinessModel.Level.ObserveEveryValueChanged(x => x.Value).Subscribe(
                    newLevel =>
                    {
                        _concreteBusinessPresenters.СoncreteBusinessDataModel.Level = newLevel;
                        _concreteBusinessPresenters.BusinessView.GeneralView.RepaintLevelInfo(newLevel.ToString());
                        RepaintPriceLevel(newLevel, _concreteBusinessPresenters.ReactiveBusinessModel.BaseCost.Value);
                    })
                .AddTo(_concreteBusinessPresenters.BusinessView);
        }

        private void UpgradeButtonLevel()
        {
            _businessView.UpgradeLevelView.Icon.OnPointerClickAsObservable()
                .Subscribe((data => OnClickUpgradeLevel(data).Forget()))
                .AddTo(_businessView);
        }

        private void RepaintPriceLevel(int level, float baseCost)
        {
            var price = (level + 1) * baseCost;
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