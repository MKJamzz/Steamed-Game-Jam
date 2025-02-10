using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public bool onGround;
    public bool hitBottom;
    public float groundCheckDistance;
    public float roofCheckDist;
    public float bufferCheckDistance = 0.16f;
    public float bufferRoof = 0f;
    public bool triggerModMenu;
    public GameObject ScoreManager;
    ScoreManagerScript smc;
    public LayerMask layerMask;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScoreManager = GameObject.Find("ScoreManager");
        smc = ScoreManager.GetComponent<ScoreManagerScript>();
        bufferCheckDistance = 0.16f;
        triggerModMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        groundCheckDistance = (GetComponent<CapsuleCollider>().height / 2) + bufferCheckDistance;
        roofCheckDist = (GetComponent<CapsuleCollider>().height / 2) + bufferRoof;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance, ~layerMask))
        {
            onGround = true;
        }
        else
        {
            onGround=false;
        }

        RaycastHit hitroof;
        if (Physics.Raycast(transform.position, transform.up, out hitroof, groundCheckDistance))
        {
            hitBottom = true;
        }
        else
        {
            hitBottom = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            smc.lives -= 1;
        }
        if (collision.gameObject.CompareTag("collectable"))
        {   
            Destroy(collision.gameObject);
            smc.score += 10;
        }
        if (collision.gameObject.CompareTag("normalScrap"))
        {
            Destroy(collision.gameObject);
            smc.normalScraps += 1;
        }
        if (collision.gameObject.CompareTag("normalScrap"))
        {
            Destroy(collision.gameObject);
            smc.normalScraps += 1;
        }
        if (collision.gameObject.CompareTag("coreScrap"))
        {
            Destroy(collision.gameObject);
            triggerModMenu = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
    }
}
