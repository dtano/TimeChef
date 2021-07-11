using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    public GameObject pagesObject;
    
    private List<GameObject> pages;
    
    private GameObject nextBtn;
    private GameObject backBtn;

    public GameObject startGameBtn;

    private int currPageIndex;
    private bool isOpen = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpen){
            if(pages.Count > 1){
                // Control presence of next and back buttons
                ButtonControl();
            }
        }
    }

    void Initialize()
    {
        pages = new List<GameObject>();
        PopulatePages();
        FindButtons();
        pagesObject.SetActive(false);
        startGameBtn.SetActive(false);

    }

    // Get the pages from the given game object
    void PopulatePages()
    {
        // Find the object that holds the pages
        GameObject pageHolder = pagesObject.transform.Find("Pages").gameObject;

        // Add each page to the list of pages
        if(pageHolder != null){
            foreach(Transform page in pageHolder.transform){
                pages.Add(page.gameObject);
                page.gameObject.SetActive(false);
            }
        }
    }

    // Assigns buttons to the variables specified
    void FindButtons()
    {
        // Arrows refer to the next and back arrow on a page
        GameObject arrowHolder = pagesObject.transform.Find("Arrows").gameObject;

        if(arrowHolder != null){
            nextBtn = arrowHolder.transform.Find("Next").gameObject;
            backBtn = arrowHolder.transform.Find("Back").gameObject;
        }
    }

    // Shows the screen
    public void Display()
    {
        pagesObject.SetActive(true);
        isOpen = true;
        if(pages.Count > 0){
            pages[currPageIndex].SetActive(true);
        }
    }

    public void OpenNextPage()
    {
        //Debug.Log("Opening next page");
        pages[currPageIndex].SetActive(false);
        if(currPageIndex < pages.Count - 1){
            currPageIndex++;
            pages[currPageIndex].SetActive(true);

        }else{
            currPageIndex = 0;
            //nextBtn.SetActive(false);
            //backBtn.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    public void OpenPrevPage()
    {
        pages[currPageIndex].SetActive(false);
        currPageIndex--;
        pages[currPageIndex].SetActive(true);
        
        if(currPageIndex == 0){
            backBtn.SetActive(false);

        }
    }

    // Manages when the next and back buttons will appear and when they won't
    void ButtonControl()
    {
        // BackBtn control
        if(currPageIndex > 0){
            backBtn.SetActive(true);
        }else{
            backBtn.SetActive(false);
        }

        // Next button control
        if(currPageIndex < pages.Count - 1){
            nextBtn.SetActive(true);
        }else{
            nextBtn.SetActive(false);
        }

        // The button that allows the player to start the game
        if(currPageIndex >= pages.Count - 1){
            startGameBtn.SetActive(true);
        }else{
            startGameBtn.SetActive(false);
        }
    }

    public void Close()
    {
        isOpen = false;
        if(pages.Count > 0){
            pages[currPageIndex].SetActive(false);
        }
        pagesObject.SetActive(false);
    }      
}
