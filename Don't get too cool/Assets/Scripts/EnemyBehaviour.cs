using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Rigidbody rb;
    EnemyCollision ec;
    GameObject player;
    public GameObject scrap;
    public GameObject core;

    public float currSpeed;
    public float maxSpeed;
    public float speedChange;
    public float chaseRange;
    public float currGravity;
    public float gravMod;
    private bool hit;
    public int health = 50;


    public float attackRange = 2f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ec = GetComponent<EnemyCollision>();

        player = GameObject.Find("Player");
        currSpeed = 0;
        maxSpeed = 3;
        speedChange = 10;
        chaseRange = 10;
        currGravity = 0;
        gravMod = 7;
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets the distance between the player and object
        float dist = Vector3.Distance(transform.position, player.transform.position);  
        Vector3 moveX = new Vector3(currSpeed, 0, 0);
        Vector3 addGravity = new Vector3(0, currGravity, 0);

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        if (!ec.onGround)
        {

            if (currGravity < -0.5)    //Speeds up falling speed if going downwards
            {
                currGravity -= Time.deltaTime * gravMod * 2;
            }
            else
            {
                currGravity -= Time.deltaTime * gravMod;
            }
            if (currGravity < -15)  //Clamps the maximum fall speed for a character
            {
                currGravity = -15;
            }
        }
        else
        {
            hit = false;
            currGravity = 0;
        }

        if (dist < attackRange && ec.onGround && !hit)
        {
            attackPlayer();
        }
        else if (dist < chaseRange && !hit)
        {
            chasePlayer();
        }


        rb.linearVelocity = moveX + addGravity;
    }

    void chasePlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            if (currSpeed > maxSpeed - 0.5f && currSpeed < maxSpeed + 0.5f)
            {
                currSpeed = maxSpeed;
            }
            else if (currSpeed < maxSpeed)
            {
                currSpeed += speedChange * Time.deltaTime;
            }
            else if (currSpeed > maxSpeed)
            {
                currSpeed -= speedChange * Time.deltaTime * 1.5f;
            }
        }
        else
        {
            if (currSpeed > -maxSpeed - 0.5f && currSpeed < -maxSpeed + 0.5f)
            {
                currSpeed = -maxSpeed;
            }
            else if (currSpeed > -maxSpeed)
            {
                currSpeed -= speedChange * Time.deltaTime;
            }
            else if (currSpeed < -maxSpeed)
            {
                currSpeed += speedChange * Time.deltaTime * 1.5f;
            }
        }
    }
    void attackPlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            currSpeed = 5f;
            currGravity = 3f;
        }
        else
        {
            currSpeed = -5f;
            currGravity = 3f;
        }
    }

    public void knockBack()
    {
        Debug.Log("enemy hit");
        hit = true;
        health -= 25;

        if (transform.position.x > player.transform.position.x)   //If enemy is on the right of player
        {
            currSpeed = 2f;
            currGravity = 1f;
        }
        else if (transform.position.x < player.transform.position.x)   //If enemy is on the right of player
        {
            currSpeed = -2f;
            currGravity = 1f;
        }
        if(health <= 0)
        {
            spawnScrap();
        }   
    }

    public void spawnScrap()
    {
        Debug.Log("SpawningScrap");
        int scrapAmount = Random.Range(1, 5);
        int coreScrapChance = Random.Range(0, 10);
        if (coreScrapChance == 5)
        {
            GameObject coreScrap = Instantiate(core, transform.position, Quaternion.identity);
            coreFloating cs = coreScrap.GetComponent<coreFloating>();
            cs.currGravity = (Random.Range(0.5f, 2f));
            cs.currSpeed = (Random.Range(-3, 3f));
        }

        
        for (int i = 0; i < scrapAmount; i++)
        {
            GameObject newScrap = Instantiate(scrap, transform.position, Quaternion.identity);
            scrapFloating ns = newScrap.GetComponent<scrapFloating>();
            ns.currGravity = (Random.Range(0.5f, 2f));
            ns.currSpeed = (Random.Range(-3, 3f));
        }
    }
}
