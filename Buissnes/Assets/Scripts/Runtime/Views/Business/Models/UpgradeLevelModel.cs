using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Views.Business.Models
{
    [System.Serializable]
    public class UpgradeLevelModel
    {
        [SerializeField] private TextMeshProUGUI _priceInfo;
        [SerializeField] private Image _icon;
    }
}