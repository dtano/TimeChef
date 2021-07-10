using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressController : MonoBehaviour
{
    public GameProgressUI gameProgressUI;
    private ControlledMovement playerMovement;
    public GameObject endingScreen;

    private bool gameOver = false;

    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Agent").GetComponent<ControlledMovement>();
    }

    void Update()
    {
        if(!gameOver && gameProgressUI.GameFinished()){
            Debug.Log("All Orders Completed");
            GetComponent<PauseController>().Disable();
            // Also need to freeze the player
            playerMovement.Freeze();
            // Now fade a screen in for the final screen
            endingScreen.SetActive(true);
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
