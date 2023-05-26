using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    public event System.Action OnStateChanged; 
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State _state;
    private float _waitingToStart = 1f;
    private float _countdownToStart = 3f;
    private float _gamePlayingTime;
    private float _gamePlayingTimeMax = 15f;
    public float GamePlayTimer => 1 - (_gamePlayingTime / _gamePlayingTimeMax);

    private void Awake()
    {
        MakeSingleton(this);
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                _waitingToStart -= Time.deltaTime;
                if (_waitingToStart < 0f)
                {
                    _state = State.CountdownToStart;
                    OnStateChanged?.Invoke();
                }
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
