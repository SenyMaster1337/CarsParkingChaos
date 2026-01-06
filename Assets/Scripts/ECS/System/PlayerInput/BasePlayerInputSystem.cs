using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BasePlayerInputSystem : IEcsInitSystem, IEcsDestroySystem
{
    protected EcsWorld EcsWorld;
    protected PlayerInput PlayerInput;
    protected Camera MainCamera;

    public void Init()
    {
        PlayerInput = new PlayerInput();
        PlayerInput.Enable();
        PlayerInput.Player.Move.performed += OnButtonClick;
    }

    public void Destroy()
    {
        PlayerInput.Disable();
        PlayerInput.Player.Move.performed -= OnButtonClick;
    }

    protected abstract void OnButtonClick(InputAction.CallbackContext context);
}
