using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject[] DontDestroy;
    public GameObject StartLevels, NextButton, PreviousButton, ResetButton;
    private const string levelPrefix = "level-";
    private int currentLevel = 0;

	void Start () {
        DontDestroyOnLoad(this);
        foreach (GameObject ob in DontDestroy)
        {
            DontDestroyOnLoad(ob);
        }
	}

    public void StartButton()
    {
        NextButton.SetActive(true);
        PreviousButton.SetActive(true);
        ResetButton.SetActive(true);
        StartLevels.SetActive(false);
        string newLevel = levelPrefix + (++currentLevel);
        SceneManager.LoadScene(newLevel);
    }

    public void ResetLevel()
    {
        string newLevel = levelPrefix + (currentLevel);
        SceneManager.LoadScene(newLevel);
    }

    public void LoadNextLevel()
    {
        string newLevel = levelPrefix + (currentLevel++);
        SceneManager.LoadScene(newLevel);
    }

    public void LoadPreviousLevel()
    {
        if (currentLevel != 1)
        {
            string newLevel = levelPrefix + (currentLevel--);
            SceneManager.LoadScene(newLevel);
        } else
        {
            SceneManager.LoadScene("main-menu");
        }
    }
}
