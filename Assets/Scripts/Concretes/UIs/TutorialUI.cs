using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteractionText;
    [SerializeField] private TextMeshProUGUI keyInteractionAlterText;
    [SerializeField] private TextMeshProUGUI keyPauseText;
    [SerializeField] private TextMeshProUGUI keyGamepadInteractionText;
    [SerializeField] private TextMeshProUGUI keyGamepadInteractionAlterText;
    [SerializeField] private TextMeshProUGUI keyGamepadPauseText;
    private void Start()
    {
        InputReader.Instance.OnBindingRebind += InputReaderBindingRebind;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanger;
        UpdateVisual();
        Show();
    }

    private void GameManager_OnStateChanger()
    {
        if (GameManager.Instance.IsCountdownStartSetActive())
        {
            Hide();
        }
    }

    private void InputReaderBindingRebind()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        keyMoveUpText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.MoveUp);
        keyMoveDownText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.MoveDown);
        keyMoveLeftText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.MoveLeft);
        keyMoveRightText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.MoveRight);
        keyInteractionText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.Interaction);
        keyInteractionAlterText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.InteractionAlternative);
        keyPauseText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.Pause);
        keyGamepadInteractionText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.Gamepad_Interact);
        keyGamepadInteractionAlterText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.Gamepad_InteractAlternative);
        keyGamepadPauseText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.Gamepad_Pause);
    }

    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}
