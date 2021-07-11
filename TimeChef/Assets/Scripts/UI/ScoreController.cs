using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public ScoreUI scoreUI;

    public void UpdateScore(int change)
    {
        scoreUI.UpdateScore(change);
    }

    public int GetScore()
    {
        return scoreUI.GetScore();
    }
}
