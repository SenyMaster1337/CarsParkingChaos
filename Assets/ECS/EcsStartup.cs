using Leopotam.Ecs;
using System.Collections.Generic;
using YG;
using UnityEngine;
using System;

public class EcsStartup : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private StaticData _staticData;
    [SerializeField] private SceneData _sceneData;

    [SerializeField] private List<Vehicle> _cars;
    [SerializeField] private List<Passenger> _passengers;

    [SerializeField] private List<RotateTriggerHandler> _triggerHandlers;
    [SerializeField] private CarEnterHandler _carHandler;

    [SerializeField] private List<ParkingSlot> _parkingSlots;
    [SerializeField] private CarToParkingTriggerHandler _parkingTriggerHandler;
    [SerializeField] private StartQueuePoint _startQueuePoint;

    [SerializeField] private RestartButtonClickReader _restartButtonClickReader;
    [SerializeField] private MenuSettingsShower _menuSettingsShower;
    [SerializeField] private LevelCompleteShower _levelCompleteShower;
    [SerializeField] private LevelLossShower _levelLossShower;
    [SerializeField] private LeaderboradShower _leaderboradShower;

    [SerializeField] private GameSounds _gameSounds;

    private EcsWorld _ecsWorld;
    private EcsSystems _systems;

    private void Start()
    {
        _ecsWorld = new EcsWorld();
        _systems = new EcsSystems(_ecsWorld);

        AddInputSystem();
        AddGameSystems();
        AddYGSystems();
        AddCarSystems();
        AddPassengerSystems();
        AddGameSoundSystems();
        AddLevelSystems();
        AddUISystems();

        //TryAddTutorial();

        _systems
            .Inject(_staticData)
            .Inject(_sceneData)
            .Inject(_mainCamera);

        _systems.Init();
    }

    private void Update()
    {
        _systems.Run();
    }

    private void OnDestroy()
    {
        _systems?.Destroy();
        _systems = null;
        _ecsWorld?.Destroy();
        _ecsWorld = null;
    }

    private void AddInputSystem()
    {
        if (YG2.envir.isDesktop)
            _systems.Add(new DesktopInputSystem());
        else
            _systems.Add(new MobileInputSystem());
    }

    private void AddGameSystems()
    {
        _systems
            .Add(new GameInitSystem(_cars, _passengers, _parkingSlots, _startQueuePoint))
            .Add(new RaycastReaderSystem())
            .Add(new CarParkingSystem(_carHandler))
            .Add(new ParkingReservationSystem())
            .Add(new PassengerBoardingSystem(_passengers, _parkingTriggerHandler))
            .Add(new ShiftQueuePassengersSystem(_passengers))
            .Add(new TimerSystem())
            .Add(new DisableUnitSystem())
            .Add(new CooldownSystem());
    }

    private void AddYGSystems()
    {
        _systems
            .Add(new YGPlayerInitSystem())
            .Add(new YGPlayerSaveProgressSystem())
            .Add(new YGAdvShowSystem());
    }

    private void AddCarSystems()
    {
        _systems
            .Add(new CarMoveSystem())
            .Add(new CarCrashHandlerSystem(_cars))
            .Add(new CarLockSystem())
            .Add(new CarRotatorSystem(_triggerHandlers))
            .Add(new AnimatedCarSystem())
            .Add(new CarSoundSystem());
    }

    private void AddPassengerSystems()
    {
        _systems
            .Add(new PassengerMoveSystem())
            .Add(new AnimatedPassengerSystem());
    }

    private void AddGameSoundSystems()
    {
        _systems
            .Add(new GameSoundInitSystem(_gameSounds))
            .Add(new GameSoundSystem());
    }

    private void AddLevelSystems()
    {
        _systems
            .Add(new LevelProgressSystem(_passengers))
            .Add(new LoadNextLevelSystem())
            .Add(new LevelLossSystem())
            .Add(new LevelRestartSystem());
    }

    private void AddUISystems()
    {
        _systems
            .Add(new UIElemntInitSystem(_menuSettingsShower, _levelCompleteShower, _levelLossShower, _leaderboradShower))
            .Add(new PlayerUIButtonReaderSystem(_menuSettingsShower, _restartButtonClickReader, _levelCompleteShower, _levelLossShower, _leaderboradShower))
            .Add(new SettingsSystem())
            .Add(new LeaderboardSystem());
    }

    private void TryAddTutorial()
    {
        if (YG2.saves.level == 1)
        {
            _systems
                .Add(new TutorialInitSystem(_cars))
                .Add(new TutorialSystem(_cars));
        }
    }
}
