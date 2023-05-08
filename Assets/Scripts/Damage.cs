using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float HP = 3;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Sword"))
        {
            if (HP > 0)
            {
                HP--;
                Debug.Log("hit");
            }

            else
            {
                Destroy(GetComponent<Zombie>());
                anim.SetBool("Dead", true);
            }
        }
    }
}
