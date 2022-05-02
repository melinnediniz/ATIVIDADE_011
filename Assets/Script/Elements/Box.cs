using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private bool isUp;

    [SerializeField]
    private int  health;

    public GameObject effect;
    public Animator anim;

    private void Update()
    {
        if (health <= 0)
        {
            Instantiate(effect, transform.position, transform.rotation);
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (isUp)
            {
                anim.SetTrigger("hit");
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                health--;
            }
            else
            {
                anim.SetTrigger("hit");
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -jumpForce), ForceMode2D.Impulse);
                health--;
            }

            
        }
    }
}
