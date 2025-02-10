using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //References to objects
    Rigidbody rb;
    PlayerCollision pc;

    //Variavles
    public float maxSpeed;
    public float speedChange;
    public float curSpeed;
    public float dashSpeed;
    public float currGravity;
    public float gravMod;
    public float jumpPower;
    public bool isHoldSpace;
    private bool hitRoof;
    private bool fastFalling;
    public float inputVal;

    bool isDashing;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pc = GetComponent<PlayerCollision>();

        maxSpeed = 4.5f;
        curSpeed = 0;
        dashSpeed = 8;
        currGravity = 0;
        gravMod = 13;
        jumpPower = 5;
        speedChange = 24;

        isDashing = false;
        isHoldSpace = false;
        fastFalling = false;
    }

    // Update is called once per frame
    void Update()
    {
        float inpX = Input.GetAxisRaw("Horizontal");
        inputVal = Input.GetAxisRaw("Horizontal");
        bool inpJump = Input.GetButton("Jump");
        bool inpSprint = Input.GetButton("Sprint");

        Vector3 horizontalMovement = new Vector3(curSpeed, 0 ,0);
        Vector3 addGravity = new Vector3(0,currGravity, 0);

        if(inpSprint && inpX > 0 && !isDashing)
        {
            curSpeed += dashSpeed;
            isDashing = true;
        }
        else if(inpSprint && inpX < 0 && !isDashing)
        {
            curSpeed -= dashSpeed;
            isDashing = true;
        }

        if (inpX == 0)
        {
            if (curSpeed > -.5 && curSpeed < .5)
            {
                curSpeed = 0;
                isDashing = false;
            }
            else if (curSpeed > 0)
            {
                curSpeed -= (Time.deltaTime * speedChange);
            }
            else if (curSpeed < 0)
            {
                curSpeed += (Time.deltaTime * speedChange);
            }
        }
        else if (inpX > 0)
        {
            if (curSpeed > maxSpeed - 0.5f && curSpeed < maxSpeed + 0.5f)
            {
                curSpeed = maxSpeed;
                isDashing = false;
            }
            else if (curSpeed < maxSpeed)
            {
                curSpeed += speedChange * Time.deltaTime;
            }
            else if (curSpeed > maxSpeed)
            {
                curSpeed -= speedChange * Time.deltaTime * 1.5f;
            }
        }
        else
        {
            if (curSpeed > -maxSpeed - 0.5f && curSpeed < -maxSpeed + 0.5f)
            {
                curSpeed = -maxSpeed;
                isDashing = false;
            }
            else if (curSpeed > -maxSpeed)
            {
                curSpeed -= speedChange * Time.deltaTime;
            }
            else if (curSpeed < -maxSpeed)
            {
                curSpeed += speedChange * Time.deltaTime * 1.5f;
            }
        }

        //Applies gravity to the player if they aren't on the ground
        if (pc.hitBottom && !hitRoof)
        {
            currGravity = 0;
            hitRoof = true;
            fastFalling = true;
        }
        else if (pc.onGround)
        {
            hitRoof = false;
        }



        if (!pc.onGround) 
        {
            if (inpJump)
            {
                isHoldSpace = true;
            }
            else
            {
                isHoldSpace = false;
                fastFalling = true;
            }
            if(!fastFalling)    //Speeds up falling speed if going downwards
            {
                currGravity -= Time.deltaTime * gravMod;
            }
            else
            {
                currGravity -= Time.deltaTime * gravMod * 2.2f;
            }
            if (currGravity < -15)  //Clamps the maximum fall speed for a character
            {
                currGravity = -15;
            }
        }
        else
        {
            currGravity = 0;
            fastFalling = false;
        }

        //Allows the player to jump when touching a walkable surface
        if(pc.onGround && inpJump)
        {
            isHoldSpace = true;
            currGravity += jumpPower;
        }

        //Applies the inputs to the player
        rb.linearVelocity = horizontalMovement + addGravity;
    }

    public void increaseSpeed(){
        maxSpeed += 1;
    }
    public void increaseJump(){
        jumpPower += 1f;
    }
}
