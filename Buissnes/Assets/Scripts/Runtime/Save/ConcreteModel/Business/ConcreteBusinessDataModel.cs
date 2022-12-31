using UnityEngine;

namespace Runtime.Save.ConcreteModel.Business
{
    [System.Serializable]
    public class ConcreteBusinessDataModel
    {
        [Header("General")] public string Name;
        public int DelayedIncoming;
        public float BaseCost;
        public int BaseIncoming;
        public int Level;
        public float Progress;

        [Header("Upgrade 1")] [SerializeField] public UpgradeDataModel UpgradeDataModel1;
        [Header("Upgrade 2")] [SerializeField] public UpgradeDataModel UpgradeDataModel2;
    }
}