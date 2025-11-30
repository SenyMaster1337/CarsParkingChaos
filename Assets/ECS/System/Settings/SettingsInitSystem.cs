using Leopotam.Ecs;
using YG;

public class SettingsInitSystem : IEcsInitSystem
{
    private const string MasterVolume = "MasterVolume";
    private const string MusicVolume = "MusicVolume";

    private EcsWorld _ecsWorld;
    private StaticData _staticData;
    private MenuSettingsShower _menuSettingsShower;

    public SettingsInitSystem(MenuSettingsShower menuSettingsShower)
    {
        _menuSettingsShower = menuSettingsShower;
    }

    public void Init()
    {
        InitSettings();
    }

    private void InitSettings()
    {
        var settingsNewEntity = _ecsWorld.NewEntity();

        ref var settingsComponent = ref settingsNewEntity.Get<UISettingsComponent>();

        settingsComponent.menuSettingsShower = _menuSettingsShower;
        settingsComponent.menuSettingsShower.WindowGroup.alpha = 0f;
        settingsComponent.menuSettingsShower.WindowGroup.interactable = false;
        settingsComponent.menuSettingsShower.WindowGroup.blocksRaycasts = false;

        if (YG2.saves.musicSoundValue == 0 || YG2.saves.musicSoundValue == _staticData.MaxMusicSoundValue)
            settingsComponent.menuSettingsShower.MusicMuteToggle.AudioMixer.SetFloat(MusicVolume, _staticData.MaxMusicSoundValue);
        else
            settingsComponent.menuSettingsShower.MusicMuteToggle.AudioMixer.SetFloat(MusicVolume, _staticData.MinMusicSoundValue);

        if (YG2.saves.masterSoundValue == 0 || YG2.saves.masterSoundValue == _staticData.MaxMusicSoundValue)
            settingsComponent.menuSettingsShower.SoundMuteToggle.AudioMixer.SetFloat(MasterVolume, _staticData.MaxMasterSoundValue);
        else
            settingsComponent.menuSettingsShower.SoundMuteToggle.AudioMixer.SetFloat(MasterVolume, _staticData.MinMasterSoundValue);
    }
}
