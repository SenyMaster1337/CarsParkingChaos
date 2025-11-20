using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestartSystem : IEcsInitSystem, IEcsDestroySystem
{
    private RestartButtonClickReader _restartButtonClickReader;

    public LevelRestartSystem(RestartButtonClickReader restartButtonClickReader)
    {
        _restartButtonClickReader = restartButtonClickReader;
    }

    public void Init()
    {
        _restartButtonClickReader.OnButtonClicked += Restart;
    }

    public void Destroy()
    {
        _restartButtonClickReader.OnButtonClicked -= Restart;
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("RESTART GAME");
    }
}
