using System.Linq;
using Runtime.Input;
using Runtime.Interfaces;
using UnityEngine;
using Zenject;

namespace Runtime.Installers.Project
{
    [CreateAssetMenu(menuName = "Installers/ProjectInstaller", fileName = "ProjectInstaller")]
    public class ProjectInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            BindServices();
            BindSignals();
        }


        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<MasterInput>().AsSingle();
        }

        private void BindSignals()
        {
            var types = typeof(ISignal).Assembly.GetTypes()
                .Where(p => typeof(ISignal).IsAssignableFrom(p)
                            && !p.IsInterface
                            && !p.IsAbstract);

            foreach (var type in types) Container.DeclareSignal(type);
        }
    }
}