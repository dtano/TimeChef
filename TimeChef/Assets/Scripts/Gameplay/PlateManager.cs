using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the plate distribution. Passes dirty plates back to the collection point when triggered
public class PlateManager : MonoBehaviour
{
    // Number of plates a game can have
    public int maxPlates = 4;
    public GameObject platePrefab;
    
    public Table dirtyCollectionPoint;
    public Table cleanCollectionPoint;

    private int currNumPlates;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currNumPlates = maxPlates;
        GenerateInitialPlates();
    }

    // Update is called once per frame
    void Update()
    {
        currNumPlates = cleanCollectionPoint.GetNumItems();
    }

    // Brings back the given amount of dirty dishes to the kitchen
    public void ReturnDirtyDishes(int numSuccessfulOrders)
    {
        // Update current number of plates
        currNumPlates -= numSuccessfulOrders;

        // Then return the used dishes back to the kitchen. Number of dirty dishes should be equal to numSuccessfulOrders
        int count = 0;
        while(count < numSuccessfulOrders){
            InstantiateDirtyPlate();
            count++;
        }


    }

    // Generates the initial amount of clean plates
    void GenerateInitialPlates()
    {
        int count = 0;
        while(count < maxPlates){
            InstantiateCleanPlate();
            count++;
        }
    }

    // Creates a dirty plate and places it in the dirty collection point
    void InstantiateDirtyPlate()
    {
        GameObject plateObj = Instantiate(platePrefab, dirtyCollectionPoint.transform.position, Quaternion.identity);
        Plate plate = plateObj.GetComponent<Plate>();

        plate.MakeDirty();
        plate.DeactivateInteraction();
        dirtyCollectionPoint.PassItem(plate);
    }

    // Creates a clena plate and places it in the clean collection point
    // Only use this when the game is initialized
    void InstantiateCleanPlate()
    {
        GameObject plateObj = Instantiate(platePrefab, dirtyCollectionPoint.transform.position, Quaternion.identity);
        Plate plate = plateObj.GetComponent<Plate>();

        cleanCollectionPoint.PassItem(plate);
    }
}
