using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : IEcsInitSystem, IEcsDestroySystem
{
    private EcsWorld _world;
    private PlayerInput _playerInput;
    private Camera _mainCamera;

    public event Action<Ray> RayShooting;

    public InputSystem(Camera camera)
    {
        _mainCamera = camera;
    }

    public void Init()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Player.Move.performed += OnButtonClick;
    }

    private void OnButtonClick(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        RayShooting?.Invoke(_mainCamera.ScreenPointToRay(mousePosition));
    }

    public void Destroy()
    {
        _playerInput.Disable();
        _playerInput.Player.Move.performed -= OnButtonClick;
    }
}
