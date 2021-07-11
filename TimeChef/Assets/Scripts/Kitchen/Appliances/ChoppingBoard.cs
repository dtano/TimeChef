using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingBoard : Appliance
{
    private Timer timer;
    private Ingredient heldIngredient;
    private bool isFinished = false;

    public Transform itemHolder;
    public GameObject processCloud;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        timer = GetComponent<Timer>();
        timer.Deactivate();

        timeEffect = new TimeAccelerator(timer, timeCost);
        soundEffect = GetComponent<AudioSource>();

        processCloud.SetActive(false);
    }

    protected override void Update()
    {
        // Initiate timer when chopping board holds an ingredient
        if(!isFinished && !isProcessing && heldIngredient != null){
            isProcessing = true;
            InitiateTimer();
        }

        // Update the status of the chopping board to finished
        if(!isFinished && timer.IsTimerFinished()){
            ChopIngredient();
        }

        // Reset timer and all that
        if(isFinished && heldIngredient == null){
            ResetBoard();
        }
    }

    protected override bool WillAcceptItem(Item givenItem)
    {   
        // Only accepts ingredients
        if(heldIngredient == null){
            if(givenItem is Ingredient){
                Ingredient ing = (Ingredient) givenItem;
                if(ing.GetIngType() == Ingredient.IngredientType.Whole && !ing.IsSpoiled()){
                    return true;
                }
            }
        }else{
            if(givenItem is Plate){
                Plate plate = (Plate) givenItem;
                if(!plate.IsFull()){
                    return true;
                }
            }
        }
        return false;
    }

    public void InitiateTimer()
    {
        Animate(true);
        timer.SetDuration(processingTime, false);
        timer.Activate();

        soundEffect.Play();
    }

    // Plays a process animation when an ingredient is being cut
    // activate is a bool to determine whether we want to start or stop the animation
    void Animate(bool activate)
    {
        if(activate){
            processCloud.SetActive(true);
            animator.SetBool("isProcessing", true);
        }else{
            animator.SetBool("isProcessing", false);
            processCloud.SetActive(false);
        }
    }
    
    protected override void HandleItem(Item givenItem)
    {
        if(givenItem is Ingredient){
            // Move the item to the item holder (on top of the board)
            heldIngredient = (Ingredient) givenItem;
            heldIngredient.transform.parent = itemHolder;
            heldIngredient.transform.position = itemHolder.position;

            // Deactivate trigger zone so that player won't be able to get it before its finished
            heldIngredient.DeactivateInteraction();
        }else if(givenItem is Plate){
            //Debug.Log("Transfer to plate");
            TransferToPlate((Plate) givenItem);
        }

    }

    protected override void AcceptItem(ItemSystem agentItems)
    {
        HandleItem(agentItems.GetCurrItem());
        if(agentItems.GetCurrItem() is Ingredient){
            agentItems.DropItem();
        }
    }

    void TransferToPlate(Plate plate)
    {
        // Only when its finished is when you can take the chopped ingredient
        if(isFinished){
            plate.AddIngredient(heldIngredient);
            Destroy(heldIngredient.gameObject);
        }
    }

    // Ends the process by converting the ingredient to its chopped variation
    void ChopIngredient()
    {
        isProcessing = false;
        isFinished = true;
        heldIngredient.TransformType(Ingredient.IngredientType.Chopped);
        Animate(false);
        
        // Change sprite
        heldIngredient.ActivateInteraction();
        timer.Deactivate();

        soundEffect.Stop();
    }

    // Reset's the board's settings
    void ResetBoard()
    {
        timer.FullReset();
        isFinished = false;
        isProcessing = false;
    }

    protected override void Action()
    {
        throw new System.NotImplementedException();
    }

    public override bool AbleToManipulate()
    {
        return timeEffect != null && heldIngredient != null && !isFinished;
    }

    public override bool IsManipulated()
    {
        return timer.IsManipulated();
    }
}
