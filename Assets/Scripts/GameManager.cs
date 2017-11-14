using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour {

    public static GameManager instance; //singleton
    public GameState currentGameState = GameState.menu;
    public Canvas menuCanvas;
    public Canvas inGameCanvas;
    public Canvas gameOverCanvas;
    public int collectedCoins = 0;

    public void CollectedCoin()
    {
        collectedCoins++;
    }

    void Awake()
    {
        instance = this;
    } 

    void Start()
    {
        //StartGame();
        currentGameState = GameState.menu;
    }

    void Update()
    {
        if (Input.GetButtonDown("s"))
        {
            StartGame();
        }
    }

    // Use this for initialization
    public void StartGame () {
        PlayerController.instance.StartGame();
        SetGameState(GameState.inGame);
	}

    // Update is called once per frame
    public void GameOver () {
        SetGameState(GameState.gameOver);
	}

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    void SetGameState(GameState newGameState)
    {
        if( newGameState == GameState.menu)
        {
            menuCanvas.enabled = true;
            inGameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
        } else if ( newGameState == GameState.inGame) {
            menuCanvas.enabled = false;
            inGameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
        } else if ( newGameState == GameState.gameOver)
        {
            menuCanvas.enabled = false;
            inGameCanvas.enabled = true;
            gameOverCanvas.enabled = true;
        }

        currentGameState = newGameState;
    }


}
