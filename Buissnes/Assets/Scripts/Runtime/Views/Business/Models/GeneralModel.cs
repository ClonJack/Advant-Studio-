using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Views.Business.Models
{
    [System.Serializable]
    public class GeneralModel
    {
        [SerializeField] private TextMeshProUGUI _nameInfo;
        [SerializeField] private TextMeshProUGUI _lvlInfo;
        [SerializeField] private TextMeshProUGUI _incomeInfo;
        [SerializeField] private Slider _progressBar;
    }
}