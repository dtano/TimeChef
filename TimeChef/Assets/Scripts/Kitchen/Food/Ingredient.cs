using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Item
{
    public bool isFresh = true;
    private bool isProcessed = false;
    public float processTime;
    private float currTimer = 0f;

    private bool isCooked = false;
    private bool isBurnt = false;

    public string ingredientName;
    // Needs a variable that 

    public enum IngredientType{
        Chopped,
        Whole,
        Cooked
    }

    public IngredientType ingredientType;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Initiates processing counter
    public void Unspoil()
    {
        if(!isFresh){
            // Start timer
            if(currTimer >= processTime){
                isFresh = true;
                // Change sprite
            }
        }
    }

    // Initates process counter (unspoil would probably be a process)
    public void Process()
    {

    }

    public void TransformType(IngredientType newType)
    {
        if(ingredientType == IngredientType.Chopped && newType == IngredientType.Whole){
            Debug.Log("Invalied transformation");
        }else{
            ingredientType = newType;
            switch(ingredientType){
                case IngredientType.Chopped:
                    ingredientName = "chopped " + ingredientName;
                    Debug.Log(ingredientName);
                    break;
                case IngredientType.Cooked:
                    ingredientName = "cooked " + ingredientName;
                    break;
            }
            // Change name of the ingredient to reflect the new type
            // Change sprite
            Debug.Log("Ingredient has been transformed");
        }
    }

    public override void Reset()
    {
        Destroy(gameObject);
        //throw new System.NotImplementedException();
    }

    public void Cook()
    {
        isCooked = true;
    }

    public void Overcook()
    {
        isBurnt = true;
    }

    public IngredientType GetIngType()
    {
        return ingredientType;
    }

    public bool IsCooked()
    {
        return isCooked;
    }

    public bool IsBurnt()
    {
        return isBurnt;
    }

    public bool IsSpoiled()
    {
        return !isFresh;
    }
}
