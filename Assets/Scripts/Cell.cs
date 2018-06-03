using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerDownHandler {

    public enum CellState
    {
        Off,
        On
    }

    [SerializeField]
    Image image;

    [SerializeField]
    int row;
    public int Row { get { return row; } }

    [SerializeField]
    int col;
    public int Column { get { return col; } }

    [SerializeField]
    Game game;

    [SerializeField]
    Sprite offSprite;
    [SerializeField]
    Sprite playerOneSprite;
    [SerializeField]
    Sprite playerTwoSprite;

    [SerializeField]
    CellState state;

    public CellState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
            UpdateCellView();
        }
    }

    [SerializeField]
    Game.Player owner;

    public Game.Player Owner
    {
        get
        {
            return owner;
        }
        set
        {
            owner = value;
        }
    }


	void Start ()
    {
		
	}


    private void UpdateCellView()
    {
        if(image == null)
        {
            Debug.LogWarning("No image set for cell");
            return;
        }

        switch (state)
        {
            case CellState.Off:
                image.sprite = offSprite; break;
            case CellState.On:
                SetPlayerSprite();  break;
            default:
                break;
        }
    }

    private void SetPlayerSprite()
    {
        switch (owner)
        {
            case Game.Player.FirstPlayer:
                image.sprite = playerOneSprite; break;
            case Game.Player.SecondPlayer:
                image.sprite = playerTwoSprite;  break;
            default:
                break;
        }
    }

    private void InvertCellState()
    {
        switch (State)
        {
            case CellState.Off:
                State = CellState.On;  break;
            case CellState.On:
                State = CellState.Off; break;
            default:
                break;
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (state == CellState.On)
            return;

        owner = game.CurrentPlayer;
        State = CellState.On;
        game.PressedCell(this);
    }
}
