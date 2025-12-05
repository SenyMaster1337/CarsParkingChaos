using Leopotam.Ecs;
using UnityEngine;

public class PassengerSortingSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<PassengerSortingComponent> _passengerSortingFilter;
    private EcsFilter<SortPassengerEvent> _sortFilter;
    private EcsFilter<GetPassengersAndCarsDataEvent> _passengersAndCarsDataFilter;

    private SceneData _sceneData;

    public void Run()
    {
        foreach (var PassengerSortingEntity in _passengerSortingFilter)
        {
            ref var component = ref _passengerSortingFilter.Get1(PassengerSortingEntity);

            foreach (var sortEntity in _sortFilter)
            {
                var sortEventEntity = _sortFilter.GetEntity(sortEntity);
                _ecsWorld.NewEntity().Get<SendRequesPassengersAndCarsDataEvent>();
                sortEventEntity.Del<SortPassengerEvent>();
            }

            foreach (var passengersAndCarsDataEntity in _passengersAndCarsDataFilter)
            {
                var dataEntity = _passengersAndCarsDataFilter.GetEntity(passengersAndCarsDataEntity);

                if (_sceneData.VariableSortingSystem == 1)
                    SortPassengersFirstVariable(dataEntity);
                else
                    SortPassengersSecondVariable(dataEntity);

                dataEntity.Del<GetPassengersAndCarsDataEvent>();
            }
        }
    }

    private void SortPassengersFirstVariable(EcsEntity dataEntity)
    {
        ref var dataEvent = ref dataEntity.Get<GetPassengersAndCarsDataEvent>();

        if (dataEvent.cars.Count == 0 || dataEvent.passengers.Count == 0)
            return;

        for (int carIndex = 0; carIndex < dataEvent.cars.Count; carIndex++)
        {
            ref var carComponent = ref dataEvent.cars[carIndex].Entity.Get<CarComponent>();
            int count = 0;
            bool isCountMax = false;

            for (int firstPassengerIndex = 0; firstPassengerIndex < dataEvent.passengers.Count && isCountMax == false; firstPassengerIndex++)
            {
                ref var firstPassengerComponent = ref dataEvent.passengers[firstPassengerIndex].Entity.Get<PassengerComponent>();
                bool isSwapColor = false;

                if (carComponent.renderer.material.color == firstPassengerComponent.renderer.material.color)
                    continue;

                for (int lastPassengerIndex = dataEvent.passengers.Count - 1; lastPassengerIndex >= 0 && isSwapColor == false; lastPassengerIndex--)
                {
                    ref var lastPassengerComponent = ref dataEvent.passengers[lastPassengerIndex].Entity.Get<PassengerComponent>();

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

        for (int z = 0; z < dataEvent.passengers.Count; z++)
        {
            ref var passengerComponent = ref dataEvent.passengers[z].Entity.Get<PassengerComponent>();
            Debug.Log(passengerComponent.isSorted);
            passengerComponent.isSorted = false;
        }

        _ecsWorld.NewEntity().Get<ConfirmPassengerSortingBuyingEvent>();
    }

    private void SortPassengersSecondVariable(EcsEntity dataEntity)
    {
        ref var dataEvent = ref dataEntity.Get<GetPassengersAndCarsDataEvent>();

        if (dataEvent.cars.Count == 0 || dataEvent.passengers.Count == 0)
            return;

        for (int carIndex = 0; carIndex < dataEvent.cars.Count; carIndex++)
        {
            ref var carComponent = ref dataEvent.cars[carIndex].Entity.Get<CarComponent>();
            int count = 0;
            bool isCountMax = false;

            for (int passengerIndex = 0; passengerIndex < dataEvent.passengers.Count && isCountMax == false; passengerIndex++)
            {
                ref var firstPassengerComponent = ref dataEvent.passengers[passengerIndex].Entity.Get<PassengerComponent>();

                if (carComponent.renderer.material.color == firstPassengerComponent.renderer.material.color)
                    continue;

                if (passengerIndex + carComponent.maxPassengersSlots >= dataEvent.passengers.Count)
                    continue;

                ref var lastPassengerComponent = ref dataEvent.passengers[passengerIndex + carComponent.maxPassengersSlots].Entity.Get<PassengerComponent>();

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
