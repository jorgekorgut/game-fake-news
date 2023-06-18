using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float EXPLORATION_TIME = 5;
    private float countDown;
    public TMP_Text timerText;
    public GameObject voteMenu;
    public GameObject dialogUI;
    public GameObject interactableUI;
    public AudioSource mainMusic;
    public AudioSource voteMusic;

    public void initialize()
    {
        timerText.text = "";
        countDown = EXPLORATION_TIME;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(countDown>0)     
        {         
            countDown -= Time.deltaTime;
            timerText.text = System.Math.Round(countDown, 0).ToString() + " sec";  
        }
        else
        {
            mainMusic.Stop();
            voteMusic.Play();
            this.gameObject.SetActive(false);
            dialogUI.SetActive(false);
            interactableUI.SetActive(false);
            voteMenu.SetActive(true);
        }    
    }
}
