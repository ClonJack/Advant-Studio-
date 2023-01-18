using Cysharp.Threading.Tasks;
using Runtime.Save.ConcreteModel.Business;
using Runtime.Views.Business.ConcreteBusiness.Models;
using Runtime.Views.Business.ConcreteBusiness.Views;
using UniRx;
using UniRx.Triggers;

namespace Runtime.Views.Business.ConcreteBusiness.Presenter
{
    public class UpgradeButtonPresenter
    {
        private readonly ConcreteBusinessPresenters _concreteBusinessPresenters;

        private readonly UpgradeReactiveDataModel _modelUpgrade;
        private readonly UpgradeBusinessView _upgradeView;
        private readonly UpgradeDataModel _upgradeDataModel;
        private BusinessView _businessView => _concreteBusinessPresenters.BusinessView;

        public UpgradeButtonPresenter(UpgradeReactiveDataModel modelUpgrade, UpgradeBusinessView upgradeView,
            UpgradeDataModel upgradeDataModel, ConcreteBusinessPresenters concreteBusinessPresenters)
        {
            _modelUpgrade = modelUpgrade;
            _upgradeView = upgradeView;
            _upgradeDataModel = upgradeDataModel;
            _concreteBusinessPresenters = concreteBusinessPresenters;

            UpgradeButton();
        }

        private void UpgradeButton()
        {
            _modelUpgrade.Name.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(name => _upgradeView.RepaintName(name)).AddTo(_businessView);

            _modelUpgrade.IncomeMultiplierPercentages.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(incomeMultiplierPercentages =>
                    _upgradeView.RepaintIncomePercentages(incomeMultiplierPercentages)).AddTo(_businessView);

            _modelUpgrade.IsPurchased.ObserveEveryValueChanged(x => x.Value).Subscribe(isPurchased =>
            {
                if (isPurchased)
                {
                    _upgradeDataModel.IsPurchased = true;
                    _businessView.GeneralView.RepaintIncomeInfo($"{_concreteBusinessPresenters.GetIncoming()}");
                    _upgradeView.RepaintPurchasedState();
                }
            }).AddTo(_businessView);

            _modelUpgrade.Price.ObserveEveryValueChanged(x => x.Value).Subscribe(price =>
            {
                if (!_modelUpgrade.IsPurchased.Value)
                    _upgradeView.RepaintPrice(price);
                else
                    _upgradeView.RepaintPurchasedState();
            }).AddTo(_businessView);

            _upgradeView.Icon.OnPointerClickAsObservable()
                .Subscribe(x =>
                {
                    if (!_modelUpgrade.IsPurchased.Value)
                        OnClickUpgrade(_modelUpgrade, _upgradeView).Forget();
                }).AddTo(_businessView);
        }

        private async UniTaskVoid OnClickUpgrade(UpgradeReactiveDataModel modelUpgrade, UpgradeBusinessView upgradeView)
        {
            var companyBalance = _concreteBusinessPresenters.ConcretePlayerPresenter.ReactivePlayer.Balance;
            if (companyBalance.Value >= modelUpgrade.Price.Value)
            {
                await upgradeView.Select();
                var playerBalance = _concreteBusinessPresenters.ConcretePlayerPresenter.ReactivePlayer.Balance;
                playerBalance.Value -= modelUpgrade.Price.Value;
                modelUpgrade.IsPurchased.Value = true;
                await upgradeView.Deselect();
            }
        }
    }
}