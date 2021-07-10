using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressController : MonoBehaviour
{
    public GameProgressUI gameProgressUI;

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
