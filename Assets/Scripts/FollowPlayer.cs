using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float camdisty=10;
    public float camdistz=-20;
    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, camdisty, camdistz);
    }
}
