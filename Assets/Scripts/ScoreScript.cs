using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour { 
    public int points;
    private static int score = 0;
    static Text scoreText;
    bool scored;

	void Start () {
        score = 0;
        GameObject scoreObj = GameObject.Find("Score");
        scoreText = scoreObj.GetComponent<Text>();
	}
	
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!scored)
        {
            ScoreScript.score += points;
            scored = true;
            scoreText.text = score.ToString();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (scored)
        {
            ScoreScript.score -= points;
            scored = false;
            scoreText.text = score.ToString();
        }
    }

    public void AddScore(int score)
    {
        ScoreScript.score += score;
    }
}
