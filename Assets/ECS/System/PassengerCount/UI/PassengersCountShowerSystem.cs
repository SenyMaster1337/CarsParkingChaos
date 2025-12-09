using Leopotam.Ecs;
using UnityEngine;

public class PassengersCountShowerSystem : IEcsRunSystem
{
    private EcsFilter<PassengersCountShowerComponent> _passengersCountFilter;
    private EcsFilter<ChangePassengersCountToShowerEvent> _changePassengersCountFilter;

    public void Run()
    {
        foreach (var passengersCountEntity in  _passengersCountFilter)
        {
            ref var passengersCountComponent = ref _passengersCountFilter.Get1(passengersCountEntity);

            foreach(var changeCountEntity  in _changePassengersCountFilter)
            {
                ChangeCount(passengersCountEntity, passengersCountComponent);
                _changePassengersCountFilter.GetEntity(changeCountEntity).Del<ChangePassengersCountToShowerEvent>();
            }
        }
    }

    private void ChangeCount(int passengersCountEntity, PassengersCountShowerComponent passengersCountComponent)
    {
        ref var changeCountEvent = ref _changePassengersCountFilter.Get1(passengersCountEntity);
        passengersCountComponent.passengersCountText.Value.SetText($"{changeCountEvent.newCurrentCount}");
    }
}
