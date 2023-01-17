using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Views.Business.ConcreteBusiness.Views
{
    [System.Serializable]
    public class GeneralView
    {
        [SerializeField] private TextMeshProUGUI _nameInfo;
        [SerializeField] private TextMeshProUGUI _lvlInfo;
        [SerializeField] private TextMeshProUGUI _incomeInfo;
        [SerializeField] private Slider _progressBar;
        public void RepaintNameInfo(string name) => _nameInfo.text = name;
        public void RepaintLevelInfo(string levelInfo) => _lvlInfo.text = levelInfo;
        public void RepaintIncomeInfo(string incomeInfo) => _incomeInfo.text = $"{incomeInfo}$";
        public void RepaintProgressBar(float value) => _progressBar.value = value;
        public Slider ProgressBar => _progressBar;
    }
}