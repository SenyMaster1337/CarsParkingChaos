using Leopotam.Ecs;
using YG;
using UnityEngine;

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
        soundComponent.soundMuteToggle = _soundMuteToggle;

        if (YG2.saves.masterSoundValue == 0 || YG2.saves.masterSoundValue == _staticData.MaxMasterSoundValue)
        {
            soundComponent.soundMuteToggle.AudioMixer.SetFloat(MasterVolume, _staticData.MaxMasterSoundValue);
            soundComponent.soundMuteToggle.MuteSoundButtonClickReader.gameObject.SetActive(true);
            soundComponent.soundMuteToggle.UnmuteSoundButtonClickReader.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("ау");
            Debug.Log(YG2.saves.masterSoundValue);
            soundComponent.soundMuteToggle.AudioMixer.SetFloat(MasterVolume, _staticData.MinMasterSoundValue);
            soundComponent.soundMuteToggle.MuteSoundButtonClickReader.gameObject.SetActive(false);
            soundComponent.soundMuteToggle.UnmuteSoundButtonClickReader.gameObject.SetActive(true);
        }
    }
}
