using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    [SerializeField] private float fallingTime;

    private TargetJoint2D target;
    private BoxCollider2D boxcollider;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxcollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Invoke("Falling", fallingTime);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }

    void Falling()
    {
            target.enabled = false;
            boxcollider.isTrigger = true;
    }
}
