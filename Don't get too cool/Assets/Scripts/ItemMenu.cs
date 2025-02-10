using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour
{
    public GameObject player; 
    public GameObject menuUI;
    public float noticeRange = 5;

    public bool enteredRange = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        menuUI.SetActive(false); // Hide menu initially
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if(dist < noticeRange && !enteredRange)
        {
            enteredRange = true;
            menuUI.SetActive(true);
        }else{
            if(dist > noticeRange){
                enteredRange = false;
                menuUI.SetActive(false);
            }
        }
    }

    public void hideMenu(){
        menuUI.SetActive(false);
    }
}
