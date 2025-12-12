using Leopotam.Ecs;
using UnityEngine;
using YG;

public class PassengerSortingSystem : IEcsRunSystem
{
    public string RewardID = "PassengerSortingRewardID";

    private const float TimeLeftToVerifyCarsCountInParking = 1f;

    private EcsWorld _ecsWorld;
    private EcsFilter<PassengerSortEvent> _sortEventFilter;
    private EcsFilter<GetUnitsDataEvent> _unitsDataFilter;
    private EcsFilter<ShowAdvToPassengerSortEvent> _showAdvFilter;

    private bool _isNeedConfirmToPay;
    private bool _isNeedAdvShow;

    public PassengerSortingSystem()
    {
        _isNeedConfirmToPay = false;
        _isNeedAdvShow = false;
    }

    public void Run()
    {
        foreach (var sortEntity in _sortEventFilter)
        {
            _ecsWorld.NewEntity().Get<SendRequesGetDataInPassengerBoardingEvent>();
            _isNeedConfirmToPay = true;
            _isNeedAdvShow = false;
            _sortEventFilter.GetEntity(sortEntity).Del<PassengerSortEvent>();
        }

        foreach (var advEntity in _showAdvFilter)
        {
            _ecsWorld.NewEntity().Get<SendRequesGetDataInPassengerBoardingEvent>();
            _isNeedConfirmToPay = false;
            _isNeedAdvShow = true;
            _showAdvFilter.GetEntity(advEntity).Del<ShowAdvToPassengerSortEvent>();
        }

        foreach (var passengersAndCarsDataEntity in _unitsDataFilter)
        {
            var dataEntity = _unitsDataFilter.GetEntity(passengersAndCarsDataEntity);

            if (dataEntity.Has<VerifyCarsToPassengerSortingEvent>())
            {
                ref var dataEvent = ref dataEntity.Get<GetUnitsDataEvent>();

                if (dataEvent.carsOnlyParkingZoneList.Count > 0)
                {
                    TryConfirmPayment();
                    SortPassengers(dataEntity);
                    _ecsWorld.NewEntity().Get<DisableRaycastReaderToggleSwitchMethodEvent>();
                }

                if (dataEvent.carsOnlyParkingZoneList.Count == 0)
                {
                    _ecsWorld.NewEntity().Get<SortPassengerInColorCarsEvent>();
                    _ecsWorld.NewEntity().Get<EnableRaycastReaderEvent>();
                    _ecsWorld.NewEntity().Get<EnableButtonsEvent>();
                    _ecsWorld.NewEntity().Get<EnableRaycastReaderToggleSwitchMethodEvent>();
                    dataEntity.Del<GetUnitsDataEvent>();
                    dataEntity.Del<VerifyCarsToPassengerSortingEvent>();
                }
            }
        }
    }

    private void SortPassengers(EcsEntity dataEntity)
    {
        if (_isNeedAdvShow == true)
        {
            YG2.RewardedAdvShow(RewardID, () =>
            {
                if (RewardID == "PassengerSortingRewardID")
                {
                    PerformSortingIterationVersionTwo(dataEntity);
                }
            });

            _ecsWorld.NewEntity().Get<ClosePassengerSortingInfoShowerEvent>();
            _isNeedAdvShow = false;

            return;
        }

        if (_isNeedConfirmToPay == false)
        {
            PerformSortingIterationVersionTwo(dataEntity);
        }
    }

    private void TryConfirmPayment()
    {
        if (_isNeedConfirmToPay == true)
        {
            var confirmEventNewEntity = _ecsWorld.NewEntity();
            confirmEventNewEntity.Get<ConfirmBuyingEvent>();
            confirmEventNewEntity.Get<PassengerSortingConfirmBuyingEvent>();
            _isNeedConfirmToPay = false;
        }
    }

    private void StartTimerToAnotherVerify(EcsEntity dataEntity)
    {
        ref var timerToVerifyCars = ref dataEntity.Get<TimerComponent>();
        timerToVerifyCars.TimeLeft = TimeLeftToVerifyCarsCountInParking;
        timerToVerifyCars.IsActive = true;
    }

