using UnityEngine;

namespace Runtime.Save.ConcreteModel.Business
{
    [System.Serializable]
    public class ConcreteBusinessDataModel
    {
        [Header("General")] public string Name;
        public int DelayedIncoming;
        public int BaseCost;
        public int BaseIncoming;
        public int Level;

        [Header("Upgrade 1")] [SerializeField] public UpgradeDataModel UpgradeDataModel1;
        [Header("Upgrade 2")] [SerializeField] public UpgradeDataModel UpgradeDataModel2;
    }
}