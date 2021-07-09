using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Accelerates the process of an item or appliance
public class TimeAccelerator : ITimeEffect
{
    // The timer to be sped up
    private Timer _timer;

    // How many time bullets does this skill need
    public int _skillCost;
    
    public TimeAccelerator(Timer timer = null, int skillCost = 1)
    {
        _timer = timer;
        _skillCost = skillCost;
    }

    public void SetCost(int cost)
    {
        _skillCost = cost;
    }
    
    // In an accelerator, the effect would be to speed up the timer
    public void Effect(TimeManipulator manipulator)
    {
        if(manipulator.CanManipulate(_skillCost)){
            // Change the time multiplier of the associated timer
            if(manipulator.timeMultiplier <= 0){
                // Default time multiplier value. Only used if the player is given a negative or zero multiplier
                manipulator.timeMultiplier = 2f;
            }
            _timer.SetTimeMultiplier(manipulator.timeMultiplier);
            manipulator.UsePoints(_skillCost);
        }
    }

    public void SetTimer(Timer timer)
    {
        _timer = timer;
    }

}
