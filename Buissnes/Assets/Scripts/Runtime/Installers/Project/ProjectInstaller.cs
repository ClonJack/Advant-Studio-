using System.Linq;
using Runtime.Configs;
using Runtime.Input;
using Runtime.Interfaces;
using Runtime.Save;
using Runtime.Views.Business;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Runtime.Installers.Project
{
    [CreateAssetMenu(menuName = "Installers/ProjectInstaller", fileName = "ProjectInstaller")]
    public class ProjectInstaller : ScriptableObjectInstaller
    {
        [FormerlySerializedAs("_dataBaseBusiness")] [Header("Data")] [SerializeField] private ConfigBusinesses configBusinesses;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            BindServices();
            BindSignals();
            BindData();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<MasterInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoaderService>().AsSingle();
        }

        private void BindData()
        {
            Container.BindInstance(configBusinesses);
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