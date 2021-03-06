using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUi : Singleton<GameUi>
{
    public GameObject gameEndScreen;
    public TextMeshProUGUI gameEndText;
    public Image gameEndSpriteRenderer;
    public Sprite whiteWinsSprite, blackWinsSprite;

    protected override void Awake()
    {
        base.Awake();
        gameEndScreen.SetActive(false);
    }

    public void OpenGameEndScreen(GameManager.State state)
    {
        gameEndScreen.SetActive(true);

        /*
        if(state == GameManager.State.BLACK_VICTORY)
        {
            gameEndText.text = "Black Wins";
        }
        else if(state == GameManager.State.WHITE_VICTORY)
        {
            gameEndText.text = "White Wins";
        }*/

        gameEndSpriteRenderer.sprite = (state == GameManager.State.WHITE_VICTORY) ? whiteWinsSprite : blackWinsSprite;
    }

    public void RestartGame()
    {
        GameManager.RestartGame();
    }

    public void OpenMainMenu()
    {
        GameManager.OpenMenuScene();
    }
}
