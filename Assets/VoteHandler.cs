using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using Platformer.Mechanics;
using System.Collections.Generic;
using TMPro;

public class VoteHandler : MonoBehaviour
{
    public GameObject stock13;
    public GameObject stock22;
    public Toggle toggle22;
    public Toggle toggle13;
    public Button confirmer;
    public Button nulle;

    public GameObject collectablesUI;
    public PlayerController player;

    public SceneHandler scene;
    public GameObject points;
    private Dictionary <String, Dialog> dialogMap;
    private List<Dialog> dialogList;
    public int currentDialogIndex = 0;

    public AudioSource errorVote;
    public AudioSource confirmVote;

    public TMP_Text information;
    public GameObject errorUI;

    public GameObject oldMan;
    public GameObject oldWoman;
    public GameObject poorMan;
    public GameObject hotDog;
    public GameObject lingeMan;
    public GameObject entrepreneur;
    public GameObject militaire;
    public GameObject evangelique;
    public GameObject runner;
    public GameObject mechanic;
    public GameObject scientist;
    public GameObject student;

    public void initialize()
    {
        currentDialogIndex = 0;
        information.text = "";
        toggle22.gameObject.SetActive(true);
        toggle13.gameObject.SetActive(true);
        toggle22.isOn = false;
        toggle13.isOn = false;
        stock13.SetActive(false);
        stock22.SetActive(false);
        errorUI.SetActive(false);
        desactivateAllImages();
    }

    void Awake()
    {
        confirmer.onClick.AddListener(confirmChoice);
        nulle.onClick.AddListener(nullChoice);

        toggle22.onValueChanged.AddListener(handle22);
        toggle13.onValueChanged.AddListener(handle13);
    }

    // Start is called before the first frame update
    void OnEnable ()
    {
        dialogList = new List<Dialog>();
        dialogMap = scene.dialogMap;
        if(dialogMap != null)
        {
            foreach(KeyValuePair<String, Dialog> entry in dialogMap)
            {
                if(entry.Value.colected)
                {
                    dialogList.Add(entry.Value);
                }
            }
        }
        
        collectablesUI.SetActive(true);
        player.controlEnabled = false;

        updateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void confirmChoice()
    {
        int entryResponse = -1;
        if(toggle22.isOn)
        {
            entryResponse = 22;
        }
        else if (toggle13.isOn)
        {
            entryResponse = 13;
        }

        if(entryResponse != -1)
        {
            if(dialogList[currentDialogIndex-1].response == entryResponse)
            {
                player.removeCollectable(entryResponse);
                player.addPoint(entryResponse);
            }
            player.addTry(entryResponse);
            toggle22.isOn = false;
            toggle13.isOn = false;
            confirmVote.Play();
            updateDisplay();
        }
        else
        {
            errorUI.SetActive(true);
            errorVote.Play();
        }
    }

    public void nullChoice()
    {
        confirmVote.Play();
        updateDisplay();
    }

    void handle22(bool change)
    {
        if(change)
        {
            toggle13.isOn = false;
        }
    }

    void handle13(bool change)
    {
        if(change)
        {
            toggle22.isOn = false;
        }
    }

    private void updateDisplay()
    {
        errorUI.SetActive(false);
        if(currentDialogIndex < dialogList.Count)
        {
            if(player.collectable13 == 0)
            {
                stock13.SetActive(true);
                toggle13.gameObject.SetActive(false);
            }

            if(player.collectable22 == 0)
            {
                stock22.SetActive(true);
                toggle22.gameObject.SetActive(false);
            }

            desactivateAllImages();
            information.text = dialogList[currentDialogIndex].information;
            switch(dialogList[currentDialogIndex].id)
            {
                case "old_man":
                    oldMan.SetActive(true);
                    break;
                case "old_woman":
                    oldWoman.SetActive(true);
                    break;
                case "poor_man":
                    poorMan.SetActive(true);
                    break;
                case "hot_dog":
                    hotDog.SetActive(true);
                    break;
                case "entrepreneur":
                    entrepreneur.SetActive(true);
                    break;
                case "linge_man":
                    lingeMan.SetActive(true);
                    break;
                case "militaire":
                    militaire.SetActive(true);
                    break;
                case "runner":
                    runner.SetActive(true);
                    break;
                case "evangelique":
                    evangelique.SetActive(true);
                    break;
                case "mechanic":
                    mechanic.SetActive(true);
                    break;
                case "scientist":
                    scientist.SetActive(true);
                    break;
                case "student":
                    student.SetActive(true);
                    break;
            }
            currentDialogIndex++;
        }
        else
        {
            desactivateAllImages();
            this.gameObject.SetActive(false);
            points.SetActive(true);
        }
    }

    private void desactivateAllImages()
    {
        oldMan.SetActive(false);
        oldWoman.SetActive(false);
        poorMan.SetActive(false);
        hotDog.SetActive(false);
        entrepreneur.SetActive(false);
        lingeMan.SetActive(false);
        militaire.SetActive(false);
        evangelique.SetActive(false);
        runner.SetActive(false);
        mechanic.SetActive(false);
        scientist.SetActive(false);
        student.SetActive(false);
    }
}
