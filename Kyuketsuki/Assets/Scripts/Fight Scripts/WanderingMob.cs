using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingMob : MonoBehaviour
{
    public Rigidbody2D body;
    public float moveSpeed;
    public Animator animator;

    private float timeToWalk;
    private float walkingCountdown;
    private bool isChasing;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.enemyEncountered == gameObject.name)
        {
            PlayerController.instance.canMove = true;
            PlayerController.instance.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Destroy(this.gameObject);
        }

        timeToWalk = Random.Range(3.0f, 5.0f);
        walkingCountdown = 0;
        isChasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateChase();

        if (isChasing == true)
        {
            float step = moveSpeed * Time.deltaTime;
            Vector2 direction = (PlayerController.instance.transform.position - transform.position).normalized * moveSpeed;
            body.velocity = direction;
            UpdateAnimation(body.velocity);
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
        walkingCountdown = Random.Range(0.5f, 1.0f);

        body.velocity = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

        UpdateAnimation(body.velocity);
    }

    void UpdateAnimation(Vector2 velocity)
    {
        if (velocity.y >= 0.65)
        {
            animator.SetInteger("Direction", 1);
        } else if (velocity.y <= -0.65)
        {
            animator.SetInteger("Direction", 3);
        } else if (velocity.x >= 0)
        {
            animator.SetInteger("Direction", 2);
        } else
        {
            animator.SetInteger("Direction", 4);
        }
    }

    void UpdateChase()
    {
        if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) <= 5)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }
    }
}
