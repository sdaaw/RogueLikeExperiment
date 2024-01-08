using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControls : MonoBehaviour
{
    // Start is called before the first frame update

    private GameStateHandler _gameStateHandler;
    void Start()
    {
        _gameStateHandler = GetComponent<GameStateHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (_gameStateHandler.CurrentState == GameStateHandler.GameState.InMenu)
        {
            _gameStateHandler.CurrentState = GameStateHandler.GameState.InPlay;
        }
    }

    public void QuitGame()
    {
        if(_gameStateHandler.CurrentState == GameStateHandler.GameState.Paused)
        {
            _gameStateHandler.CurrentState = GameStateHandler.GameState.InMenu;
        }
    }
}
