using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
public Transform player;
    // Start is called before the first frame update
private Vector3 startingPoint;
    void Start()
    {
        startingPoint = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (player.position.x - (player.position.x - startingPoint.x)*0.2f, transform.position.y, transform.position.z);
    }
}
