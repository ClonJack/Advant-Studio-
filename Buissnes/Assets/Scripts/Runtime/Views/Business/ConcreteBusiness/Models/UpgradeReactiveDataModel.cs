using Runtime.Save.ConcreteModel.Business;
using UniRx;
using UnityEngine;

namespace Runtime.Views.Business.ConcreteBusiness.Models
{
    [System.Serializable]
    public class UpgradeReactiveDataModel
    {
        public ReactiveProperty<string> Name = new ReactiveProperty<string>();
        public ReactiveProperty<float> Price = new ReactiveProperty<float>();
        public ReactiveProperty<int> IncomeMultiplierPercentages = new ReactiveProperty<int>();
        public ReactiveProperty<bool> IsPurchased = new ReactiveProperty<bool>();
        public UpgradeReactiveDataModel(UpgradeDataModel upgradeDataModel)
        {
            Name.Value = upgradeDataModel.Name;
            Price.Value = upgradeDataModel.Price;
            IncomeMultiplierPercentages.Value = upgradeDataModel.IncomeMultiplierPercentages;
            IsPurchased.Value = upgradeDataModel.IsPurchased;
        }
    }
}