using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameFieldManager : MonoBehaviour {

    const int ROW_COUNT = 3;
    const int COL_COUNT = 3;
    Game.Player[,] cellPlayers = new Game.Player[ROW_COUNT,COL_COUNT];

    [SerializeField]
    Cell[] cells = new Cell[ROW_COUNT*COL_COUNT];

    internal void SetCellPlayer(Cell cell, Game.Player currentPlayer)
    {
        cellPlayers[cell.Row, cell.Column] = currentPlayer;
    }

    internal bool HasFilledLine()
    {
        if (CheckRowFilled())
            return true;

        if (CheckColFilled())
            return true;

        if (CheckDiagonalFilled())
            return true;

        return false;
    }

    private bool CheckDiagonalFilled()
    {
        int firstDiagonalCount = 0;
        int secondDiagonalCount = 0;
        int firstInvDiagCount = 0;
        int secondInvDiagCount = 0;

        for (int r = 0; r < ROW_COUNT; r++)
        {
            //game field is square so we can use basic model field[i,i] and field[r-i,i]
            if (cellPlayers[r, r] == Game.Player.FirstPlayer)
                firstDiagonalCount += 1;
            if (cellPlayers[r, r] == Game.Player.SecondPlayer)
                secondDiagonalCount += 1;
            if (cellPlayers[r, COL_COUNT - r - 1] == Game.Player.FirstPlayer)
                firstInvDiagCount += 1;
            if (cellPlayers[r, COL_COUNT - r - 1] == Game.Player.SecondPlayer)
                secondInvDiagCount += 1;
        }

        if (firstDiagonalCount == ROW_COUNT)
            return true;
        if (secondDiagonalCount == ROW_COUNT)
            return true;
        if (firstInvDiagCount == ROW_COUNT)
            return true;
        if (secondInvDiagCount == ROW_COUNT)
            return true;
        if (firstDiagonalCount == ROW_COUNT)
            return true;
        if (secondDiagonalCount == ROW_COUNT)
            return true;

        return false;
    }

    internal bool NoMoreMoves()
    {
        //if it we have problem with performance - this linq can be changed to array
        int count = (from Game.Player cellPlayer in cellPlayers
                     where cellPlayer == Game.Player.None
                     select cellPlayer)
            .Count();

        Debug.LogFormat("Free count:{0}", count);

        return count == 0;
    }

    internal void Clear()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].State = Cell.CellState.Off;
            cells[i].Owner = Game.Player.None;
        }

        for (int r = 0; r < ROW_COUNT; r++)
        {
            for (int c = 0; c < COL_COUNT; c++)
            {
                cellPlayers[r, c] = Game.Player.None;
            }
        }
    }

    private bool CheckRowFilled()
    {
        for (int r = 0; r < ROW_COUNT; r++)
        {
            int firstColCount = 0;
            int secondColCount = 0;

            for (int c = 0; c < COL_COUNT; c++)
            {
                if (cellPlayers[r, c] == Game.Player.FirstPlayer)
                    firstColCount += 1;
                if (cellPlayers[r, c] == Game.Player.SecondPlayer)
                    secondColCount += 1;
            }

            if (firstColCount == COL_COUNT)
                return true;
            if (secondColCount == COL_COUNT) 
                return true;
        }

        return false;
    }

    private bool CheckColFilled()
    {
        for (int col = 0; col < COL_COUNT; col++)
        {
            int firstColCount = 0;
            int secondColCount = 0;

            for (int row = 0; row < ROW_COUNT; row++)
            {
                if (cellPlayers[row, col] == Game.Player.FirstPlayer)
                    firstColCount += 1;
                if (cellPlayers[row, col] == Game.Player.SecondPlayer)
                    secondColCount += 1;
            }

            if (firstColCount == ROW_COUNT)
                return true;
            if (secondColCount == ROW_COUNT)
                return true;
        }

        return false;
    }
}
