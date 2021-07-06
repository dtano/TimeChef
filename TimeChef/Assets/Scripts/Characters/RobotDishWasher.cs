using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDishWasher : MonoBehaviour
{
    public float breakdownChance;
    private bool isDown = false;
    private bool isProcessing = false;
    public float resetTime;

    // Specific locations of where these tables are at
    public Table dirtyPlateTable;
    public Table cleanPlateTable; 

    // Timer for the reset time
    private Timer timer;
    private Sink sink;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Timer>();
        timer.Deactivate();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDown && !isProcessing){
            HandleDirtyPlates();
        }

        // There's a chance of the robot breaking down
        if(isProcessing){

        }
    }

    // What must a robot dishwasher do?
    // Needs to be able to check the dirty plates table
    // Operate the sink
    // Move clean plates to the clean plates table

    // Constantly checks for dirty plates
    void HandleDirtyPlates()
    {
        if(dirtyPlateTable.GetNumItems() > 0){
            isProcessing = true;
            StartCoroutine(WashPlates());
        }

        
    }

    
    // Full process of washing all dirty plates on the dirty plate table
    IEnumerator WashPlates()
    {
        Debug.Log("Start plate washing");
        while(dirtyPlateTable.GetNumItems() > 0){
            Plate dirtyPlate = TakePlate(dirtyPlateTable);
            if(dirtyPlate != null){
                // Then the robot is now holding this plate and is washing it
                StartCoroutine(WashSinglePlate(dirtyPlate));
                while(dirtyPlate.IsPlateDirty()){
                    yield return null;
                }
            }
            yield return null;
        }
        isProcessing = false;
        timer.Deactivate();
    }

    // Takes a plate from the given table if there are any plates
    Plate TakePlate(Table table)
    {
        if(table.GetNumItems() > 0){
            GameObject plateObj = table.GetTopObj();
            plateObj.transform.parent = null;
            Plate plate = plateObj.GetComponent<Plate>();
            return plate;
        }else{
            return null;
        }
    }

    // The process of washing a single plate
    // void WashSinglePlate(Plate dirtyPlate)
    // {
    //     // sink.AddPlate();
    //     dirtyPlate.HideSprite();

    //     // Should have a timer going on before calling dirtyPlate.Wash()
    //     dirtyPlate.Wash();
        
    //     PostWash(dirtyPlate);

    // }

    // The process of washing a single plate
    // Involves activating a timer for how long the wash will take
    IEnumerator WashSinglePlate(Plate dirtyPlate)
    {
        // sink.AddPlate();
        dirtyPlate.HideSprite();

        InitiateTimer();
        yield return new WaitUntil(() => timer.IsTimerFinished());
        // Should have a timer going on before calling dirtyPlate.Wash()

        timer.FullReset();
        dirtyPlate.Wash();
        
        PostWash(dirtyPlate);

    }

    // What happens to the plate after being washed
    void PostWash(Plate plate)
    {
        plate.ShowSprite();
        plate.ActivateInteraction();
        cleanPlateTable.PassItem(plate);
    }

    // Starts the timer up
    public void InitiateTimer()
    {
        timer.SetDuration(resetTime, false);
        timer.Activate();
    }
}
