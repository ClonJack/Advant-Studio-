using Runtime.Save.ConcreteModel.Business;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Views.Business.ConcreteBussines.Models
{
    [System.Serializable]
    public class GeneralModel
    {
        [SerializeField] private TextMeshProUGUI _nameInfo;
        [SerializeField] private TextMeshProUGUI _lvlInfo;
        [SerializeField] private TextMeshProUGUI _incomeInfo;
        [SerializeField] private Slider _progressBar;

        public void Repaint(ConcreteBusinessDataModel concreteBusinessDataModel)
        {
            _nameInfo.text = concreteBusinessDataModel.Name;
            _lvlInfo.text = concreteBusinessDataModel.Level.ToString();

            var multiplier1 = (concreteBusinessDataModel.UpgradeDataModel1.IncomeMultiplierPercentages / 100f) *
                              concreteBusinessDataModel.BaseCost;
            var multiplier2 = (concreteBusinessDataModel.UpgradeDataModel2.IncomeMultiplierPercentages / 100f) *
                              concreteBusinessDataModel.BaseCost;
            _incomeInfo.text =
                $"{concreteBusinessDataModel.Level * concreteBusinessDataModel.BaseCost * (1 + multiplier1 + multiplier2)}$";

            _progressBar.value = 0;
        }
    }
}