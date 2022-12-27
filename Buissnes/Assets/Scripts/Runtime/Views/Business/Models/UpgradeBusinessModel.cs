using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Views.Business.Models
{
    [System.Serializable]
    public class UpgradeBusinessModel
    {
        [SerializeField] private TextMeshProUGUI _nameInfo;
        [SerializeField] private TextMeshProUGUI _incomePercentagesValueInfo;
        [SerializeField] private TextMeshProUGUI _infoStateInfo;
        [SerializeField] private Image _icon;
    }
}