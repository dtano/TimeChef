using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressController : MonoBehaviour
{
    public GameProgressUI gameProgressUI;
    private ControlledMovement playerMovement;
    public GameObject endingScreen;

    // This is to update the ending screen's score
    private ScoreController scoreController;
    private PauseController pauseController;

    private bool gameOver = false;

    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Agent").GetComponent<ControlledMovement>();
        scoreController = GetComponent<ScoreController>();
        pauseController = GetComponent<PauseController>();
    }

    void Update()
    {
        if(!gameOver && gameProgressUI.GameFinished()){
            GetComponent<PauseController>().Disable();
            // Also need to freeze the player
            playerMovement.Freeze();
            pauseController.Deactivate();
            // Now fade a screen in for the final screen
            endingScreen.SetActive(true);
            endingScreen.GetComponent<EndingScreenUI>().UpdateScore(scoreController.GetScore());
            gameOver = true;

        }
    }

    // In this game, progress is determined by how many orders have been given
    public void SetMaxValue(int maxOrders)
    {
        gameProgressUI.SetMaxValue(maxOrders);
    }

    public void UpdateValue(int numOrders = 1)
    {
        gameProgressUI.UpdateValue(numOrders);
    }
}
