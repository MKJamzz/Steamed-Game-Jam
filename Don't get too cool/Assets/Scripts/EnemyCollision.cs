using UnityEngine;

public class EnemyCollision : MonoBehaviour
{

    public bool onGround;
    public float groundCheckDistance;
    public float bufferCheckDistance;
    public LayerMask layerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bufferCheckDistance = -0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        int ignoredLayers = LayerMask.GetMask("Enemy", "Ignore Raycast");
        groundCheckDistance = (GetComponent<CapsuleCollider>().height / 2) + bufferCheckDistance;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance,~ignoredLayers))
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }
}
