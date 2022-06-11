using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void ChooseDifficulty(int difficulty)
    {
        AI.difficultyThatPlayerChose = (AI.Difficulty)difficulty;
        AI.didPlayerChooseDifficulty = true;
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
