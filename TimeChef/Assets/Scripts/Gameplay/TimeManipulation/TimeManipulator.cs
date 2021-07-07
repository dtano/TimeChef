using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManipulator : MonoBehaviour
{
    public float timeMultiplier;
    public int timePoints;

    private int currTimePoints;
    
    
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
        }else{
            currTimePoints += points;
        }
    }

    // Decrement time points by the given value
    public void UsePoints(int cost)
    {
        currTimePoints -= cost;
    }

    // A check to see whether the player has enough points to execute this command
    public bool CanManipulate(int cost)
    {
        if(currTimePoints < cost){
            return false;
        }
        return true;
    }
}
