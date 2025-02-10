using Unity.Cinemachine;
using UnityEngine;

public class attackScript : MonoBehaviour
{
    public float attackCooldown;
    public float attackTime = 2.0f;
    public bool hit;
    public bool enemyInCollider = false;
    public bool swinging = false;
    public float swingTimerMax = 0.25f;
    public float swingTimerCurr = 0;
    EnemyBehaviour enemyScript;
    Collider attackCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackCollider = GetComponent<SphereCollider>();
        attackCooldown = 0;
        swingTimerMax = 0.09f;
        attackTime = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        bool inpAttack = Input.GetKeyDown(KeyCode.J);

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
            hit = false;
        }
        else
        {
            hit = false;
            attackCooldown = 0;
        }

        if (inpAttack && attackCooldown <= 0 && !swinging)
        {

            attackCooldown = attackTime;
            Debug.Log("Attacked");
            swinging = true;
            swingTimerCurr = 0;

        }

        if (swinging)
        {
            swingTimerCurr += Time.deltaTime;

            if (swingTimerCurr > swingTimerMax)
            {
                swingTimerCurr = 0;
                swinging = false;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (swinging)
            {
                enemyScript = other.gameObject.GetComponent<EnemyBehaviour>();
                enemyScript.knockBack();
            }
            //enemyInCollider = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (swinging)
            {
                enemyScript = other.gameObject.GetComponent<EnemyBehaviour>();
                enemyScript.knockBack();
            }
            //enemyInCollider = true;
        }
    }

    public void increaseAttackSpeed()
    {
        attackCooldown -= 0.1f;
    }

}