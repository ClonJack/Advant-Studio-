using UnityEngine;
namespace Runtime.Views.Business.ConcreteBusiness.Views
{
    public class BusinessView : MonoBehaviour
    {
        [Header("General Info"), SerializeField]
        private GeneralView _generalView;

        [Header("Button Upgrade"), SerializeField]
        private UpgradeLevelView _upgradeLevelView;

        [Header("Left Button Upgrade"), SerializeField]
        private UpgradeBusinessView _upgradeBusinessViewRight;

        [Header("Right Button Upgrade"), SerializeField]
        private UpgradeBusinessView _upgradeBusinessViewLeft;

        public GeneralView GeneralView => _generalView;
        public UpgradeLevelView UpgradeLevelView => _upgradeLevelView;
        public UpgradeBusinessView UpgradeBusinessViewRight => _upgradeBusinessViewRight;
        public UpgradeBusinessView UpgradeBusinessViewLeft => _upgradeBusinessViewLeft;
    }
}