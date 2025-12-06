using Leopotam.Ecs;
using UnityEngine;

public class PassengerSortingSystem : IEcsRunSystem
{
    private const float TimeLeftToVerifyCarsCountInParking = 1f;

    private EcsWorld _ecsWorld;
    private EcsFilter<PassengerSortingComponent> _passengerSortingFilter;
    private EcsFilter<SortPassengerEvent> _sortFilter;
    private EcsFilter<GetUnitsDataEvent> _passengersAndCarsDataFilter;

    private SceneData _sceneData;

    public void Run()
    {
        foreach (var PassengerSortingEntity in _passengerSortingFilter)
        {
            ref var component = ref _passengerSortingFilter.Get1(PassengerSortingEntity);

            foreach (var sortEntity in _sortFilter)
            {
                var sortEventEntity = _sortFilter.GetEntity(sortEntity);
                _ecsWorld.NewEntity().Get<SendRequesUnitsDataEvent>();
                sortEventEntity.Del<SortPassengerEvent>();
            }

            foreach (var passengersAndCarsDataEntity in _passengersAndCarsDataFilter)
            {
                var dataEntity = _passengersAndCarsDataFilter.GetEntity(passengersAndCarsDataEntity);

                if (dataEntity.Has<VerifyCarsToPassengerSortingEvent>())
                {
                    if (_sceneData.VariableSortingSystem == 1)
                        SortPassengersFirstVariable(dataEntity);
                    else
                        SortPassengersSecondVariable(dataEntity);

                    dataEntity.Del<VerifyCarsToPassengerSortingEvent>();
                }
            }
        }
    }

    private void SortPassengersFirstVariable(EcsEntity dataEntity)
    {
        ref var dataEvent = ref dataEntity.Get<GetUnitsDataEvent>();

        if (dataEvent.carsOnlyInParking.Count == 0 || dataEvent.allPassengersInLevel.Count == 0)
        {
            _ecsWorld.NewEntity().Get<ParkingCancelReservationEvent>();
            _ecsWorld.NewEntity().Get<RaycastReaderEnableEvent>();

            dataEntity.Del<GetUnitsDataEvent>();
            return;
        }

        ref var timerToVerifyCars = ref dataEntity.Get<TimerComponent>();
        timerToVerifyCars.TimeLeft = TimeLeftToVerifyCarsCountInParking;
        timerToVerifyCars.IsActive = true;

        PerformSortingIteration(ref dataEvent);
    }

    private void PerformSortingIteration(ref GetUnitsDataEvent dataEvent)
    {
        for (int carIndex = 0; carIndex < dataEvent.carsOnlyInParking.Count; carIndex++)
        {
            ref var carComponent = ref dataEvent.carsOnlyInParking[carIndex].Entity.Get<CarComponent>();
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

    private void SortPassengersSecondVariable(EcsEntity dataEntity)
    {
        ref var dataEvent = ref dataEntity.Get<GetUnitsDataEvent>();

        if (dataEvent.carsOnlyInParking.Count == 0 || dataEvent.allPassengersInLevel.Count == 0)
            return;

        for (int carIndex = 0; carIndex < dataEvent.carsOnlyInParking.Count; carIndex++)
        {
            ref var carComponent = ref dataEvent.carsOnlyInParking[carIndex].Entity.Get<CarComponent>();
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

        _ecsWorld.NewEntity().Get<ConfirmPassengerSortingBuyingEvent>();
    }
}
