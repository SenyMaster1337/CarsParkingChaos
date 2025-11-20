using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSettingButtonReaderSystem : IEcsInitSystem, IEcsDestroySystem
{
    private MenuSettings _menuSettings;

    public PlayerSettingButtonReaderSystem(MenuSettings menuSettings)
    {
        _menuSettings = menuSettings;
    }

    public void Init()
    {
        _menuSettings.SettingsOpenButtonClick.OnButtonClicked += OpenMenuSettings;
        _menuSettings.SettingsCloseButtonClick.OnButtonClicked += CloseMenuSettings;
        _menuSettings.SoundMuteToggle.MuteSoundButtonClickReader.OnButtonClicked += MuteSound;
        _menuSettings.SoundMuteToggle.UnmuteSoundButtonClickReader.OnButtonClicked += UnmuteSound;
        CloseMenuSettings();
    }

    public void Destroy()
    {
        _menuSettings.SettingsOpenButtonClick.OnButtonClicked -= OpenMenuSettings;
        _menuSettings.SettingsCloseButtonClick.OnButtonClicked -= CloseMenuSettings;
        _menuSettings.SoundMuteToggle.MuteSoundButtonClickReader.OnButtonClicked -= MuteSound;
        _menuSettings.SoundMuteToggle.UnmuteSoundButtonClickReader.OnButtonClicked -= UnmuteSound;
    }

    private void MuteSound()
    {
        _menuSettings.SoundMuteToggle.MuteSoundButtonClickReader.gameObject.SetActive(false);
        _menuSettings.SoundMuteToggle.UnmuteSoundButtonClickReader.gameObject.SetActive(true);
    }

    private void UnmuteSound()
    {
        _menuSettings.SoundMuteToggle.MuteSoundButtonClickReader.gameObject.SetActive(true);
        _menuSettings.SoundMuteToggle.UnmuteSoundButtonClickReader.gameObject.SetActive(false);
    }

    private void OpenMenuSettings()
    {
        _menuSettings.WindowGroup.alpha = 1.0f;
        _menuSettings.WindowGroup.interactable = true;
        _menuSettings.WindowGroup.blocksRaycasts = true;
    }

    private void CloseMenuSettings()
    {
        _menuSettings.WindowGroup.alpha = 0f;
        _menuSettings.WindowGroup.interactable = false;
        _menuSettings.WindowGroup.blocksRaycasts = false;
    }
}
