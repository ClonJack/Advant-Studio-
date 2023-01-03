using Runtime.Views.Business;
using UnityEngine;
using Zenject;

namespace Runtime.Views.Screens
{
    public class ScreenServiceView : MonoBehaviour, IInitializable
    {
        [SerializeField] private BusinessManagementView _businessManagementView;
        public void Initialize()
        {
            _businessManagementView.Repaint();
        }
    }
}