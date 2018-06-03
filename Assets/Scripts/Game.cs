using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public enum Player
    {
        FirstPlayer,
        SecondPlayer
    }

    [SerializeField]
    Toggle playerOneTurnView;
    [SerializeField]
    Toggle playerTwoTurnView;

    Player currentPlayer;
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

    internal void PressedCell(Cell cell)
    {
    }
}
