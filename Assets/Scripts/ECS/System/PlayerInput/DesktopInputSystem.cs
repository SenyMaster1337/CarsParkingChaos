using Leopotam.Ecs;
using UnityEngine.InputSystem;

namespace CarParkingChaos.ECS.Systems
{
    public class DesktopInputSystem : BasePlayerInputSystem
    {
        protected override void OnButtonClick(InputAction.CallbackContext context)
        {
            EcsWorld.NewEntity().Get<InputEvent>() = new InputEvent
            {
                Ray = MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()),
            };
        }
    }
}
