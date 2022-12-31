﻿using System.Linq;
using Runtime.Configs;
using Runtime.Configs.Save;
using Runtime.Input;
using Runtime.Interfaces;
using Runtime.Save;
using UnityEngine;
using UnityEngine.Serialization;
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
         
            Application.targetFrameRate = 120;
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<MasterInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoaderAndSaverService>().AsSingle();
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