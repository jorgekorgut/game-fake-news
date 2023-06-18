using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platformer.Mechanics;
using TMPro;

public class PointsHandler : MonoBehaviour
{
    public Button menuButton;
    public MainMenuHandler menuUI; 
    public PlayerController player;
    public TMP_Text lulaPoints;
    public TMP_Text bolsonaroPoints;

    public void initialize()
    {
        lulaPoints.text = "";
        bolsonaroPoints.text = "";
        player.controlEnabled = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        menuButton.onClick.AddListener(menuButtonClick);
    }

    void OnEnable()
    {
        lulaPoints.text = "(" + player.point13.ToString() + "/" + player.try13.ToString() + ")";
        bolsonaroPoints.text = "(" + player.point22.ToString() + "/" + player.try22.ToString() + ")";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void menuButtonClick()
    {
        this.gameObject.SetActive(false);
        menuUI.gameObject.SetActive(true);
    }
}
