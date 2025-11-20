using Leopotam.Ecs;
using System.Collections.Generic;
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
    [SerializeField] private MenuSettings _menuSettings;
    //[SerializeField] private AudioMixer _audioMixer;

    private EcsWorld _ecsWorld;
    private EcsSystems _systems;

    private void Start()
    {
        _ecsWorld = new EcsWorld();
        _systems = new EcsSystems(_ecsWorld);

        var input = new InputSystem(_mainCamera);
        var raycastReader = new RaycastReaderSystem(input);

        _systems
            .Inject(_staticData)
            .Add(new GameInitSystem(_staticData, _cars, _passengers, _parkingSlots))
            .Add(input)
            .Add(raycastReader)
            .Add(new CarMoveSystem())
            .Add(new CarLockSystem())
            .Add(new AnimatedCarSystem())
            .Add(new PassengerMoveSystem())
            .Add(new AnimatedPassengerSystem())
            .Add(new CarRotatorSystem(_triggerHandlers))
            .Add(new CarCrashHandlerSystem(_cars))
            .Add(new CarParkingSystem(_carHandler, _parkingTriggerHandler))
            .Add(new ParkingReservationSystem(_parkingSlots, raycastReader))
            .Add(new PassengerBoardingSystem(_passengers, _parkingTriggerHandler, _parkingSlots))
            .Add(new ShiftQueuePassengersSystem(_passengers, _startQueuePoint))
            .Add(new TimerSystem())
            .Add(new DisableUnitSystem())
            .Add(new LevelRestartSystem(_restartButtonClickReader))
            .Add(new PlayerSettingButtonReaderSystem(_menuSettings))
            .Init();


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
