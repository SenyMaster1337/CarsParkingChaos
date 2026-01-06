using Leopotam.Ecs;
using UnityEngine.InputSystem;

public class MobileInputSystem : BasePlayerInputSystem
{
    protected override void OnButtonClick(InputAction.CallbackContext context)
    {
        EcsWorld.NewEntity().Get<InputEvent>() = new InputEvent
        {
            Ray = MainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue()),
        };
    }
}
