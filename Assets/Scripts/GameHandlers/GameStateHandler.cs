using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
    // Start is called before the first frame update


    public enum GameState
    {
        InMenu,
        InPlay,
        Paused
    }

    [SerializeField]
    private GameObject _mainMenuCanvasParent;

    [SerializeField]
    private GameObject _pausedCanvasParent;

    private GameObject _player;

    private GameManager _gameManager;

    public GameState CurrentState { get; set; }
    private GameState _previousState { get; set; }



    private void Awake()
    {
        _gameManager = GetComponent<GameManager>();
    }
    void Start()
    {

        CurrentState = GameState.InMenu;
    }

    // Update is called once per frame
    void Update()
    {
        CheckStates();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CurrentState = (CurrentState == GameState.Paused) ? GameState.InPlay : GameState.Paused;
        }
    }

    public void CheckStates()
    {
        if (_previousState == CurrentState) return;
        _previousState = CurrentState;
        switch (CurrentState) 
        {
            case GameState.InMenu:
            {
                print("switching to menu");
                _gameManager.LoadMainMenu();
                break;
            }
            case GameState.InPlay:
            {
                _gameManager.InitPlay();
                break;
            }
            case GameState.Paused:
            {
                break;
            }
        }
        _pausedCanvasParent.SetActive(CurrentState == GameState.Paused);
        _mainMenuCanvasParent.SetActive(CurrentState == GameState.InMenu);
    }
}
