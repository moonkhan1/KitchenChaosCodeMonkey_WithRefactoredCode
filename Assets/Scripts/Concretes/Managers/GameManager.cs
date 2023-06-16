using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    public event System.Action OnStateChanged;
    public event System.Action OnGamePaused;
    public event System.Action OnGameUnPaused;
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State _state;
    private float _countdownToStart = 3f;
    private float _gamePlayingTime;
    private float _gamePlayingTimeMax = 300f;
    private bool IsGamePause = false;
    public float GamePlayTimer => 1 - (_gamePlayingTime / _gamePlayingTimeMax);

    private void Awake()
    {
        MakeSingleton(this);
    }

    private void Start()
    {
        InputReader.Instance.OnPause += InputReader_OnPause;
        InputReader.Instance.OnInteraction += InputReader_OnInteraction;
        
        //Debug trigger game start automatically
        _state = State.CountdownToStart;
        OnStateChanged?.Invoke();
    }

    private void InputReader_OnInteraction(object sender, EventArgs e)
    {
        if (_state == State.WaitingToStart)
        {
            _state = State.CountdownToStart;
            OnStateChanged?.Invoke();
        }
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                break;
            case State.CountdownToStart:
                _countdownToStart -= Time.deltaTime;
                if (_countdownToStart < 0f)
                {
                    _state = State.GamePlaying;
                    _gamePlayingTime = _gamePlayingTimeMax;
                    OnStateChanged?.Invoke();
                }
                break;
            case State.GamePlaying:
                _gamePlayingTime -= Time.deltaTime;
                if (_gamePlayingTime < 0f)
                {
                    _state = State.GameOver;
                    OnStateChanged?.Invoke();
                }
                break;
            case State.GameOver:
                break;
        }
        Debug.Log(_state);
    }
    private void InputReader_OnPause()
    {
        TogglePauseGame();
    }

    public void TogglePauseGame()
    {
        IsGamePause = !IsGamePause;
        if (IsGamePause)
        {
            OnGamePaused?.Invoke();
            Time.timeScale = 0;
        }
        else
        {
            OnGameUnPaused?.Invoke();
            Time.timeScale = 1f;
        }
    }

    public bool IsGamePlaying()
    {
        return _state == State.GamePlaying;
    }
    public bool IsGameOver()
    {
        return _state == State.GameOver;
    }
    public bool IsCountdownStartSetActive()
    {
        return _state == State.CountdownToStart;
    }

    public float GetCountdownNumber()
    {
        return _countdownToStart;
    }
}
