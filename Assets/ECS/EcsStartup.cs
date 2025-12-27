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
    [SerializeField] private Passenger _passengerPrefab;

    [SerializeField] private List<ParkingSlot> _parkingSlots;
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
    [SerializeField] private LevelCarsMaterial _levelCarsMaterial;

    private List<Passenger> _passengers;

    private EcsWorld _ecsWorld;
    private EcsSystems _updateSystems;
    private EcsSystems _fixedUpdateSystems;

    private void Start()
    {
        _ecsWorld = new EcsWorld();
        _updateSystems = new EcsSystems(_ecsWorld);
        _fixedUpdateSystems = new EcsSystems(_ecsWorld);

        AddYGSystems();

        AddCarSystems();
        AddPassengerSystems();
        AddParkingSystems();

        AddInputSystem();
        AddGameSystems();

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

        _updateSystems
            .Inject(_staticData)
            .Inject(_sceneData)
            .Inject(_passengers)
            .Inject(_mainCamera);

        _fixedUpdateSystems
            .Inject(_staticData);

        _updateSystems.Init();
        _fixedUpdateSystems.Init();
    }

    private void Update()
    {
        _updateSystems.Run();
    }

    private void FixedUpdate()
    {
        _fixedUpdateSystems.Run();
    }

    private void OnDestroy()
    {
        _updateSystems?.Destroy();
        _updateSystems = null;
        _fixedUpdateSystems?.Destroy();
        _fixedUpdateSystems = null;
        _ecsWorld?.Destroy();
        _ecsWorld = null;
    }

    private void AddYGSystems()
    {
        _updateSystems
            .Add(new YGPlayerDeviceInitSystem())
            .Add(new YGPlayerSaveProgressSystem())
            .Add(new YGAdvShowSystem())
            .Add(new YGLeaderboardShowInitSystem(_leaderboradShower))
            .Add(new YGShowLeaderboardSystem())
            .Add(new YGLeaderboardSystem());
    }

    private void AddInputSystem()
    {
        if (YG2.envir.isDesktop)
            _updateSystems.Add(new DesktopInputSystem());
        else
            _updateSystems.Add(new MobileInputSystem());
    }
    private void AddCarSystems()
    {
        _updateSystems
            .Add(new CarsInitSystem(_cars))
            .Add(new CarCrashHandlerSystem(_cars))
            .Add(new CarLeavingInitSystem(_cars))
            .Add(new CarLeavingSystem())
            .Add(new CarRotatorSystem(_cars))
            .Add(new AnimatedCarSystem())
            .Add(new CarSoundSystem())
            .Add(new CarEffectsSystem());

        _fixedUpdateSystems
            .Add(new CarMoveInitSystem(_cars))
            .Add(new CarMoveSystem());

        if (_sceneData.RandomColorCarsEnabled)
        {
            _updateSystems
                .Add(new CarsRandomMaterialInitSystem(_cars))
                .Add(new CarsRandomMaterialSystem());
        }
    }

    private void AddPassengerSystems()
    {
        var passengerSpawnSystem = new PassengerSpawnSystem(_cars, _passengerPrefab);
        _passengers = passengerSpawnSystem.Passengers;

        _updateSystems
            .Add(passengerSpawnSystem)
            .Add(new PassengersInitSystem(_startQueuePoint))
            .Add(new PassengerMoveSystem())
            .Add(new AnimatedPassengerSystem());
    }

    private void AddParkingSystems()
    {
        _updateSystems
            .Add(new ParkingReservationInitSystem(_parkingSlots))
            .Add(new CarParkingSystem(_cars))
            .Add(new ParkingReservationSystem());
    }

    private void AddGameSystems()
    {
        _updateSystems
            .Add(new RaycastReaderSystem())
            .Add(new PassengerBoardingSystem(_cars))
            .Add(new ShiftQueuePassengersSystem())
            .Add(new TimerSystem())
            .Add(new DisableUnitSystem())
            .Add(new CooldownSystem());
    }

    private void AddLevelSoundSystems()
    {
        _updateSystems
            .Add(new LevelSoundInitSystem(_gameSounds))
            .Add(new LevelSoundSystem());
    }

    private void AddLevelSystems()
    {
        _updateSystems
            .Add(new LevelInitSystem())
            .Add(new LevelShowInitSystem(_levelCompleteShower, _levelLossShower, _levelCurrentShower))
            .Add(new LevelUIButtonsReaderSystem(_levelCompleteShower, _levelLossShower))
            .Add(new LevelProgressSystem())
            .Add(new LoadNextLevelSystem())
            .Add(new LevelLossShowerSystem())
            .Add(new LevelRestartSystem());
    }

    private void AddButtonsUISystems()
    {
        _updateSystems
            .Add(new PlayerUIButtonReaderSystem(_soundMuteToggle, _restartButtonClickReader, _levelCompleteShower, _levelLossShower, _leaderboradShower, _shopShower));
    }

    private void AddSettingSystems()
    {
        _updateSystems
            .Add(new SoundMuteToggleInitSystem(_soundMuteToggle))
            .Add(new SoundMuteToggleSystem());
    }

    private void AddShowCurrencySystems()
    {
        _updateSystems
            .Add(new CurrencyShowInitSystem(_coinCountText))
            .Add(new CurrencyShowSystem());
    }

    private void AddCurrencySystems()
    {
        _updateSystems
            .Add(new CurrencyInitSystem())
            .Add(new CurrencySystem());
    }

    private void AddShopSystem()
    {
        _updateSystems
            .Add(new ShopShowerInitSystem(_shopShower))
            .Add(new ShopShowerSystem());
    }

    private void AddShuffleSystem()
    {
        _updateSystems
            .Add(new ShuffleInitSystem(_cars))
            .Add(new ShuffleSystem())
            .Add(new CarShuffleShowerInitSystem(_shopShower.BuyPassengerShuffleShower))
            .Add(new CarShuffleShowerSystem())
            .Add(new CarShuffleUIButtonsReader(_shopShower.BuyPassengerShuffleShower));
    }

    private void AddPassengerCountShowerSystems()
    {
        _updateSystems
            .Add(new PassengersCountShowerInitSystem(_passengersCountText))
            .Add(new PassengersCountShowerSystem());
    }

    private void AddUnlcokParkingSlotSystems()
    {
        _updateSystems
            .Add(new UnlockParkingSlotSystem())
            .Add(new UnlockParkingSlotShowerInitSystem(_advUnlockParkingSlotShower))
            .Add(new UnlockParkingSlotShowerButtonReaderSystem(_advUnlockParkingSlotShower))
            .Add(new UnlockParkingSlotShowerSystem());
    }

    private void AddPassengerSortingSystems()
    {
        _updateSystems
            .Add(new PassengerSortingSystem())
            .Add(new PassengerSortingUIButtonsReader(_shopShower.BuyPassengerSortingShower))
            .Add(new PassengerSortingShowerInitSystem(_shopShower.BuyPassengerSortingShower))
            .Add(new PassengerSortingShowerSystem());
    }

    private void TryAddTutorial()
    {
        if (_sceneData.TutorialEnabed)
        {
            _updateSystems
                .Add(new TutorialInitSystem(_cars))
                .Add(new TutorialSystem(_cars));
        }
    }
}
