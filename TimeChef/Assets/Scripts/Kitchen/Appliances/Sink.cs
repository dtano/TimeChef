using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// In this game, the sink is coupled to the dishwashing robot. And so, it is a special type of appliance
// Dirty dishes appear on the left side of the sink, while clean plates are on the right side
public class Sink : Appliance
{
    private bool isFinished = false;
    private Timer timer;
    
    Stack<Plate> dirtyPlates;
    Stack<Plate> cleanPlates;

    Plate currPlate;

    // The place where clean plate game objects are stored
    public Transform cleanCollectionPoint;
    // The place where dirty plate game objects are spawned
    public Transform dirtyCollectionPoint;

    public GameObject platePrefab;
    
    
    // Start is called before the first frame update
    void Start()
    {
        dirtyPlates = new Stack<Plate>();
        cleanPlates = new Stack<Plate>();
        timer = GetComponent<Timer>();
        animator = GetComponent<Animator>();
        soundEffect = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        // Since its a special type of appliance, its going to need different behaviour
        if(!isFinished && !isProcessing && dirtyPlates.Count > 0){
            // Take a plate from the dirty plates
            // currPlate = dirtyPlates.Pop();
            // isProcessing = true;
            // InitiateTimer();
            StartCoroutine(WashDishes());
        }
    }

    IEnumerator WashDishes()
    {
        isProcessing = true;
        while(dirtyPlates.Count > 0){
            // Keep washing
            currPlate = dirtyPlates.Pop();
            InitiateTimer();
            
            StartCoroutine(WaitForWash());
            
            currPlate.Wash();
            cleanPlates.Push(currPlate);
            currPlate = null;
        }
        yield return null;
    }

    IEnumerator WaitForWash()
    {
        yield return new WaitUntil(timer.IsTimerFinished);
        timer.FullReset();
    }

    protected override bool WillAcceptItem(Item givenItem)
    {
        if(givenItem is Plate){
            if(((Plate) givenItem).IsPlateDirty()){
                return true;
            }
        }
        return false;
    }

    protected override void HandleItem(Item givenItem)
    {
        throw new System.NotImplementedException();
    }

    protected override void Action()
    {
        throw new System.NotImplementedException();
    }

    void TakeCleanPlate()
    {
        if(cleanPlates.Count > 0){
            cleanPlates.Pop();
        }
    }

    // Now who adds the dirty plates? Probably the finished order counter.
    public void AddDirtyPlate()
    {
        GameObject plateObject = Instantiate(platePrefab, dirtyCollectionPoint.position, Quaternion.identity);
        plateObject.transform.parent = dirtyCollectionPoint;
        
        Plate plate = plateObject.GetComponent<Plate>();
        plate.MakeDirty();

        dirtyPlates.Push(plate);
    }

    // Takes the uppermost dirty plate if its there
    void TakeDirtyPlate()
    {
        currPlate = dirtyPlates.Pop();
        // Make it invisible first to make it seem like as if the plate is being washed
        currPlate.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        // Play taking a plate sound effect
        
    }

    // Moves a dirty plate to the clean plate stack after its been washed
    void AddCleanPlate()
    {
        currPlate.Wash();
        currPlate.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        currPlate.gameObject.transform.parent = cleanCollectionPoint;
        currPlate.gameObject.transform.position = cleanCollectionPoint.position;
    }

    public void InitiateTimer()
    {
        timer.SetDuration(processingTime, false);
        timer.Activate();
    }

    public void TurnOn()
    {
        animator.SetBool("IsRunning", true);
    }

    public void TurnOff()
    {
        animator.SetBool("IsRunning", false);
    }

    public void PlaySoundEffect()
    {
        soundEffect.Play();
    }

    public void StopSoundEffect()
    {
        soundEffect.Stop();
    }
}
