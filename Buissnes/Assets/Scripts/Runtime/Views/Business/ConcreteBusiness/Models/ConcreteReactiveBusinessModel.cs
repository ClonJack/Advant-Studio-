using Runtime.Save.ConcreteModel.Business;
using UniRx;

namespace Runtime.Views.Business.ConcreteBusiness.Models
{
    [System.Serializable]
    public class ConcreteReactiveBusinessModel
    {
        public ReactiveProperty<string> Name = new ReactiveProperty<string>();
        public ReactiveProperty<int> DelayedIncoming = new ReactiveProperty<int>();
        public ReactiveProperty<float> BaseCost = new ReactiveProperty<float>();
        public ReactiveProperty<int> BaseIncoming = new ReactiveProperty<int>();
        public ReactiveProperty<int> Level = new ReactiveProperty<int>();
        public ReactiveProperty<float> Progress = new ReactiveProperty<float>();
        public UpgradeReactiveDataModel UpgradeReactiveDataModel1;
        public UpgradeReactiveDataModel UpgradeReactiveDataModel2;

        public ConcreteReactiveBusinessModel(ConcreteBusinessDataModel concreteBusinessDataModel)
        {
            Name.Value = concreteBusinessDataModel.Name;
            DelayedIncoming.Value = concreteBusinessDataModel.DelayedIncoming;
            BaseCost.Value = concreteBusinessDataModel.BaseCost;
            BaseIncoming.Value = concreteBusinessDataModel.BaseIncoming;
            Level.Value = concreteBusinessDataModel.Level;
            Progress.Value = concreteBusinessDataModel.Progress;
            
            UpgradeReactiveDataModel1 = new UpgradeReactiveDataModel(concreteBusinessDataModel.UpgradeDataModel1);
            UpgradeReactiveDataModel2 = new UpgradeReactiveDataModel(concreteBusinessDataModel.UpgradeDataModel2);
        }
    }
}