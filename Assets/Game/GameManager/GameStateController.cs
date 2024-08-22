using System;
using UnityEngine;

namespace Lion.Manager
{
    public class GameStateController
    {
        private GameState _currentState = GameState.MainMenu;

        public event Action<GameState> OnGameStateChanged;

        public GameState CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                OnGameStateChanged?.Invoke(_currentState);
            }
        }
    }

    public enum GameState
    {
        MainMenu,
        InGame,
        Paused,
        GameOver
    }
}