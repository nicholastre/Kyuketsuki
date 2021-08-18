using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingMob : MonoBehaviour
{
    public Rigidbody2D body;
    public float moveSpeed;

    private float timeToWalk;
    private float walkingCountdown;
    private bool isChasing;

    // Start is called before the first frame update
    void Start()
    {
        timeToWalk = Random.Range(3.0f, 5.0f);
        walkingCountdown = 0;
        isChasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) <= 5)
        {
            isChasing = true;
        } else
        {
            isChasing = false;
        }

        if (isChasing == true)
        {
            float step = moveSpeed * Time.deltaTime;
            Vector2 target = PlayerController.instance.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, target, step);
        } else
        {
            if (walkingCountdown == 0.0f)
            {
                timeToWalk -= Time.deltaTime;

                if (timeToWalk <= 0.0f)
                {
                    timeToWalk = 0.0f;
                    prepareWalk();
                }
            }
            else if (timeToWalk == 0.0f)
            {
                walkingCountdown -= Time.deltaTime;

                if (walkingCountdown <= 0.0f)
                {
                    walkingCountdown = 0.0f;
                    timeToWalk = Random.Range(3.0f, 5.0f);
                    body.velocity = Vector2.zero;
                }
            }
        }
    }

    void prepareWalk()
    {
        int direction = Random.Range(1, 8);
        walkingCountdown = Random.Range(0.5f, 1.0f);

        switch (direction)
        {
            case 1:
                body.velocity = new Vector2(0.0f, 1.0f) * moveSpeed;
                break;
            case 2:
                body.velocity = new Vector2(0.5f, 0.5f) * moveSpeed;
                break;
            case 3:
                body.velocity = new Vector2(1.0f, 0.0f) * moveSpeed;
                break;
            case 4:
                body.velocity = new Vector2(0.5f, -0.5f) * moveSpeed;
                break;
            case 5:
                body.velocity = new Vector2(0.0f, -1.0f) * moveSpeed;
                break;
            case 6:
                body.velocity = new Vector2(-0.5f, -0.5f) * moveSpeed;
                break;
            case 7:
                body.velocity = new Vector2(-1.0f, 0.0f) * moveSpeed;
                break;
            case 8:
                body.velocity = new Vector2(-0.5f, 0.5f) * moveSpeed;
                break;
        }

    }
}
