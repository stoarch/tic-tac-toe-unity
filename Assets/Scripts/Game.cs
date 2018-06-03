using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public enum Player
    {
        None,
        FirstPlayer,
        SecondPlayer
    }

    public enum GameState
    {
        Playing,
        GameOver
    }

    GameState gameState = GameState.Playing;

    [SerializeField]
    Toggle playerOneTurnView;
    [SerializeField]
    Toggle playerTwoTurnView;
    [SerializeField]
    GameFieldManager gameFieldManager;
    [SerializeField]
    Image winPanel;
    [SerializeField]
    Text winText;
    [SerializeField]
    Image drawPanel;

    Player currentPlayer = Player.FirstPlayer;
    public Player CurrentPlayer
    {
        get
        {
            return currentPlayer;
        }
    }

    public void SetPlayerOneTurn()
    {
        currentPlayer = Player.FirstPlayer;
    }

    public void SetPlayerTwoTurn()
    {
        currentPlayer = Player.SecondPlayer;
    }

    public void Restart()
    {
        gameFieldManager.Clear();
        SetPlayerOneTurn();
        ShowCurrentPlayer();
        gameState = GameState.Playing;
    }

    internal void PressedCell(Cell cell)
    {
        if(gameFieldManager == null)
        {
            Debug.LogWarning("Game field manager is not set");
            return;
        }

        if(gameState == GameState.GameOver)
        {
            return;
        }

        gameFieldManager.SetCellPlayer(cell, currentPlayer);
        if(gameFieldManager.HasFilledLine())
        {
            gameState = GameState.GameOver;
            ShowCurrentPlayerWin();
            return;
        }

        if(gameFieldManager.NoMoreMoves())
        {
            gameState = GameState.GameOver;
            ShowPlayersDraw();
            return;
        }

        SetNextPlayerTurn();
    }

    private void ShowPlayersDraw()
    {
        if(drawPanel == null)
        {
            Debug.LogWarning("Draw panel is not set");
            return;
        }

        drawPanel.gameObject.SetActive(true);
    }

    private void ShowCurrentPlayerWin()
    {
        if(winPanel == null)
        {
            Debug.LogWarning("Win panel is not set");
            return;
        }

        winPanel.gameObject.SetActive(true);

        switch (currentPlayer)
        {
            case Player.FirstPlayer:
                winText.text = "First"; break;
            case Player.SecondPlayer:
                winText.text = "Second"; break;
            default:
                break;
        }
    }

    private void SetNextPlayerTurn()
    {
        SetNextPlayer();
        ShowCurrentPlayer();
    }

    private void SetNextPlayer()
    {
        switch (currentPlayer)
        {
            case Player.FirstPlayer:
                currentPlayer = Player.SecondPlayer; break;
            case Player.SecondPlayer:
                currentPlayer = Player.FirstPlayer;  break;
            default:
                break;
        }
    }

    private void ShowCurrentPlayer()
    {
        playerOneTurnView.isOn = false;
        playerTwoTurnView.isOn = false;

        switch (currentPlayer)
        {
            case Player.FirstPlayer:
                playerOneTurnView.isOn = true; break;
            case Player.SecondPlayer:
                playerTwoTurnView.isOn = true; break;
            default:
                break;
        }
    }
}
