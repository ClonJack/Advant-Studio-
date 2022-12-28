using Runtime.Save.ConcreteModel.Business;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Views.Business.ConcreteBussines.Models
{
    [System.Serializable]
    public class UpgradeBusinessModel
    {
        [SerializeField] private TextMeshProUGUI _nameInfo;
        [SerializeField] private TextMeshProUGUI _incomePercentagesValueInfo;
        [SerializeField] private TextMeshProUGUI _infoStateInfo;
        [SerializeField] private Image _icon;

        public void Repaint(UpgradeDataModel upgradeDataModel)
        {
            _nameInfo.text = upgradeDataModel.Name;
            _incomePercentagesValueInfo.text = $"{upgradeDataModel.IncomeMultiplierPercentages}%";
        }
    }
}