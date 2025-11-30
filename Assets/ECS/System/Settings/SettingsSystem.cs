using Leopotam.Ecs;
using YG;

public class SettingsSystem : IEcsRunSystem
{
    private const string MasterVolume = "MasterVolume";
    private const string MusicVolume = "MusicVolume";

    private EcsFilter<UISettingsComponent> _filter;
    private EcsFilter<OpenSettingsEvent> _openSettings;
    private EcsFilter<CloseSettingsEvent> _closeSettings;
    private EcsFilter<MuteSoundEvent> _muteSound;
    private EcsFilter<UnmuteSoundEvent> _unmuteSound;
    private EcsFilter<MuteMusicEvent> _muteMusic;
    private EcsFilter<UnmuteMusicEvent> _unmuteMusic;

    private StaticData _staticData;

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
                MuteMasterVolume(settingsComponent);
                eventMuteSoundEntity.Del<MuteSoundEvent>();
            }

            foreach (var soundUnmuteEntity in _unmuteSound)
            {
                var eventUnmuteSoundEntity = _unmuteSound.GetEntity(soundUnmuteEntity);
                UnmuteMasterVolume(settingsComponent);
                eventUnmuteSoundEntity.Del<UnmuteSoundEvent>();
            }

            foreach (var musicMuteEntity in _muteMusic)
            {
                var eventMuteMusicEntity = _muteMusic.GetEntity(musicMuteEntity);
                MuteMusicVolume(settingsComponent);
                eventMuteMusicEntity.Del<MuteMusicEvent>();
            }

            foreach (var musicUnmuteEntity in _unmuteMusic)
            {
                var eventUnmuteMusicEntity = _unmuteMusic.GetEntity(musicUnmuteEntity);
                UnmuteMusicVolume(settingsComponent);
                eventUnmuteMusicEntity.Del<UnmuteMusicEvent>();
            }
        }
    }

    private void MuteMasterVolume(UISettingsComponent settingsComponent)
    {
        settingsComponent.menuSettingsShower.SoundMuteToggle.MuteSoundButtonClickReader.gameObject.SetActive(false);
        settingsComponent.menuSettingsShower.SoundMuteToggle.UnmuteSoundButtonClickReader.gameObject.SetActive(true);
        settingsComponent.menuSettingsShower.SoundMuteToggle.AudioMixer.SetFloat(MasterVolume, _staticData.MinMasterSoundValue);
        YG2.saves.masterSoundValue = _staticData.MaxMasterSoundValue;
    }

    private void UnmuteMasterVolume(UISettingsComponent settingsComponent)
    {
        settingsComponent.menuSettingsShower.SoundMuteToggle.MuteSoundButtonClickReader.gameObject.SetActive(true);
        settingsComponent.menuSettingsShower.SoundMuteToggle.UnmuteSoundButtonClickReader.gameObject.SetActive(false);
        settingsComponent.menuSettingsShower.SoundMuteToggle.AudioMixer.SetFloat(MasterVolume, _staticData.MaxMasterSoundValue);
        YG2.saves.masterSoundValue = _staticData.MaxMasterSoundValue;
    }

    private void MuteMusicVolume(UISettingsComponent settingsComponent)
    {
        settingsComponent.menuSettingsShower.MusicMuteToggle.MuteMusicButtonClickReader.gameObject.SetActive(false);
        settingsComponent.menuSettingsShower.MusicMuteToggle.UnmuteMusicButtonClickReader.gameObject.SetActive(true);
        settingsComponent.menuSettingsShower.MusicMuteToggle.AudioMixer.SetFloat(MusicVolume, _staticData.MinMusicSoundValue);
        YG2.saves.musicSoundValue = _staticData.MinMusicSoundValue;
    }

    private void UnmuteMusicVolume(UISettingsComponent settingsComponent)
    {
        settingsComponent.menuSettingsShower.MusicMuteToggle.MuteMusicButtonClickReader.gameObject.SetActive(true);
        settingsComponent.menuSettingsShower.MusicMuteToggle.UnmuteMusicButtonClickReader.gameObject.SetActive(false);
        settingsComponent.menuSettingsShower.MusicMuteToggle.AudioMixer.SetFloat(MusicVolume, _staticData.MaxMusicSoundValue);
        YG2.saves.musicSoundValue = _staticData.MaxMusicSoundValue;
    }

    private void OpenSettings(UISettingsComponent settingsComponent)
    {
        settingsComponent.menuSettingsShower.WindowGroup.alpha = 1.0f;
        settingsComponent.menuSettingsShower.WindowGroup.interactable = true;
        settingsComponent.menuSettingsShower.WindowGroup.blocksRaycasts = true;
    }

    private void CloseSettings(UISettingsComponent settingsComponent)
    {
        settingsComponent.menuSettingsShower.WindowGroup.alpha = 0f;
        settingsComponent.menuSettingsShower.WindowGroup.interactable = false;
        settingsComponent.menuSettingsShower.WindowGroup.blocksRaycasts = false;
    }
}
