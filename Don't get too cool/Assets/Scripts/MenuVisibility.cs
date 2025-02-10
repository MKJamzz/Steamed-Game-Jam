using UnityEngine;

public class MenuVisibility : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false); // Hide menu initially
    }

    // Update is called once per frame
    void Update()
    {
    }    
    public void HideSelf()
    {
        gameObject.SetActive(false);
    }
}