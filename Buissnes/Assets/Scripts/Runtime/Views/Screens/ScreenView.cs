using Runtime.Views.Business;
using UnityEngine;
using Zenject;

namespace Runtime.Views.Screens
{
    public class ScreenView : MonoBehaviour,IInitializable
    {
        public BusinessManagementView businessManagementView;
        public void Initialize()
        {
        }
    }
}