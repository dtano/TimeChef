using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the flow of the game (tutorial, player freezes)
public class LevelManager : MonoBehaviour
{
    private ControlledMovement playerMovement;
    private PageManager tutorialScreenManager;
    [SerializeField]
    private OrderManager orderManager;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Agent").GetComponent<ControlledMovement>();
        tutorialScreenManager = GameObject.FindGameObjectWithTag("UIController").GetComponent<PageManager>();
        

        StartCoroutine(TutorialSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TutorialSequence()
    {
        playerMovement.Freeze();
        yield return new WaitForSeconds(1f);
        tutorialScreenManager.Display();
    }

    public void StartGame()
    {
        // Start countdown sequence
        tutorialScreenManager.Close();
        StartCoroutine(StartingSequence());
    }

    IEnumerator StartingSequence()
    {
        yield return new WaitForSeconds(3f);

        playerMovement.AllowMovement();
        orderManager.StartOperation();

    }
}
