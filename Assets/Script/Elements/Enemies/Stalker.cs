using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float agroRange; // quanto até ele ver o player
    public float speed;
    public int jumpForce = 6;
    public int health;

    private Rigidbody2D _rb2D;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }
    
    void CheckDistance()
    {
        float distPlayer = Vector2.Distance(transform.position, target.position);
        if(distPlayer < agroRange)
        {
            ChasePlayer();
            anim.SetBool("isAround", true);
            
        }
        else
        {
            StopChasing();
            anim.SetBool("isAround", false);
        }
    }

    void ChasePlayer()
    {
        if(transform.position.x < target.position.x) // esta na esquerda
        {
            _rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            _rb2D.velocity = new Vector2(speed, 0);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);

        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            _rb2D.velocity = new Vector2(-speed, 0);
        }
    }

    void StopChasing()
    {
        _rb2D.velocity = new Vector2(0, 0);
    }
}
