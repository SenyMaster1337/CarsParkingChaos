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
    [SerializeField] private SoundMuteToggle _soundMuteToggle;
    [SerializeField] private LevelCompleteShower _levelCompleteShower;
    [SerializeField] private LevelLossShower _levelLossShower;
    [SerializeField] private LeaderboradShower _leaderboradShower;
    [SerializeField] private CurrentCoinCountText _coinCountText;
    [SerializeField] private ShopShower _shopShower;

    [SerializeField] private GameSounds _gameSounds;

    private EcsWorld _ecsWorld;
    private EcsSystems _systems;

    private void Start()
    {
        _ecsWorld = new EcsWorld();
        _systems = new EcsSystems(_ecsWorld);

        AddYGSystems();

        AddInputSystem();
        AddGameSystems();

        AddCarSystems();
        AddPassengerSystems();
        AddParkingSystems();

        AddLevelSoundSystems();
        AddLevelSystems();

        AddCurrencySystems();
        AddShowCurrencySystems();

        AddSettingSystems();
        AddButtonsUISystems();

        AddShowPassengerSortingSystems();
        AddPassengerSortingSystems();

        _systems
            .Add(new ShopShowerInitSystem(_shopShower))
            .Add(new ShopShowerSystem());

        _systems
            .Add(new PassengerShuffleShowerInitSystem(_shopShower.BuyPassengerShuffleShower))
            .Add(new PassengerShuffleShowerSystem());

        TryAddTutorial();

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
            .Add(new RaycastReaderSystem())
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
            .Add(new YGAdvShowSystem())
            .Add(new YGLeaderboardShowInitSystem(_leaderboradShower))
            .Add(new YGShowLeaderboardSystem())
            .Add(new YGLeaderboardSystem());
    }

    private void AddCarSystems()
    {
        _systems
            .Add(new CarsInitSystem(_cars))
            .Add(new CarMoveSystem())
            .Add(new CarCrashHandlerSystem(_cars))
            .Add(new CarSystem())
            .Add(new CarRotatorSystem(_triggerHandlers))
            .Add(new AnimatedCarSystem())
            .Add(new CarSoundSystem())
            .Add(new CarEffectsSystem());
    }

    private void AddPassengerSystems()
    {
        _systems
            .Add(new PassengersInitSystem(_passengers, _startQueuePoint))
            .Add(new PassengerMoveSystem())
            .Add(new AnimatedPassengerSystem());
    }

    private void AddParkingSystems()
    {
        _systems
            .Add(new ParkingInitSystem(_parkingSlots))
            .Add(new CarParkingSystem(_carHandler))
            .Add(new ParkingReservationSystem());
    }

    private void AddLevelSoundSystems()
    {
        _systems
            .Add(new LevelSoundInitSystem(_gameSounds))
            .Add(new LevelSoundSystem());
    }

    private void AddLevelSystems()
    {
        _systems
            .Add(new LevelInitSystem())
            .Add(new LevelShowInitSystem(_levelCompleteShower, _levelLossShower))
            .Add(new LevelProgressSystem(_passengers))
            .Add(new LoadNextLevelSystem())
            .Add(new LevelLossSystem())
            .Add(new LevelRestartSystem());
    }

    private void AddButtonsUISystems()
    {
        _systems
            .Add(new PlayerUIButtonReaderSystem(_soundMuteToggle, _restartButtonClickReader, _levelCompleteShower, _levelLossShower, _leaderboradShower, _shopShower));
    }

    private void AddSettingSystems()
    {
        _systems
            .Add(new SoundMuteToggleInitSystem(_soundMuteToggle))
            .Add(new SoundMuteToggleSystem());
    }

    private void AddShowCurrencySystems()
    {
        _systems
            .Add(new CurrencyShowInitSystem(_coinCountText))
            .Add(new CurrencyShowSystem());
    }

    private void AddCurrencySystems()
    {
        _systems
            .Add(new CurrencyInitSystem())
            .Add(new CurrencySystem());
    }

    private void AddShowPassengerSortingSystems()
    {
        _systems
            .Add(new PassengerSortingShowerInitSystem(_shopShower.BuyPassengerSortingShower))
            .Add(new PassengerSortingShowerSystem());
    }

    private void AddPassengerSortingSystems()
    {
        _systems
            .Add(new PassengerSortingInitSystem())
            .Add(new PassengerSortingSystem());
    }

    private void TryAddTutorial()
    {
        if (_sceneData.TutorialEnabe)
        {
            _systems
                .Add(new TutorialInitSystem(_cars))
                .Add(new TutorialSystem(_cars));
        }
    }
}
