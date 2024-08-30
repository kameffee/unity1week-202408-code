using Unity1week.Audio;
using Unity1week202408.Anomaly;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202408.Installer
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private AudioLifetimeScope _audioLifetimeScope;

        [SerializeField]
        private AnomalyMasterDataStoreSource _anomalyMasterMasterDataStoreSource;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.unityLogger.logEnabled = Debug.isDebugBuild;
            
            _audioLifetimeScope.Configure(builder);

            ConfigureAnomaly(builder);
        }

        private void ConfigureAnomaly(IContainerBuilder builder)
        {
            builder.RegisterInstance(_anomalyMasterMasterDataStoreSource);
            builder.Register<AnomalyMasterDataRepository>(Lifetime.Singleton);
        }
    }
}