using Leopotam.Ecs;
using UnityEngine.InputSystem;

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
