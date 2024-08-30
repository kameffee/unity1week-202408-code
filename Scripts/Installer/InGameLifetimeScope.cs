using Unity1week.Audio;
using Unity1week202408.Anomaly;
using Unity1week202408.Audio;
using Unity1week202408.Calender;
using Unity1week202408.Ending;
using Unity1week202408.Manual;
using Unity1week202408.Menu;
using Unity1week202408.Player;
using Unity1week202408.Stage;
using Unity1week202408.Title;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202408.Installer
{
    public class InGameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private LightController _myRoomLightController;

        [SerializeField]
        private Transform _performCanvasRoot;

        [SerializeField]
        private EndingPerformView _endingPerformViewPrefab;

        [SerializeField]
        private ManualView _manualViewPrefab;

        [SerializeField]
        private InGameMenuView _inGameMenuViewPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<InGameLoop>();
            builder.RegisterComponentInHierarchy<PlayerFollowCamera>();
            builder.RegisterComponentInHierarchy<InGameUIView>();

            ConfigureStage(builder);
            ConfigurePlayer(builder);
            ConfigureAnomaly(builder);
            ConfigureCalender(builder);
            ConfigureCover(builder);
            ConfigureDebug(builder);
            ConfigureEnding(builder);
            ConfigureManual(builder);
            ConfigureMenu(builder);
            ConfigureRecord(builder);
            ConfigureTitle(builder);
        }

        private void ConfigureRecord(IContainerBuilder builder)
        {
            builder.Register<AnomalyRecordRepository>(Lifetime.Singleton);
            builder.Register<AnomalyRecordUseCase>(Lifetime.Singleton);
        }

        private static void ConfigureStage(IContainerBuilder builder)
        {
            builder.Register<Situation>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<NextStagePoint>();
            builder.RegisterComponentInHierarchy<StartPoint>();
            builder.RegisterComponentInHierarchy<StageArea>();
            builder.RegisterComponentInHierarchy<GoalPoint>();
            builder.Register<StageTransitionUseCase>(Lifetime.Singleton);
            builder.Register<TerminationCalculateUseCase>(Lifetime.Singleton);
        }

        private static void ConfigurePlayer(IContainerBuilder builder)
        {
            builder.Register<PlayerService>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<PlayerController>();
            builder.RegisterComponentInHierarchy<PlayerMoveUIView>();
            builder.Register<TeleportPlayerUseCase>(Lifetime.Singleton);
            builder.RegisterEntryPoint<PlayerPresenter>();
        }

        private static void ConfigureAnomaly(IContainerBuilder builder)
        {
            builder.Register<AnomalyInitializer>(Lifetime.Singleton);
            builder.Register<ApplyAnomalyUseCase>(Lifetime.Singleton);
            builder.Register<AnomalyObjectContainer>(Lifetime.Singleton);
            builder.Register<AnomalyLotteryCalculator>(Lifetime.Singleton);
            builder.Register<LotteryAnomalyUseCase>(Lifetime.Singleton);
            builder.Register<AnomalyService>(Lifetime.Singleton);
        }

        private static void ConfigureCalender(IContainerBuilder builder)
        {
            builder.Register<ProgressService>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<CalenderView>();
            builder.RegisterEntryPoint<CalenderPresenter>();
        }

        private void ConfigureCover(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<StageCoverView>();
            builder.RegisterEntryPoint<StageAreaPresenter>()
                .WithParameter<LightController>(_myRoomLightController);
        }

        private static void ConfigureDebug(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<InGameDebugCanvas>();
        }

        private void ConfigureEnding(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<EndingPerformView>();
            builder.RegisterEntryPoint<EndingPerformPresenter>().AsSelf();
            builder.RegisterComponentInNewPrefab<EndingPerformView>(_endingPerformViewPrefab, Lifetime.Singleton)
                .UnderTransform(_performCanvasRoot);
        }

        private void ConfigureManual(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<ManualView>();
            builder.RegisterComponentInHierarchy<GameManualPoint>();
            builder.RegisterComponentInNewPrefab<ManualView>(_manualViewPrefab, Lifetime.Singleton)
                .UnderTransform(_performCanvasRoot);
            builder.RegisterEntryPoint<ManualPresenter>();
        }

        private static void ConfigureMenu(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<InGameMenuView>();
            builder.RegisterEntryPoint<InGameMenuPresenter>();

            builder.RegisterEntryPoint<AudioSettingPresenter>();
            builder.RegisterComponentInHierarchy<AudioSettingView>();

            builder.Register<CreateInGameMenuViewModelUseCase>(Lifetime.Singleton);
        }

        private static void ConfigureTitle(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<TitleView>();
            builder.RegisterEntryPoint<TitlePresenter>().AsSelf();
        }
    }
}