    private void PerformSortingIteration(EcsEntity dataEntity)
    {
        ref var dataEvent = ref dataEntity.Get<GetUnitsDataEvent>();

        for (int carIndex = 0; carIndex < dataEvent.carsOnlyParkingZoneList.Count; carIndex++)
        {
            ref var carComponent = ref dataEvent.carsOnlyParkingZoneList[carIndex].Entity.Get<CarComponent>();
            int count = 0;
            bool isCountMax = false;

            for (int firstPassengerIndex = 0; firstPassengerIndex < dataEvent.allPassengersInLevel.Count && isCountMax == false; firstPassengerIndex++)
            {
                ref var firstPassengerComponent = ref dataEvent.allPassengersInLevel[firstPassengerIndex].Entity.Get<PassengerComponent>();
                bool isSwapColor = false;

                if (carComponent.renderer.material.color == firstPassengerComponent.renderer.material.color)
                    continue;

                for (int lastPassengerIndex = dataEvent.allPassengersInLevel.Count - 1; lastPassengerIndex >= 0 && isSwapColor == false; lastPassengerIndex--)
                {
                    ref var lastPassengerComponent = ref dataEvent.allPassengersInLevel[lastPassengerIndex].Entity.Get<PassengerComponent>();

                    if (carComponent.renderer.material.color != lastPassengerComponent.renderer.material.color)
                        continue;

                    if (firstPassengerComponent.isSorted)
                        continue;

                    if (firstPassengerIndex == lastPassengerIndex)
                        continue;

                    Color tempFirstPassengerColor = firstPassengerComponent.renderer.material.color;
                    Color templastPassengerColor = lastPassengerComponent.renderer.material.color;

                    firstPassengerComponent.renderer.material.color = templastPassengerColor;
                    lastPassengerComponent.renderer.material.color = tempFirstPassengerColor;

                    firstPassengerComponent.isSorted = true;

                    isSwapColor = true;
                }

                count++;

                if (count == carComponent.maxPassengersSlots)
                    isCountMax = true;
            }
        }

        for (int z = 0; z < dataEvent.allPassengersInLevel.Count; z++)
        {
            ref var passengerComponent = ref dataEvent.allPassengersInLevel[z].Entity.Get<PassengerComponent>();
            passengerComponent.isSorted = false;
        }
    }

    private void PerformSortingIterationVersionTwo(EcsEntity dataEntity)
    {
        ref var dataEvent = ref dataEntity.Get<GetUnitsDataEvent>();

        int passengerIndex = 0;

        for (int carIndex = 0; carIndex < dataEvent.carsOnlyParkingZoneList.Count; carIndex++)
        {
            ref var carComponent = ref dataEvent.carsOnlyParkingZoneList[carIndex].Entity.Get<CarComponent>();

            for (int currentPassengerIndex = 0; currentPassengerIndex < carComponent.maxPassengersSlots; currentPassengerIndex++)
            {
                ref var firstPassengerComponent = ref dataEvent.allPassengersInLevel[passengerIndex].Entity.Get<PassengerComponent>();

                Color tempCarColor = carComponent.renderer.material.color;
                firstPassengerComponent.renderer.material.color = tempCarColor;

                passengerIndex++;
            }
        }
    }

    private void SortPassengersSecondVariable(EcsEntity dataEntity)
    {
        ref var dataEvent = ref dataEntity.Get<GetUnitsDataEvent>();

        if (dataEvent.carsOnlyParkingZoneList.Count == 0 || dataEvent.allPassengersInLevel.Count == 0)
            return;

        for (int carIndex = 0; carIndex < dataEvent.carsOnlyParkingZoneList.Count; carIndex++)
        {
            ref var carComponent = ref dataEvent.carsOnlyParkingZoneList[carIndex].Entity.Get<CarComponent>();
            int count = 0;
            bool isCountMax = false;

            for (int passengerIndex = 0; passengerIndex < dataEvent.allPassengersInLevel.Count && isCountMax == false; passengerIndex++)
            {
                ref var firstPassengerComponent = ref dataEvent.allPassengersInLevel[passengerIndex].Entity.Get<PassengerComponent>();

                if (carComponent.renderer.material.color == firstPassengerComponent.renderer.material.color)
                    continue;

                if (passengerIndex + carComponent.maxPassengersSlots >= dataEvent.allPassengersInLevel.Count)
                    continue;

                ref var lastPassengerComponent = ref dataEvent.allPassengersInLevel[passengerIndex + carComponent.maxPassengersSlots].Entity.Get<PassengerComponent>();

                if (carComponent.renderer.material.color != lastPassengerComponent.renderer.material.color)
                    continue;

                Color tempFirstPassengerColor = firstPassengerComponent.renderer.material.color;
                Color templastPassengerColor = lastPassengerComponent.renderer.material.color;

                firstPassengerComponent.renderer.material.color = templastPassengerColor;
                lastPassengerComponent.renderer.material.color = tempFirstPassengerColor;

                count++;

                if (count == carComponent.maxPassengersSlots)
                    isCountMax = true;
            }
        }

        _ecsWorld.NewEntity().Get<ConfirmBuyingEvent>();
    }
}
