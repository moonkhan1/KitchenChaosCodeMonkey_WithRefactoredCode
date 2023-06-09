using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingsUI : SingletonBase<KeyBindingsUI>
{
    [SerializeField] private Button _closeButton;
    [Header("Key Buttons")]
    [SerializeField] private Button _moveUpButton;
    [SerializeField] private Button _moveDownButton;
    [SerializeField] private Button _moveLeftButton;
    [SerializeField] private Button _moveRightButton;
    [SerializeField] private Button _interactionButton;
    [SerializeField] private Button _interactionAlternativeButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _interactionButtonGamepad;
    [SerializeField] private Button _interactionAlternativeButtonGamepad;
    [SerializeField] private Button _pauseButtonGamepad;
    [Header("Key Text in buttons")]
    [SerializeField] private TextMeshProUGUI _moveUpText;
    [SerializeField] private TextMeshProUGUI _moveDownText;
    [SerializeField] private TextMeshProUGUI _moveLeftText;
    [SerializeField] private TextMeshProUGUI _moveRightText;
    [SerializeField] private TextMeshProUGUI _interactionText;
    [SerializeField] private TextMeshProUGUI _interactionAlternativeText;
    [SerializeField] private TextMeshProUGUI _pauseText;
    [SerializeField] private TextMeshProUGUI _interactionTextGamepad;
    [SerializeField] private TextMeshProUGUI _interactionAlternativeTextGamepad;
    [SerializeField] private TextMeshProUGUI _pauseTextGamepad;

    [SerializeField] private Transform _pressToAnyKeyToRebindTransform;

    private void Awake()
    {
        MakeSingleton(this);
        _closeButton.onClick.AddListener(() =>
        {
            Hide();
            OptionsUI.Instance.Show();
        });
        _moveUpButton.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.MoveUp);
        });
        _moveDownButton.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.MoveDown);
        });
        _moveLeftButton.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.MoveLeft);
        });
        _moveRightButton.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.MoveRight);
        });
        _interactionButton.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.Interaction);
        });
        _interactionAlternativeButton.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.InteractionAlternative);
        });
        _pauseButton.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.Pause);
        });
        _interactionButtonGamepad.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.Gamepad_Interact);
        });
        _interactionAlternativeButtonGamepad.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.Gamepad_InteractAlternative);
        });
        _pauseButtonGamepad.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.Gamepad_Pause);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        Hide();
        UpdateVisual();
        HidePressToRebindKey();
    }

    private void GameManager_OnGameUnPaused()
    {
        Hide();
    }

    private void UpdateVisual()
    {
        _moveUpText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.MoveUp);
        _moveDownText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.MoveDown);
        _moveLeftText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.MoveLeft);
        _moveRightText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.MoveRight);
        _interactionText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.Interaction);
        _interactionAlternativeText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.InteractionAlternative);
        _pauseText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.Pause);
        _interactionTextGamepad.text = InputReader.Instance.GetBindingText(InputReader.Bindings.Gamepad_Interact);
        _interactionAlternativeTextGamepad.text = InputReader.Instance.GetBindingText(InputReader.Bindings.Gamepad_InteractAlternative);
        _pauseTextGamepad.text = InputReader.Instance.GetBindingText(InputReader.Bindings.Gamepad_Pause);
    }


    public void Show()
    {
        gameObject.SetActive(true);
        _moveUpButton.Select();   
    }
    public void Hide() => gameObject.SetActive(false);

    private void ShowPressToRebindKey() => _pressToAnyKeyToRebindTransform.gameObject.SetActive(true);
    private void HidePressToRebindKey() => _pressToAnyKeyToRebindTransform.gameObject.SetActive(false);
    private void RebindBinding(InputReader.Bindings bindings)
    {
        ShowPressToRebindKey();
        InputReader.Instance.RebindBinding(bindings, ()=>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
