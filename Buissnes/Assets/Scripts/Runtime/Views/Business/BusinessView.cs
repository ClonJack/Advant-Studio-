using Runtime.Views.Business.Models;
using UnityEngine;

namespace Runtime.Views.Business
{
    public class BusinessView : MonoBehaviour
    {
        [Header("General Info"), SerializeField]
        private GeneralModel _generalModel;

        [Header("Button Upgrade"), SerializeField]
        private UpgradeLevelModel _upgradeLevelModel;

        [Header("Left Button Upgrade"), SerializeField]
        private UpgradeBusinessModel _upgradeBusinessModelRight;

        [Header("Right Button Upgrade"), SerializeField]
        private UpgradeBusinessModel _upgradeBusinessModelLeft;
    }
}