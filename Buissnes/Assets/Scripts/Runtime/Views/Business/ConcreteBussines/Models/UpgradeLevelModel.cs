using Runtime.Save.ConcreteModel.Business;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Views.Business.ConcreteBussines.Models
{
    [System.Serializable]
    public class UpgradeLevelModel
    {
        [SerializeField] private TextMeshProUGUI _priceInfo;
        [SerializeField] private Image _icon;

        public void Repaint(ConcreteBusinessDataModel concreteBusinessDataModel)
        {
            _priceInfo.text = $"{(concreteBusinessDataModel.Level + 1) * concreteBusinessDataModel.BaseCost}$";
        }
    }
}