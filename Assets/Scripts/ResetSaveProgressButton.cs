using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ResetSaveProgressButton : MonoBehaviour
{
    [field: SerializeField] public Button Button;

    private void OnEnable()
    {
        Button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        YG2.saves.level = 1;
        YG2.saves.coins = 1000;
        YG2.saves.leaderboardScore = 0;
        YG2.saves.masterSoundValue = 0;
        YG2.saves.additionalRewardParkingSlotsCount = 0;
    }
}
