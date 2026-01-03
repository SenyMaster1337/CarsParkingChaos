using Leopotam.Ecs;

public class LevelUIButtonsReaderSystem : IEcsInitSystem, IEcsDestroySystem
{
    private EcsWorld _ecsWorld;

    private LevelCompleteShower _levelCompleteShower;
    private LevelLossShower _levelLossShower;

    public LevelUIButtonsReaderSystem(LevelCompleteShower levelCompleteShower, LevelLossShower levelLossShower)
    {
        _levelCompleteShower = levelCompleteShower;
        _levelLossShower = levelLossShower;
    }

    public void Init()
    {
        _levelCompleteShower.NextLevelButtonClickReader.OnButtonClicked += OnButtonCkickNextLevel;
        _levelLossShower.RestartButtonClickReader.OnButtonClicked += OnButtonClickRestart;
    }

    public void Destroy()
    {
        _levelCompleteShower.NextLevelButtonClickReader.OnButtonClicked -= OnButtonCkickNextLevel;
        _levelLossShower.RestartButtonClickReader.OnButtonClicked -= OnButtonClickRestart;
    }

    private void OnButtonCkickNextLevel()
    {
        _ecsWorld.NewEntity().Get<LoadNextLevelEvent>();
    }

    private void OnButtonClickRestart()
    {
        _ecsWorld.NewEntity().Get<RestartLevelEvent>();
    }
}
