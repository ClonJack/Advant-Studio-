using Runtime.Save.ConcreteModel.Business;
using Runtime.Views.Business.ConcreteBusiness.Models;
using Runtime.Views.Business.ConcreteBusiness.Views;
using Runtime.Views.Business.Player.Presentor;
using UniRx;

namespace Runtime.Views.Business.ConcreteBusiness.Presenter
{
    [System.Serializable]
    public class ConcreteBusinessPresenters
    {
        public readonly ConcreteBusinessDataModel СoncreteBusinessDataModel;
        public readonly BusinessView BusinessView;
        public readonly ConcretePlayerPresenter ConcretePlayerPresenter;
        public ConcreteReactiveBusinessModel ReactiveBusinessModel;

        public ConcreteBusinessPresenters(ConcreteBusinessDataModel сoncreteBusinessDataModel,
            BusinessView businessView,
            ConcretePlayerPresenter concretePlayerPresenter)
        {
            СoncreteBusinessDataModel = сoncreteBusinessDataModel;
            BusinessView = businessView;

            ConcretePlayerPresenter = concretePlayerPresenter;

            ReactiveBusinessModel = new ConcreteReactiveBusinessModel(СoncreteBusinessDataModel);
        }
        public void StartHandler()
        {
            NameCompany();

            var upgradeLevel = new LevelPresenter(this);
            upgradeLevel.Start();

            var rightButtonUpgrade = new UpgradeButtonPresenter(ReactiveBusinessModel.UpgradeReactiveDataModel1,
                BusinessView.UpgradeBusinessViewRight,
                СoncreteBusinessDataModel.UpgradeDataModel1, this);
            rightButtonUpgrade.Start();

            var leftButtonUpgrade = new UpgradeButtonPresenter(ReactiveBusinessModel.UpgradeReactiveDataModel2,
                BusinessView.UpgradeBusinessViewLeft,
                СoncreteBusinessDataModel.UpgradeDataModel2, this);
            leftButtonUpgrade.Start();
            
            var progressbar = new ProgressbarBusinessPresenter(this);
            progressbar.Start();
        }
        private void NameCompany()
        {
            ReactiveBusinessModel.Name.ObserveEveryValueChanged(x => x.Value).Subscribe(x =>
                BusinessView.GeneralView.RepaintNameInfo(x)).AddTo(BusinessView);
        }
        public float GetIncoming()
        {
            var income1 = ReactiveBusinessModel.UpgradeReactiveDataModel1.IsPurchased.Value
                ? ReactiveBusinessModel.UpgradeReactiveDataModel1.IncomeMultiplierPercentages.Value
                : 0f;
            var multiplier1 =
                (income1 / 100f) *
                СoncreteBusinessDataModel.BaseCost;

            var income2 = ReactiveBusinessModel.UpgradeReactiveDataModel2.IsPurchased.Value
                ? ReactiveBusinessModel.UpgradeReactiveDataModel2.IncomeMultiplierPercentages.Value
                : 0f;
            var multiplier2 =
                (income2 / 100f) *
                СoncreteBusinessDataModel.BaseCost;

            return
                ReactiveBusinessModel.Level.Value * СoncreteBusinessDataModel.BaseCost *
                (1 + multiplier1 + multiplier2);
        }
    }
}