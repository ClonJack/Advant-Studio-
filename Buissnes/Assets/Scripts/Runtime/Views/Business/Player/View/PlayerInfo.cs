using TMPro;
using UnityEngine;

namespace Runtime.Views.Business.Player.View
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balance;

        public void RepaintBalance(float balance)
        {
            if (balance < 0)
            {
                _balance.text = "0";
                return;
            }

            _balance.text = $"{balance}$";
        }
    }
}