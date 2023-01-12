using Runtime.Views.Screens;
using UnityEngine;
using Zenject;

namespace Runtime.Installers.Project
{
    [CreateAssetMenu(menuName = "Installers/UIPrefabInstaller", fileName = "UIPrefabInstaller")]
    public class GameSceneUIInstaller : ScriptableObjectInstaller
    {
        [Header("Base")] [SerializeField] private ScreenServiceView _screens;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ScreenServiceView>().FromComponentInNewPrefab(_screens).AsSingle();
        }
    }
}