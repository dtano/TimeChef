using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManipulator : MonoBehaviour
{
    public float timeMultiplier;
    public int timePoints;

    private int currTimePoints;

    // Visual representation of time points on screen
    public GameObject[] pointBullets;
    
    
    // Start is called before the first frame update
    void Start()
    {
        ResetPoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Resets points back to the original amount
    void ResetPoints()
    {
        currTimePoints = timePoints;

    }

    // Adds points back to the player's current time points
    public void AddPoints(int points)
    {
        if(currTimePoints + points > timePoints){
            ResetPoints();
            AddBullets(timePoints);
        }else{
            currTimePoints += points;
            AddBullets(points);
        }
    }

    // Decrement time points by the given value
    public void UsePoints(int cost)
    {
        // if(currTimePoints - cost < 0){
        //     currTimePoints = 0;
        // }else{
        //     currTimePoints -= cost;
        // }
        currTimePoints -= cost;
        UseBullets(cost);
    }

    // A check to see whether the player has enough points to execute this command
    public bool CanManipulate(int cost = 1)
    {
        if(currTimePoints < cost){
            return false;
        }
        return true;
    }

    // Use up the given amount of bullets and show it visually
    void UseBullets(int numBullets)
    {
        if(numBullets <= timePoints){
            // Need to use currTimePoints to figure out which bullet to deactivate
            for(int i = pointBullets.Length - 1; i >= currTimePoints; i--){
                pointBullets[i].SetActive(false);
            }
        }
    }

    // Add bullets back and show it visually
    void AddBullets(int numBullets)
    {
        if(numBullets <= timePoints){
            // Need to find bullet that is inactive
            for(int i = 0; i < currTimePoints; i++){
                pointBullets[i].SetActive(true);
            }
        }
    }
}
