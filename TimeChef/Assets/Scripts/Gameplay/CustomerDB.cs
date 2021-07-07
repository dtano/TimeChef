using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDB : MonoBehaviour
{
    public static CustomerDB _instance;

    public enum CustomerType{
        Cube,
        Human,
        Fleeb
    }

    public Dictionary<CustomerType, float> waitTimes;
    public Dictionary<CustomerType, Sprite> custSprites;

    private void Awake()
    {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        waitTimes = new Dictionary<CustomerType, float>(){
            {CustomerType.Cube, 30}, 
            {CustomerType.Human, 45},
            {CustomerType.Fleeb, 40}
        };
    }

    public CustomerType GetRandomCustType()
    {
        System.Random rand = new System.Random();
        List<CustomerType> types = new List<CustomerType>(waitTimes.Keys);

        int index = rand.Next(types.Count);
        return types[index];
    }

    
}
