using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreVal;
    public TMPro.TextMeshProUGUI scoreChange;
    private int currScore;

    // The color of the score change text depending on how much the score is changed by
    private Color addition;
    private Color subtraction;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        addition = Color.green;
        subtraction = Color.red;

        //scoreChange.enabled = false;
        scoreVal.text = 0.ToString();

        animator = GetComponent<Animator>();
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void UpdateScore(int change)
    {
        // Update the new score to this + change
        currScore += change;
        scoreVal.text = currScore.ToString();

        // Show the change in score as well
        if(change < 0){
            scoreChange.color = subtraction;
            scoreChange.text = change.ToString();
        }else{
            scoreChange.color = addition;
            scoreChange.text = "+" + change.ToString();
        }

        // This text needs to fade out after some time
        animator.SetTrigger("Display");
        //scoreChange.enabled = true;
    }

    public int GetScore()
    {
        return currScore;
    }
}
