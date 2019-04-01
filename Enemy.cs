using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private Transform target;
    private Animator anim;
    private float timeBtwAttack;
    private float dazedTime;
    private bool facingLeft = true;
    private float waitTime;
    private int randomSpot;
    private bool seePlayer = false;

    public int health;
    public int enemyDamage;
    public int exp;
    public float speed;
    public float thisSpeed;
    public float startTimeBtwAttack;
	public float tooClose;
	public float iSeeYou;
    public float startDazedTime;
    public float startWaitTime;
    public Transform[] moveSpots;


    // Use this for initialization
    void Start () {
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        anim = GetComponent<Animator>();
        waitTime = startWaitTime;
        if (moveSpots.Length > 0)
        {
            randomSpot = Random.Range(0, moveSpots.Length);
        }

    }
	
	void Update(){

        if(transform.position.x < target.position.x && facingLeft){
            flip();
        }else if(transform.position.x > target.position.x && !facingLeft){
            flip();
        }

        if (dazedTime <= 0){
            speed = thisSpeed;
        }else{
            speed = 0;
            dazedTime -= Time.deltaTime;
        }
        if (Vector2.Distance(transform.position, target.position) < iSeeYou && Vector2.Distance(transform.position, target.position) > tooClose){
            seePlayer = true;
            anim.SetBool("Walk", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }else if(Vector2.Distance(transform.position, target.position) < iSeeYou){
            anim.SetBool("Walk", false);
        }else if(Vector2.Distance(transform.position, target.position) > iSeeYou){
            seePlayer = false;
        }
        if(Vector2.Distance(transform.position, target.position) <= tooClose){
            attack();
        }
        if (moveSpots.Length > 0){
            patrol();
        }

        if (health <= 0){
            die();
        }

	}

    public void TakeDamage(int damage){
        dazedTime = startDazedTime;
        health -= damage;
    }

    public void patrol(){
        if (!seePlayer)
        {
            Vector3 position = moveSpots[randomSpot].position;
            transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);
            anim.SetBool("Walk", true);

            if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                    anim.SetBool("Walk", false);
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

    public void attack(){
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

    public void die(){
        target.SendMessage("GainExp", exp);
        gameObject.SetActive(false);
    }
}
