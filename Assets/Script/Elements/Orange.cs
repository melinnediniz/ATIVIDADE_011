using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange : MonoBehaviour
{

    private SpriteRenderer sr;
    private CircleCollider2D circle;

    public GameObject collected;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            sr.enabled = false;
            circle.enabled = false;

            GameController.instance.totalScore += score;
            GameController.instance.UpdateScoreText();

            collected.SetActive(true);
            Destroy(gameObject, 0.3f);
        }
    }
}
