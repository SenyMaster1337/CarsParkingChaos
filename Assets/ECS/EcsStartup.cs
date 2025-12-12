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
    [SerializeField] private LevelCurrentShower _levelCurrentShower;
    [SerializeField] private LevelLossShower _levelLossShower;
    [SerializeField] private LeaderboradShower _leaderboradShower;
    [SerializeField] private CurrentCoinCountText _coinCountText;
    [SerializeField] private ShopShower _shopShower;
    [SerializeField] private ADVUnlockParkingSlotShower _advUnlockParkingSlotShower;
    [SerializeField] private PassengersCountText _passengersCountText;

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

        AddShopSystem();
        AddPassengerSortingSystems();
        AddShuffleSystem();
        AddUnlcokParkingSlotSystems();
        AddPassengerCountShowerSystems();

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
            .Add(new CarLeavingInitSystem(_cars))
            .Add(new CarLeavingSystem())
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
            .Add(new ParkingReservationInitSystem(_parkingSlots))
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
            .Add(new LevelShowInitSystem(_levelCompleteShower, _levelLossShower, _levelCurrentShower))
            .Add(new LevelProgressSystem(_passengers))
            .Add(new LoadNextLevelSystem())
            .Add(new LevelLossShowerSystem())
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

    private void AddShopSystem()
    {
        _systems
            .Add(new ShopShowerInitSystem(_shopShower))
            .Add(new ShopShowerSystem());
    }

    private void AddShuffleSystem()
    {
        _systems
            .Add(new ShuffleInitSystem(_cars, _passengers))
            .Add(new ShuffleSystem())
            .Add(new CarShuffleShowerInitSystem(_shopShower.BuyPassengerShuffleShower))
            .Add(new CarShuffleShowerSystem())
            .Add(new CarShuffleUIButtonsReader(_shopShower.BuyPassengerShuffleShower));
    }

    private void AddPassengerCountShowerSystems()
    {
        _systems
            .Add(new PassengersCountShowerInitSystem(_passengers, _passengersCountText))
            .Add(new PassengersCountShowerSystem());
    }

    private void AddUnlcokParkingSlotSystems()
    {
        _systems
            .Add(new UnlockParkingSlotSystem())
            .Add(new UnlockParkingSlotShowerInitSystem(_advUnlockParkingSlotShower))
            .Add(new UnlockParkingSlotShowerButtonReaderSystem(_advUnlockParkingSlotShower))
            .Add(new UnlockParkingSlotShowerSystem());
    }

    private void AddPassengerSortingSystems()
    {
        _systems
            .Add(new PassengerSortingSystem())
            .Add(new PassengerSortingUIButtonsReader(_shopShower.BuyPassengerSortingShower))
            .Add(new PassengerSortingShowerInitSystem(_shopShower.BuyPassengerSortingShower))
            .Add(new PassengerSortingShowerSystem());
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
