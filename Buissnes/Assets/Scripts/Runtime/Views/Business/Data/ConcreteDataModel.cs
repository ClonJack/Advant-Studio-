using UnityEngine;

namespace Runtime.Views.Business.Data
{
    [System.Serializable]
    public class ConcreteDataModel
    {
        [Header("General")] [SerializeField] private int _delayedIncoming;
        [SerializeField] private int _baseCost;
        [SerializeField] private int _baseIncoming;

        [Header("Upgrade 1")] [SerializeField] private UpgradeDataModel _upgradeDataModel1;
        [Header("Upgrade 2")] [SerializeField] private UpgradeDataModel _upgradeDataModel2;
    }
}