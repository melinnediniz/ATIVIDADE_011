using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movements")]
    [SerializeField] private float speed;

    public int health;
    [SerializeField] private float jumpForce;
    private bool isJumping;
    private bool doubleJump;
    
    [Header("Fall")]
    private bool isFalling;
    [SerializeField]public float fallingLimit;
    
    private Rigidbody2D rig;
    private Animator anim;
    private bool isUnder;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        CheckLife();
        Falling();
        Move();
        Jump();
        CheckRotation();
    }
    
    public void CheckLife()
    {
        if (health <= 0)
        {
            anim.SetBool("die", true);
            StartCoroutine(delay());

        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(2);
        GameController.instance.ShowGameOver();
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void Move()
    {
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

            if(movement > 0f) // right
            {
                anim.SetBool("walk", true);
                transform.eulerAngles = new Vector3(0, 0, 0f);
            }
            if(movement < 0f) // left
            {
                anim.SetBool("walk", true);
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }

            if (movement == 0f)
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
                rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                doubleJump = true;
            
                anim.SetBool("jump", true);

            }
            else if(doubleJump)
            {
                rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                doubleJump = false;
                anim.SetBool("jump", true);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
            anim.SetBool("fall", false);
        }

        if(col.gameObject.CompareTag("Trap"))
        {
            health = -10;
            isJumping = false;
        }

    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.layer == 8)
        {
            isJumping = true;
        }
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.layer == 11)
        {
            isUnder = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.layer == 11)
        {
            isUnder = false;
        }
    }
    

    void CheckRotation()
    {
        if (gameObject.transform.rotation.z != 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }

    void Falling()
    {
        if (rig.velocity.y < fallingLimit)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
            anim.SetBool("fall", false);
        }

        if (isFalling)
        {
            anim.SetBool("fall", true);
            anim.SetBool("jump", false);
        }
    }



}
