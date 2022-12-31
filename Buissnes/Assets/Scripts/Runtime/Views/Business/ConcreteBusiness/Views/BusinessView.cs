using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Views.Business.ConcreteBusiness.Views
{
    public class BusinessView : MonoBehaviour
    {
        [FormerlySerializedAs("_generalModel")] [Header("General Info"), SerializeField]
        private GeneralView generalView;

        [FormerlySerializedAs("_upgradeLevelModel")] [Header("Button Upgrade"), SerializeField]
        private UpgradeLevelView upgradeLevelView;

        [FormerlySerializedAs("_upgradeBusinessModelRight")] [Header("Left Button Upgrade"), SerializeField]
        private UpgradeBusinessView upgradeBusinessViewRight;

        [FormerlySerializedAs("_upgradeBusinessModelLeft")] [Header("Right Button Upgrade"), SerializeField]
        private UpgradeBusinessView upgradeBusinessViewLeft;
        
        public GeneralView GeneralView => generalView;
        public UpgradeLevelView UpgradeLevelView => upgradeLevelView;

        public UpgradeBusinessView UpgradeBusinessViewRight => upgradeBusinessViewRight;
        public UpgradeBusinessView UpgradeBusinessViewLeft => upgradeBusinessViewLeft;
        
    }
}