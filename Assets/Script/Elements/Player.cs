using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Movements")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private bool isJumping;
    private bool doubleJump;

    [Header("Life")]
    public int health;
   public int currentHealth;


    [Header("Fall")]
    private bool isFalling;
    public float fallingLimit;

    [Header("Attack")] 
    public Transform attackpoint;

    public float attackrange = 0.5f;
    public LayerMask enemylayer;
    public int attackDamage;
    
    
    private Rigidbody2D rig;
    private Animator anim;

    [SerializeField] private HealthBar healthBar;
    private bool isUnder;

    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = health;
        healthBar.SetMaxHealth(health);
 
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.instance.isOver)
        {
            CheckLife();
            Falling();
            Move();
            Jump();
            CheckRotation();

            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }
}

    void CheckLife()
    {
        if(currentHealth <= 0)
        {
            anim.SetBool("die", true);
            StartCoroutine(Die());
            GameController.instance.isOver = true;
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthBar.SetHealth(currentHealth);
        anim.SetBool("hit", true);
            
    }
    
    IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        GameController.instance.ShowGameOver();
    }

    void Move()
    {
        anim.SetBool("hit", false);
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
        anim.SetBool("hit", false);
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

    void Attack()
    {
        anim.SetTrigger("attack");
        Collider2D[] hitEnemy =  Physics2D.OverlapCircleAll(attackpoint.position, attackrange, enemylayer);

        foreach (Collider2D enemy in hitEnemy)
        {
            enemy.GetComponent<Stalker>().TakeDamage(attackDamage);
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
            TakeDamage(50);
            isJumping = false;
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
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
