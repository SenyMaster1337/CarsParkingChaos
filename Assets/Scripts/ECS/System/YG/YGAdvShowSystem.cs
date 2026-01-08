using Leopotam.Ecs;
using YG;
using CarParkingChaos.ECS.Components;

namespace CarParkingChaos.ECS.Systems
{
    public class YGAdvShowSystem : IEcsRunSystem
    {
        private EcsFilter<YGInterstitialAdvShowEvent> _interstitialAdv;

        public void Run()
        {
            foreach (var interstitialEntity in _interstitialAdv)
            {
                var interstitialEvent = _interstitialAdv.GetEntity(interstitialEntity);
                YG2.InterstitialAdvShow();
                interstitialEvent.Del<YGInterstitialAdvShowEvent>();
            }
        }
    }
}
