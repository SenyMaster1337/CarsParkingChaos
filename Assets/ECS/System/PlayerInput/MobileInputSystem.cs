using Leopotam.Ecs;
using UnityEngine.InputSystem;

public class MobileInputSystem : BasePlayerInputSystem
{
    protected override void OnButtonClick(InputAction.CallbackContext context)
    {
        _ecsWorld.NewEntity().Get<InputEvent>() = new InputEvent
        {
            ray = _mainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue())
        };
    }
}
