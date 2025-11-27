using Leopotam.Ecs;
using System.Collections.Generic;
using YG;
using UnityEngine;
using UnityEngine.Audio;

public class EcsStartup : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private StaticData _staticData;

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

    //[SerializeField] private AudioMixer _audioMixer;

    private EcsWorld _ecsWorld;
    private EcsSystems _systems;

    private void Start()
    {
        _ecsWorld = new EcsWorld();
        _systems = new EcsSystems(_ecsWorld);

        if (YG2.envir.isDesktop)
            _systems.Add(new DesktopInputSystem());
        else
            _systems.Add(new MobileInputSystem());

        _systems
            .Add(new YGInitPlayerSystem())
            .Add(new GameInitSystem(_cars, _passengers, _parkingSlots, _startQueuePoint))
            .Add(new UIElemntInitSystem(_menuSettingsShower, _levelCompleteShower, _levelLossShower, _leaderboradShower))
            .Add(new PlayerUIButtonReaderSystem(_menuSettingsShower, _restartButtonClickReader, _levelCompleteShower, _levelLossShower, _leaderboradShower))
            .Add(new RaycastReaderSystem())
            .Add(new CarMoveSystem())
            .Add(new CarLockSystem())
            .Add(new AnimatedCarSystem())
            .Add(new PassengerMoveSystem())
            .Add(new AnimatedPassengerSystem())
            .Add(new CarRotatorSystem(_triggerHandlers))
            .Add(new CarCrashHandlerSystem(_cars))
            .Add(new CarParkingSystem(_carHandler))
            .Add(new ParkingReservationSystem())
            .Add(new PassengerBoardingSystem(_passengers, _parkingTriggerHandler))
            .Add(new ShiftQueuePassengersSystem(_passengers))
            .Add(new TimerSystem())
            .Add(new DisableUnitSystem())
            .Add(new LevelRestartSystem())
            .Add(new LevelProgressSystem(_passengers))
            .Add(new SettingsSystem())
            .Add(new LeaderboardSystem())
            .Add(new LoadNextLevelSystem())
            .Add(new LevelLossSystem())
            .Add(new CooldownSystem());

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
        {
            _systems
                .Add(new TutorialInitSystem(_cars))
                .Add(new TutorialSystem(_cars));
        }

        _systems
            .Inject(_staticData)
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
}
