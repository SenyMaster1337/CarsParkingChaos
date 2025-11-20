using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonClickReader : MonoBehaviour
{
    [field: SerializeField] public Button Button;

    public event Action OnButtonClicked;

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
        OnButtonClicked?.Invoke();
    }
}
