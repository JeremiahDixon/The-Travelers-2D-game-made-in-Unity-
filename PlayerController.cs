using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    //private static bool created = false;
    private static int maxHealth = 20;
    private static int level = 1;
    private Rigidbody2D rb2d;
    private Animator anim;
    private float timeBtwAttack;
    private GameObject door;
    private bool facingLeft = true;
    private Transform attackingPos;
    private static int health = 20;
    private static int exp = 0;
    private static int damage = 2;
    private static int keyCount = 0;
    private static float speed = 3;
    private bool paused = false;
    private bool obtainKey = false;
    private bool levelUpOnce = false;
    private bool attackEnemy = false;

    public float startTimeBtwAttack;
    public Transform attackPos;
    public Transform attackPosUp;
    public Transform attackPosDown;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public float cameraShakeAmt = 0.3f;
    public GameObject pauseMenu;

    CameraShake camShake;

    QuestManager questManager = new QuestManager();
    public static event Action<string> CompleteQuest;

    // Use this for awake
    void Awake()
    {
        Camera mainCam = Camera.main;
        camShake = mainCam.GetComponent<CameraShake>();
        /*if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }*/
    }


    // Use this for initialization
    void Start()
    {
        timeBtwAttack = 0;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackingPos = attackPos;
    }


    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal * speed, moveVertical * speed);
        rb2d.velocity = movement;

        if (moveHorizontal > 0 && facingLeft){
            flip();
        }
        else if (moveHorizontal < 0 && !facingLeft){
            flip();
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow)
          || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
            anim.SetBool("Walk", true);
        }else{
            anim.SetBool("Walk", false);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            attackingPos = attackPosUp;
        }else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            attackingPos = attackPosDown;
        }else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)){
            attackingPos = attackPos;
        }

        if (timeBtwAttack <= 0){
            attack();
        }else{
            timeBtwAttack -= Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            if (paused)
            {
                pauseMenu.SetActive(false);
                paused = false;
                Time.timeScale = 1.0f;
            }else{
                pauseMenu.SetActive(true);
                paused = true;
                Time.timeScale = 0.0f;
            }
        }

        if (health <= 0){
            Destroy(gameObject);
            //gameObject.SetActive(false);
            Reset();
            MenuManager.NextSceneFromDeath();
        }//else{
            //gameObject.SetActive(true);
        //}
    }

    public static void Reset()
    {
        maxHealth = 20;
        health = maxHealth;
        level = 1;
        damage = 2;
        exp = 0;
        speed = 3;
        keyCount = 0;
    }

    public static int getLevel()
    {
        return level;
    }
    public static int getkeys()
    {
        return keyCount;
    }
    public static int getHealth()
    {
        return health;
    }
    public static int getMaxHealth()
    {
        return maxHealth;
    }
    public static void setHealth(int newHealth)
    {
        health = newHealth;
    }
    public static void setHealthToMax()
    {
        health = maxHealth;
    }

    public static void setSpeed(int newSpeed)
    {
        speed = newSpeed;
    }

    /*void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackingPos.position, attackRange);
    }*/

    public void attack(){
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetTrigger("Attack");
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackingPos.position, attackRange, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if (enemiesToDamage[i].CompareTag("Enemy"))
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
                else if (enemiesToDamage[i].CompareTag("SpawnableEnemy"))
                {
                    enemiesToDamage[i].GetComponent<SpawnableEnemy>().TakeDamage(damage);
                }
                else if (enemiesToDamage[i].CompareTag("Boss"))
                {
                    enemiesToDamage[i].GetComponent<Boss>().TakeDamage(damage);
                }
                if (!attackEnemy)
                {
                    attackEnemy = true;
                    CompleteQuest("Attack an Enemy");
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        camShake.Shake(cameraShakeAmt, 0.3f);
    }
    public void flip()
    {
        facingLeft = !facingLeft;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void GainExp(int expGain)
    {
        exp += expGain;
        if (exp >= 12 && level < 2)
        {
            levelUp(2);
        }else if (exp >= 25 && level < 3)
        {
            levelUp(3);
        }else if (exp >= 40 && level < 4)
        {
            levelUp(4);
        }else if (exp >= 55 && level < 5)
        {
            levelUp(5);
        }
    }
    public void levelUp(int levelTo)
    {
        if(!levelUpOnce)
        {
            levelUpOnce = true;
            CompleteQuest("Level Up");
        }
        level = levelTo;
        damage += level;
        maxHealth += level;
        health += level;
        //transform.localScale += new Vector3(.5F, .5f, 0);
        //Debug.Log("You Leveled up to level " + level + ". Your health is now " + health + "! Your" +
                  //" Maxhealth is not " + maxHealth + "!");
    }
    public void GainKey()
    {
        if (!obtainKey)
        {
            obtainKey = true;
            CompleteQuest("Obtain a Key");
        }
        keyCount += 1;
    }
    public void UnlockDoor()
    {
        if (keyCount > 0)
        {
            if (door != null)
            {
                door.SendMessage("DestroyDoor");
                keyCount--;
            }
        }else{
            //need keys UI
        }
       
    }
    void ThisDoor(GameObject thisDoor){

        door = thisDoor;
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Key"))
        {
            other.gameObject.SetActive(false);
            GainKey();
        }
    }
}
