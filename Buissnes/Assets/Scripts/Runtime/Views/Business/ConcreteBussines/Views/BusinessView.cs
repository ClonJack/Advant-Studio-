using Runtime.Save.ConcreteModel.Business;
using Runtime.Views.Business.ConcreteBussines.Models;
using UnityEngine;

namespace Runtime.Views.Business.ConcreteBussines.Views
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


        public void Repaint(ConcreteBusinessDataModel concreteBusinessData)
        {
            _generalModel.Repaint(concreteBusinessData);
            _upgradeLevelModel.Repaint(concreteBusinessData);
            _upgradeBusinessModelRight.Repaint(concreteBusinessData.UpgradeDataModel1);
            _upgradeBusinessModelLeft.Repaint(concreteBusinessData.UpgradeDataModel2);
        }
    }
}