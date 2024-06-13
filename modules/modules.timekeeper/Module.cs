﻿using modules.timekeeper.services;
using Prism.Ioc;
using Prism.Modularity;

namespace modules.timekeeper
{
    [Module(ModuleName = "TimekeeperModule")]
    public class Module : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) 
            => containerProvider.Resolve<IClockService>();

        public void RegisterTypes(IContainerRegistry containerRegistry) 
            => containerRegistry.RegisterSingleton<IClockService, ClockService>();
    }
}