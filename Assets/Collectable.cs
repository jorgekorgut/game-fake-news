using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class Collectable : MonoBehaviour
{
    public int type;
    public PlayerController player;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        source.Play();
        player.addCollectable(type);
        this.gameObject.SetActive(false);
    }
}
