using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIElemntInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private MenuSettingsShower _menuSettingsShower;
    private LevelCompleteShower _levelCompleteShower;

    public UIElemntInitSystem(MenuSettingsShower menuSettingsShower, LevelCompleteShower levelCompleteShower)
    {
        _menuSettingsShower = menuSettingsShower;
        _levelCompleteShower = levelCompleteShower;
    }

    public void Init()
    {
        InitLevel();
        InitSettings();
    }

    private void InitLevel()
    {
        var leveNewEntity = _ecsWorld.NewEntity();

        ref var levelComponent = ref leveNewEntity.Get<LevelComponent>();
        levelComponent.isLevelCompleted = false;
        levelComponent.currentLevel = SceneManager.GetActiveScene().buildIndex;

        ref var completeLevelComponent = ref leveNewEntity.Get<CompleteLevelComponent>();
        completeLevelComponent.windowGroup = _levelCompleteShower.WindowGroup;

        completeLevelComponent.windowGroup.alpha = 0f;
        completeLevelComponent.windowGroup.interactable = false;
        completeLevelComponent.windowGroup.blocksRaycasts = false;
    }

    private void InitSettings()
    {
        var settingsNewEntity = _ecsWorld.NewEntity();

        ref var completeLevelComponent = ref settingsNewEntity.Get<SettingsComponent>();

        completeLevelComponent.windowGroup = _menuSettingsShower.WindowGroup;
        completeLevelComponent.windowGroup.alpha = 0f;
        completeLevelComponent.windowGroup.interactable = false;
        completeLevelComponent.windowGroup.blocksRaycasts = false;

        completeLevelComponent.soundMuteToggle = _menuSettingsShower.SoundMuteToggle;
        completeLevelComponent.musicMuteToggle = _menuSettingsShower.MusicMuteToggle;
    }
}
