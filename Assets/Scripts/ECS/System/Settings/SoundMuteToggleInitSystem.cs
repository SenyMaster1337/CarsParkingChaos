using Leopotam.Ecs;
using YG;

public class SoundMuteToggleInitSystem : IEcsInitSystem
{
    private const string MasterVolume = "MasterVolume";

    private EcsWorld _ecsWorld;
    private StaticData _staticData;
    private SoundMuteToggle _soundMuteToggle;

    public SoundMuteToggleInitSystem(SoundMuteToggle soundMuteToggle)
    {
        _soundMuteToggle = soundMuteToggle;
    }

    public void Init()
    {
        InitSettings();
    }

    private void InitSettings()
    {
        var settingsNewEntity = _ecsWorld.NewEntity();

        ref var soundComponent = ref settingsNewEntity.Get<UIISoundToggleComponent>();
        soundComponent.SoundMuteToggle = _soundMuteToggle;

        if (YG2.saves.MasterSoundValue == 0 || YG2.saves.MasterSoundValue == _staticData.MaxMasterSoundValue)
        {
            soundComponent.SoundMuteToggle.AudioMixer.SetFloat(MasterVolume, _staticData.MaxMasterSoundValue);
            soundComponent.SoundMuteToggle.MuteSoundButtonClickReader.gameObject.SetActive(true);
            soundComponent.SoundMuteToggle.UnmuteSoundButtonClickReader.gameObject.SetActive(false);
        }
        else
        {
            soundComponent.SoundMuteToggle.AudioMixer.SetFloat(MasterVolume, _staticData.MinMasterSoundValue);
            soundComponent.SoundMuteToggle.MuteSoundButtonClickReader.gameObject.SetActive(false);
            soundComponent.SoundMuteToggle.UnmuteSoundButtonClickReader.gameObject.SetActive(true);
        }
    }
}
