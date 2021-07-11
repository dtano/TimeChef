using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScreenUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreVal;
    

    public void UpdateScore(int score)
    {
        scoreVal.text = score.ToString();
    }
}
