using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BasePlayerInputSystem : IEcsInitSystem, IEcsDestroySystem
{
    protected EcsWorld _ecsWorld;
    protected PlayerInput _playerInput;
    protected Camera _mainCamera;

    public void Init()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Player.Move.performed += OnButtonClick;
    }

    public void Destroy()
    {
        _playerInput.Disable();
        _playerInput.Player.Move.performed -= OnButtonClick;
    }

    protected abstract void OnButtonClick(InputAction.CallbackContext context);
}
