using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler<OnGameStateEventArgs> OnGameStateChanged;
    public class OnGameStateEventArgs
    {
        public GameState setState;
    }

    public enum GameState
    {
        Playing,Paused,Won,Lost
    }

    GameState _currentGameState;

    float _playTimer;
    float _playTimerMax = 120;

    bool _gamePaused;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnEscapePressed += Instance_OnEscapePressed;
    }
    private void OnDestroy()
    {
        GameInput.Instance.OnEscapePressed -= Instance_OnEscapePressed;
    }

    private void Instance_OnEscapePressed(object sender, EventArgs e)
    {
        if (_gamePaused)
        SetGameState(GameState.Playing);
        else
        SetGameState(GameState.Paused);
    }

    private void Update()
    {
        switch (_currentGameState)
        {
            case GameState.Playing:
                _playTimer += Time.deltaTime;

                if(_playTimer > _playTimerMax)
                {
                    SetGameState(GameState.Won);
                }

                break;
            case GameState.Paused:
                break;
            case GameState.Won:
                break;
            case GameState.Lost:
                break;
        }
    }

    public void SetGameState(GameState setGameState)
    {
        _currentGameState = setGameState;

        if (setGameState == GameState.Paused)
        {
            //pause the game
            _gamePaused = true;
            Time.timeScale = 0;
        }
        else
        {
            _gamePaused = false;
            Time.timeScale = 1;
        }
            OnGameStateChanged?.Invoke(this, new OnGameStateEventArgs { setState = setGameState });
    }
}
