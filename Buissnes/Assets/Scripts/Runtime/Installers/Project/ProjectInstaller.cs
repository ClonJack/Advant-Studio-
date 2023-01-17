using Runtime.Save;
using UnityEngine;
using Zenject;

namespace Runtime.Installers.Project
{
    [CreateAssetMenu(menuName = "Installers/ProjectInstaller", fileName = "ProjectInstaller")]
    public class ProjectInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
            
            Application.targetFrameRate = 120;
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<LoaderAndSaverService>().AsSingle();
        }
    }
}