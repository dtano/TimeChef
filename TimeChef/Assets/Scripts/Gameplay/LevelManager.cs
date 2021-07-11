using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the flow of the game (tutorial, player freezes)
public class LevelManager : MonoBehaviour
{
    private ControlledMovement playerMovement;
    private PageManager tutorialScreenManager;
    private PauseController pauseController;
    
    [SerializeField]
    private OrderManager orderManager;

    private float secondsLeft = 3f;
    public TMPro.TextMeshProUGUI countdownTimer;
    private bool activateTimer = false;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Agent").GetComponent<ControlledMovement>();
        GameObject UIController = GameObject.FindGameObjectWithTag("UIController");
        
        tutorialScreenManager = UIController.GetComponent<PageManager>();
        pauseController = UIController.GetComponent<PauseController>();
        
        countdownTimer.text = ((int)secondsLeft).ToString();

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        StartCoroutine(TutorialSequence());
    }

    // Update is called once per frame
    void Update()
    {
        if(activateTimer && Mathf.Round(secondsLeft) > 0){
            secondsLeft -= Time.deltaTime;
            countdownTimer.text = Mathf.Round(secondsLeft).ToString();
        }

        if(activateTimer && Mathf.Round(secondsLeft) <= 0){
            activateTimer = false;
            countdownTimer.text = "GO";
            // Deactivate timer object
            StartGame();
        }
    }

    IEnumerator TutorialSequence()
    {
        playerMovement.Freeze();
        pauseController.Deactivate();
        yield return new WaitForSeconds(0.8f);
        tutorialScreenManager.Display();
    }

    public void StartCountdown()
    {
        // Start countdown sequence
        tutorialScreenManager.Close();
        countdownTimer.transform.parent.gameObject.SetActive(true);
        activateTimer = true;
        //StartCoroutine(StartingSequence());
    }

    // IEnumerator StartingSequence()
    // {
    //     yield return new WaitForSeconds(3f);

    //     playerMovement.AllowMovement();
    //     orderManager.StartOperation();
    //     pauseController.Activate();

    // }

    void StartGame()
    {
        playerMovement.AllowMovement();
        orderManager.StartOperation();
        pauseController.Activate();

        audioManager.PlaySound("StartService");
        audioManager.PlaySound("BGM");
        countdownTimer.transform.parent.gameObject.SetActive(false);

    }
}
