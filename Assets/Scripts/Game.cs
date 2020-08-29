using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class Game : MonoBehaviour
{
    [Header("Stats")] 
    [SerializeField, Range(1, 99)] private int _startLives = 3;
    
    [Header("Screens")]
    [SerializeField] private Popup _startScreen;
    [SerializeField] private Popup _gameOverScreen;

    [Header("Components")] 
    [SerializeField] private Lives _livesView;
    [SerializeField] private Score _scoreView;
    [SerializeField] private Conveyor _conveyor;

    private int _lives;
    private int _score;
    
    private void Start()
    {
        _lives = _startLives;
        
        _startScreen.Submit += OnSubmit;
        _gameOverScreen.Submit += OnSubmit;
        _conveyor.ConveyorEvent += OnConveyorEvent;
        
        _startScreen.Show();
        _gameOverScreen.Hide();
    }

    private void OnConveyorEvent(ConveyorEventType et)
    {
        switch (et)
        {
            case ConveyorEventType.Correct:
                ++_score;
                _scoreView.Value = _score;
                _gameOverScreen.Data = $"Your score: {_score:N0}";
                break;
            case ConveyorEventType.Wrong:
                _livesView.Count = --_lives;
                if (_lives == 0)
                    Lose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(et), et, null);
        }
    }

    private void Lose()
    {
        _conveyor.Stop();
        _gameOverScreen.Show();
    }

    private void OnSubmit()
    {
        Restart();
    }

    private void Restart()
    {
        _lives = _startLives;
        _score = 0;

        _livesView.Count = _lives;
        _scoreView.Value = _score;
        
        _startScreen.Hide();
        _gameOverScreen.Hide();
        
        _conveyor.Restart();
    }
}
