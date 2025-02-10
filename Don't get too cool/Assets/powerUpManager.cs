
using UnityEngine;

public class powerUpManager : MonoBehaviour
{
    public GameObject powerUpMenu;
    public GameObject attackSpeedPanel;
    public GameObject speedBoostPanel;
    public GameObject jumpBoostPanel;

    PlayerCollision pc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerCollision>();

        attackSpeedPanel.SetActive(false);
        speedBoostPanel.SetActive(false);
        jumpBoostPanel.SetActive(false);
        powerUpMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (pc.triggerModMenu)
        {

            pc.triggerModMenu = false;
            powerUpMenu.SetActive(true);
            attackSpeedPanel.SetActive(false);
            speedBoostPanel.SetActive(false);
            jumpBoostPanel.SetActive(false);
            int randRoll = Random.Range(0, 3);

            placeOption(randRoll, -100);

            int randRoll2 = Random.Range(0, 3);
                
            while (randRoll2 == randRoll)
            {
                randRoll2 = Random.Range(0, 3);
            }
            
            placeOption(randRoll2, 88);   

        }
    }

    public void exitMenu()
    {
        powerUpMenu.SetActive(false);
        attackSpeedPanel.SetActive(false);
        speedBoostPanel.SetActive(false);
        jumpBoostPanel.SetActive(false);
    }

    void placeOption(int panelNumber,float curPos)
    {
        Debug.Log($"Placing panel {panelNumber} at X: {curPos}");
        if (panelNumber == 0)
        {
            attackSpeedPanel.SetActive(true);
            attackSpeedPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(curPos, 0);
        }
        else if(panelNumber == 1){
            speedBoostPanel.SetActive(true);
            speedBoostPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(curPos, 0);
        }
        else
        {
            jumpBoostPanel.SetActive(true);
            jumpBoostPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(curPos, 0);
        }
    }
}
