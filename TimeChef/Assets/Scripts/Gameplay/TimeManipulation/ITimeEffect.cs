using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeEffect
{
    void Effect(TimeManipulator manipulator);

    void SetCost(int cost);
}
