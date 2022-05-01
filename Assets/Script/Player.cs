using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Publicas")]
    public float Speed;
    public float JumpForce;
    public bool isJumping;
    public bool DoubleJump;

    private Rigidbody2D rig;
    private Animator anim;
    private bool isUnder;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        // Move o personagem em uma posição (sem física)
        //transform.position += movement * Time.deltaTime * Speed;

        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * Speed, rig.velocity.y);

            if(movement > 0f) // direita
            {
                anim.SetBool("walk", true);
                transform.eulerAngles = new Vector3(0, 0, 0f);
            }
            if(movement < 0f) // esquerda
            {
                anim.SetBool("walk", true);
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }

            if(movement == 0f)
            {
                anim.SetBool("walk", false);
            }
        
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && !isUnder)
        {
            if(!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                DoubleJump = true;
            
                anim.SetBool("jump", true);

            }
            else if(DoubleJump)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                DoubleJump = false;
                anim.SetBool("jump", true);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }

        if(collision.gameObject.tag == "Trap")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
            isJumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 11)
        {
            isUnder = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 11)
        {
            isUnder = false;
        }
    }

}
