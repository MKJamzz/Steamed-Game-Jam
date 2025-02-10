using Unity.VisualScripting;
using UnityEngine;

public class AnimationControllerScript : MonoBehaviour
{
    Animator ani;
    PlayerMovement pm;
    attackScript atk;
    private float horizontalInput;
    public float xOffset;
    public float yOffset;
    private bool shifted;
    Vector3 newPosition;
    Vector3 originalPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ani = GetComponent<Animator>();
        GameObject player = GameObject.Find("Player");
        pm = player.GetComponent<PlayerMovement>();
        GameObject pVis = GameObject.Find("PlayerVisuals");
        atk = pVis.GetComponent<attackScript>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo state = ani.GetCurrentAnimatorStateInfo(0);

        if (atk.swinging)
        {
            ani.SetBool("isattacking", true);
        }
        else if(pm.curSpeed < 0)
        {
            ani.SetBool("movingLeft", true);
            ani.SetBool("movingRight", false);
            ani.SetBool("isattacking", false);
            transform.localScale = new Vector3(-1, 1f, 1f);
        }
        else if(pm.curSpeed > 0)
        {
            ani.SetBool("movingLeft", false);
            ani.SetBool("movingRight", true);
            ani.SetBool("isattacking", false);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else{
            ani.SetBool("movingLeft", false);
            ani.SetBool("movingRight", false);
            ani.SetBool( "isattacking", false);
        }

    }
}
