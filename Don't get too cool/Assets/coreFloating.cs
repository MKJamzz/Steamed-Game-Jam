using UnityEngine;

public class coreFloating : MonoBehaviour
{

    public float flDist;
    public float flSpeed;
    Rigidbody rb;
    public float currSpeed;
    public float currGravity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        flDist = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 addMotion = new Vector3(currSpeed, 0, 0);
        Vector3 addGravity = new Vector3(0, currGravity, 0);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, flDist))
        {
            currGravity += Time.deltaTime * 2f;
        }
        else
        {
            currGravity -= Time.deltaTime * 3f;
        }

        slowDown();


        rb.linearVelocity = addGravity + addMotion;
    }

    void slowDown()
    {
        if (currSpeed > -0.2 && currSpeed < 0.2)
        {
            currSpeed = 0;
        }
        else if (currSpeed > 0.2)
        {
            currSpeed -= Time.deltaTime * 2;
        }
        else if (currSpeed < -0.2)
        {
            currSpeed += Time.deltaTime * 2;
        }
    }
}
