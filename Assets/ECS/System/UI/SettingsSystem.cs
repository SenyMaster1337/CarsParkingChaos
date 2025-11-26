using Leopotam.Ecs;

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
        settingsComponent.menuSettingsShower.SoundMuteToggle.MuteSoundButtonClickReader.gameObject.SetActive(false);
        settingsComponent.menuSettingsShower.SoundMuteToggle.UnmuteSoundButtonClickReader.gameObject.SetActive(true);
    }

    private static void UnmuteSound(SettingsComponent settingsComponent)
    {
        settingsComponent.menuSettingsShower.SoundMuteToggle.MuteSoundButtonClickReader.gameObject.SetActive(true);
        settingsComponent.menuSettingsShower.SoundMuteToggle.UnmuteSoundButtonClickReader.gameObject.SetActive(false);
    }

    private static void MuteMusic(SettingsComponent settingsComponent)
    {
        settingsComponent.menuSettingsShower.MusicMuteToggle.MuteMusicButtonClickReader.gameObject.SetActive(false);
        settingsComponent.menuSettingsShower.MusicMuteToggle.UnmuteMusicButtonClickReader.gameObject.SetActive(true);
    }

    private static void UnmuteMusic(SettingsComponent settingsComponent)
    {
        settingsComponent.menuSettingsShower.MusicMuteToggle.MuteMusicButtonClickReader.gameObject.SetActive(true);
        settingsComponent.menuSettingsShower.MusicMuteToggle.UnmuteMusicButtonClickReader.gameObject.SetActive(false);
    }

    private void OpenSettings(SettingsComponent settingsComponent)
    {
        settingsComponent.menuSettingsShower.WindowGroup.alpha = 1.0f;
        settingsComponent.menuSettingsShower.WindowGroup.interactable = true;
        settingsComponent.menuSettingsShower.WindowGroup.blocksRaycasts = true;
    }

    private void CloseSettings(SettingsComponent settingsComponent)
    {
        settingsComponent.menuSettingsShower.WindowGroup.alpha = 0f;
        settingsComponent.menuSettingsShower.WindowGroup.interactable = false;
        settingsComponent.menuSettingsShower.WindowGroup.blocksRaycasts = false;
    }
}
