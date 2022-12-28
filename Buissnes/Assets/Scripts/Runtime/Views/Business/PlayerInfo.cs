using Runtime.Save.ConcreteModel.Player;
using TMPro;
using UnityEngine;

namespace Runtime.Views.Business
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balance;
        public void Repaint(ConcretePlayerModel playerModel)
        {
            _balance.text = $"{playerModel.Balance}$";
        }
    }
}