using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSystem : IEcsRunSystem
{
    private EcsFilter<SettingsComponent> _filter;
    private EcsFilter<OpenSettingsEvent> _openSettings;
    private EcsFilter<CloseSettingsEvent> _closeSettings;
    private EcsFilter<MuteSoundEvent> _muteSound;
    private EcsFilter<UnmuteSoundEvent> _unmuteSound;
    private EcsFilter<MuteMusicEvent> _muteMusic;
    private EcsFilter<UnmuteMusicEvent> _unmuteMusic;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var settingsComponent = ref _filter.Get1(entity);

            foreach (var openEntity in _openSettings)
            {
                var eventOpenEntity = _openSettings.GetEntity(openEntity);
                OpenSettings(settingsComponent);
                eventOpenEntity.Del<OpenSettingsEvent>();
            }

            foreach (var closeEntity in _closeSettings)
            {
                var eventCloseEntity = _closeSettings.GetEntity(closeEntity);
                CloseSettings(settingsComponent);
                eventCloseEntity.Del<CloseSettingsEvent>();
            }

            foreach (var soundMuteEntity in _muteSound)
            {
                var eventMuteSoundEntity = _muteSound.GetEntity(soundMuteEntity);
                MuteSound(settingsComponent);
                eventMuteSoundEntity.Del<MuteSoundEvent>();
            }

            foreach (var soundUnmuteEntity in _unmuteSound)
            {
                var eventUnmuteSoundEntity = _unmuteSound.GetEntity(soundUnmuteEntity);
                UnmuteSound(settingsComponent);
                eventUnmuteSoundEntity.Del<UnmuteSoundEvent>();
            }

            foreach (var musicMuteEntity in _muteMusic)
            {
                var eventMuteMusicEntity = _muteMusic.GetEntity(musicMuteEntity);
                MuteMusic(settingsComponent);
                eventMuteMusicEntity.Del<MuteMusicEvent>();
            }

            foreach (var musicUnmuteEntity in _unmuteMusic)
            {
                var eventUnmuteMusicEntity = _unmuteMusic.GetEntity(musicUnmuteEntity);
                UnmuteMusic(settingsComponent);
                eventUnmuteMusicEntity.Del<UnmuteMusicEvent>();
            }
        }
    }

    private static void MuteSound(SettingsComponent settingsComponent)
    {
        settingsComponent.soundMuteToggle.MuteSoundButtonClickReader.gameObject.SetActive(false);
        settingsComponent.soundMuteToggle.UnmuteSoundButtonClickReader.gameObject.SetActive(true);
    }

    private static void UnmuteSound(SettingsComponent settingsComponent)
    {
        settingsComponent.soundMuteToggle.MuteSoundButtonClickReader.gameObject.SetActive(true);
        settingsComponent.soundMuteToggle.UnmuteSoundButtonClickReader.gameObject.SetActive(false);
    }

    private static void MuteMusic(SettingsComponent settingsComponent)
    {
        settingsComponent.musicMuteToggle.MuteMusicButtonClickReader.gameObject.SetActive(false);
        settingsComponent.musicMuteToggle.UnmuteMusicButtonClickReader.gameObject.SetActive(true);
    }

    private static void UnmuteMusic(SettingsComponent settingsComponent)
    {
        settingsComponent.musicMuteToggle.MuteMusicButtonClickReader.gameObject.SetActive(true);
        settingsComponent.musicMuteToggle.UnmuteMusicButtonClickReader.gameObject.SetActive(false);
    }

    private void OpenSettings(SettingsComponent settingsComponent)
    {
        settingsComponent.windowGroup.alpha = 1.0f;
        settingsComponent.windowGroup.interactable = true;
        settingsComponent.windowGroup.blocksRaycasts = true;
    }

    private void CloseSettings(SettingsComponent settingsComponent)
    {
        settingsComponent.windowGroup.alpha = 0f;
        settingsComponent.windowGroup.interactable = false;
        settingsComponent.windowGroup.blocksRaycasts = false;
    }
}
