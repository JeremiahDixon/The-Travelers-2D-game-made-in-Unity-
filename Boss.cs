using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Transform target;
    private Animator anim;
    private bool facingLeft = true;
    private float dazedTime;
    private int randomSpot;
    private int randomSpawnableEnemy;
    private float btwSpawningTime;
    private float waitTime;
    private float timeBtwAttack;

    public float startDazedTime;
    public int health;
    public float speed;
    public float thisSpeed;
    public int exp;
    public Transform[] moveSpots;
    public bool playerIsHere = false;
    public float startBtwSpawningTime;
    public GameObject[] spawnableEnemies;
    public float startWaitTime;
    public int enemyDamage;
    public float startTimeBtwAttack;
    public float tooClose;
    public CircleCollider2D colliderCircle;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        if (moveSpots.Length > 0)
        {
            randomSpot = Random.Range(0, moveSpots.Length);
        }
        if (spawnableEnemies.Length > 0)
        {
            randomSpawnableEnemy = Random.Range(0, spawnableEnemies.Length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsHere)
        {
            if (transform.position.x < target.position.x && facingLeft)
            {
                flip();
            }
            else if (transform.position.x > target.position.x && !facingLeft)
            {
                flip();
            }


            if (dazedTime <= 0)
            {
                speed = thisSpeed;
            }
            else
            {
                speed = 0;
                dazedTime -= Time.deltaTime;
            }


            if (health <= 0)
            {
                target.SendMessage("GainExp", exp);
                gameObject.SetActive(false);
                MenuManager.NextSceneFromWin();
            }

            if(btwSpawningTime > 0 && waitTime<= 0){
                anim.SetBool("Walk", true);
                if (Vector2.Distance(transform.position, target.position) > tooClose)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                }
                    if (Vector2.Distance(transform.position, target.position) <= tooClose)
                {
                    anim.SetBool("Walk", false);
                    if (timeBtwAttack <= 0)
                    {
                        anim.SetTrigger("Attack");
                        target.SendMessage("TakeDamage", enemyDamage);
                        timeBtwAttack = startTimeBtwAttack;
                    }
                    else
                    {
                        timeBtwAttack -= Time.deltaTime;
                    }

                }   
            }
            else{
                waitTime -= Time.deltaTime;
            }

            if (btwSpawningTime <= 0)
            {
                Vector3 position = moveSpots[randomSpot].position;
                transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);
                anim.SetBool("Walk", true);

            }
            else{
                btwSpawningTime -= Time.deltaTime;
            }

            if (moveSpots.Length > 0)
            {
                if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    if (btwSpawningTime <= 0)
                    {
                        anim.SetBool("Walk", false);
                        anim.SetBool("Attack", true);
                        Vector2 bossPos = new Vector2(transform.position.x, transform.position.y + 1.25f);
                        Instantiate(spawnableEnemies[randomSpawnableEnemy], bossPos, Quaternion.identity);
                        btwSpawningTime = startBtwSpawningTime;
                        randomSpawnableEnemy = Random.Range(0, spawnableEnemies.Length);
                        waitTime = startWaitTime;
                    }
                    else
                    {
                        anim.SetBool("Attack", false);
                       
                    }
                }
            }
        }
    }

    public void flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;
        health -= damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsHere = true;
            Destroy(colliderCircle);
        }
    }
}
