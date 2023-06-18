using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using TMPro;

public class UICollectable : MonoBehaviour
{
    public PlayerController player;
    public TMP_Text lulaText;
    public TMP_Text bolsonaroText;

    public void initialize()
    {
        lulaText.text = "0";
        bolsonaroText.text = "0";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateUI()
    {
        lulaText.text = player.collectable13.ToString();
        bolsonaroText.text = player.collectable22.ToString();
    }
}
