using Runtime.Views.Screens;
using UnityEngine;
using Zenject;

namespace Runtime.Installers.Project
{
    [CreateAssetMenu(menuName = "Installers/UIPrefabInstaller", fileName = "UIPrefabInstaller")]
    public class ProjectUIPrefabInstaller : ScriptableObjectInstaller
    {
        [Header("Base")] [SerializeField] private ScreenView _screens;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ScreenView>().FromComponentInNewPrefab(_screens).AsSingle();
        }
    }
}