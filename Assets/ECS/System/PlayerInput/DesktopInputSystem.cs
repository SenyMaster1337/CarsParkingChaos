using Leopotam.Ecs;
using UnityEngine.InputSystem;

public class DesktopInputSystem : BasePlayerInputSystem
{
    protected override void OnButtonClick(InputAction.CallbackContext context)
    {
        _ecsWorld.NewEntity().Get<InputEvent>() = new InputEvent
        {
            ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue())
        };
    }
}
