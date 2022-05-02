using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StepOnIt : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator anim;
    private bool colliding;
    private bool playerDestroyed;

    [SerializeField] private float speed;

    public Transform rightCol;
    public Transform leftCol;

    public Transform headPoint;
    public LayerMask layer;

    public BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckRotation();
    }

    void Move()
    {
        if (rb2D.bodyType != RigidbodyType2D.Static)
        {
            rb2D.velocity = new Vector2(speed, rb2D.velocity.y);

            colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);

            if (colliding)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
                speed *= -1f;
            }
        }
    }
    
    
    void CheckRotation()
    {
        if (gameObject.transform.rotation.z != 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            float height = col.contacts[0].point.y - headPoint.position.y;
            
            if(height > 0 && !playerDestroyed)
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 15, ForceMode2D.Impulse);
                anim.SetTrigger("die");
                speed = 0;
                rb2D.bodyType = RigidbodyType2D.Static;
                boxCollider.enabled = false;
                Destroy(gameObject, 0.33f);
            }
            else
            {
                col.gameObject.GetComponent<Player>().TakeDamage(10);
                Debug.Log("Recebe dano");
            }
        }
    }
}
