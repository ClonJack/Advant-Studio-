using UnityEngine;
using Zenject;

namespace Runtime.Installers.Project
{
    [CreateAssetMenu(menuName = "Installers/GameInstaller", fileName = "GameInstaller")]
    public class GameInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
        }
        
        public void BindServices()
        {
          //  Container.BindInterfacesAndSelfTo<
        }
    }
}