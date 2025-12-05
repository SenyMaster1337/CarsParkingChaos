using Leopotam.Ecs;
using YG;

public class SoundMuteToggleSystem : IEcsRunSystem
{
    private const string MasterVolume = "MasterVolume";

    private EcsFilter<UIISoundToggleComponent> _filter;
    private EcsFilter<MuteSoundEvent> _muteSound;
    private EcsFilter<UnmuteSoundEvent> _unmuteSound;

    private StaticData _staticData;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var soundToggleComponent = ref _filter.Get1(entity);

            foreach (var soundMuteEntity in _muteSound)
            {
                var eventMuteSoundEntity = _muteSound.GetEntity(soundMuteEntity);
                MuteMasterVolume(soundToggleComponent);
                eventMuteSoundEntity.Del<MuteSoundEvent>();
            }

            foreach (var soundUnmuteEntity in _unmuteSound)
            {
                var eventUnmuteSoundEntity = _unmuteSound.GetEntity(soundUnmuteEntity);
                UnmuteMasterVolume(soundToggleComponent);
                eventUnmuteSoundEntity.Del<UnmuteSoundEvent>();
            }
        }
    }

    private void MuteMasterVolume(UIISoundToggleComponent soundComponent)
    {
        soundComponent.soundMuteToggle.MuteSoundButtonClickReader.gameObject.SetActive(false);
        soundComponent.soundMuteToggle.UnmuteSoundButtonClickReader.gameObject.SetActive(true);
        soundComponent.soundMuteToggle.AudioMixer.SetFloat(MasterVolume, _staticData.MinMasterSoundValue);
        YG2.saves.masterSoundValue = _staticData.MinMasterSoundValue;
    }

    private void UnmuteMasterVolume(UIISoundToggleComponent soundComponent)
    {
        soundComponent.soundMuteToggle.MuteSoundButtonClickReader.gameObject.SetActive(true);
        soundComponent.soundMuteToggle.UnmuteSoundButtonClickReader.gameObject.SetActive(false);
        soundComponent.soundMuteToggle.AudioMixer.SetFloat(MasterVolume, _staticData.MaxMasterSoundValue);
        YG2.saves.masterSoundValue = _staticData.MaxMasterSoundValue;
    }
}
