using Leopotam.Ecs;
using UnityEngine;
using YG;

public class PassengerSortingSystem : IEcsRunSystem
{
    public string RewardID = "PassengerSortingRewardID";

    private EcsWorld _ecsWorld;
    private EcsFilter<PassengerSortEvent> _sortEventFilter;
    private EcsFilter<GetUnitsDataEvent> _unitsDataFilter;
    private EcsFilter<ShowAdvToPassengerSortEvent> _showAdvFilter;

    private bool _isNeedConfirmToPay;
    private bool _isNeedAdvShow;
    private bool _isSortingActive;

    public PassengerSortingSystem()
    {
        _isNeedConfirmToPay = false;
        _isNeedAdvShow = false;
        _isSortingActive = false;
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
                    ConfirmPayment();
                    PerformInerationDependingPaymentMethod(dataEntity);
                    _isSortingActive = true;
                    _ecsWorld.NewEntity().Get<DisableRaycastReaderToggleSwitchMethodEvent>();
                }

                if (dataEvent.carsOnlyParkingZoneList.Count == 0 && _isSortingActive == false)
                {
                    _ecsWorld.NewEntity().Get<ShowNotCarsToSortingWindowEvent>();
                    dataEntity.Del<GetUnitsDataEvent>();
                    dataEntity.Del<VerifyCarsToPassengerSortingEvent>();
                }

                if (dataEntity.IsAlive() && dataEvent.carsOnlyParkingZoneList.Count == 0 && _isSortingActive == true)
                {
                    _ecsWorld.NewEntity().Get<SortPassengerInColorCarsEvent>();
                    _ecsWorld.NewEntity().Get<EnableRaycastReaderEvent>();
                    _ecsWorld.NewEntity().Get<EnableButtonsEvent>();
                    _ecsWorld.NewEntity().Get<EnableRaycastReaderToggleSwitchMethodEvent>();
                    _isSortingActive = false;
                    dataEntity.Del<GetUnitsDataEvent>();
                    dataEntity.Del<VerifyCarsToPassengerSortingEvent>();
                }
            }
        }
    }

    private void PerformInerationDependingPaymentMethod(EcsEntity dataEntity)
    {
        if (_isNeedAdvShow == true)
        {
            YG2.RewardedAdvShow(RewardID, () =>
            {
                if (RewardID == "PassengerSortingRewardID")
                {
                    ReplaceColorCars(dataEntity);
                }
            });

            _ecsWorld.NewEntity().Get<ClosePassengerSortingInfoShowerEvent>();
            _isNeedAdvShow = false;

            return;
        }

        if (_isNeedConfirmToPay == false)
        {
            ReplaceColorCars(dataEntity);
        }
    }

    private void ConfirmPayment()
    {
        if (_isNeedConfirmToPay == true)
        {
            var confirmEventNewEntity = _ecsWorld.NewEntity();
            confirmEventNewEntity.Get<ConfirmBuyingEvent>();
            confirmEventNewEntity.Get<PassengerSortingConfirmBuyingEvent>();
            _isNeedConfirmToPay = false;
        }
    }

    private void ReplaceColorCars(EcsEntity dataEntity)
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
}
