using UnityEngine;
using UnityEngine.SceneManagement;

public class engineManager : MonoBehaviour
{
    triggerCollision tc;
    GameObject buyMenu;
    GameObject timeBar;
    GameObject brokePanel;
    ScoreManagerScript ps;
    public GameObject core;
    public float maxTime;
    public float curTime;
    private float timeSpeedMod = 1;
    private float freezeTime;
    private bool timeFrozen;
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject engineTrigger = GameObject.Find("Engine Trigger");
        GameObject scoreManager = GameObject.Find("ScoreManager");
        ps = scoreManager.GetComponent<ScoreManagerScript>();
        tc = engineTrigger.GetComponent<triggerCollision>();

        timeBar = GameObject.Find("time");
        buyMenu = GameObject.Find("Buy Menu");
        brokePanel = GameObject.Find("brokePanel");
        brokePanel.SetActive(false);
        buyMenu.SetActive(false);
        timeFrozen = false;


        maxTime = 40;
        curTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        bool interactPressed = Input.GetKeyDown(KeyCode.E);

        if(tc.playerInRange && interactPressed)
        {
            Debug.Log("Interact Pressed");
            buyMenu.SetActive(true);
        }
        else if (!tc.playerInRange)     
        {
            exitMenu();
        }

        if (timeFrozen)
        {
            freezeTime -= Time.deltaTime;
            if (freezeTime < 0)
            {
                freezeTime = 0;
                timeFrozen = false;
                timeSpeedMod += 0.3f;
            }
        }
        else
        {
            if (curTime > 0)
            {
                curTime -= Time.deltaTime * timeSpeedMod;
                ChangeXScale(curTime / maxTime);
            }
            else
            {
                curTime = 0;
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    public void exitMenu()
    {
        buyMenu.SetActive(false);
        brokePanel.SetActive(false);
    }

    public void buyTime()
    {
        if (checkCurrency(7)){
            ps.normalScraps -= 7;
            if (curTime + 5 <= maxTime)
            {
                curTime += 10;
            }
            else if (curTime + 5 >= maxTime)
            {
                curTime = maxTime;
            }
            exitMenu();
        }
        else
        {
            brokePanel.SetActive(true);
            Invoke("flashBrokePanel", 1f);
        }
    }

    public void freezeTimer()
    {
        if (checkCurrency(10))
        {
            ps.normalScraps -= 10;
            timeFrozen = true;
            freezeTime = 15;
            exitMenu();
        }
        else
        {
            brokePanel.SetActive(true);
            Invoke("flashBrokePanel", 1f);
        }

    }

    public void forge()
    {
        if (checkCurrency(5) && curTime > 5)
        {
            ps.normalScraps -=5;
            curTime -= 3;
            exitMenu();
            int coreScrapChance = Random.Range(0, 3);
            Vector3 spawnPos = new Vector3(0.12f, 2.25f, 0.03f);

            if (coreScrapChance == 1)
            {
                GameObject coreScrap = Instantiate(core,spawnPos, Quaternion.identity);
                scrapFloating ns = coreScrap.GetComponent<scrapFloating>();
                ns.currGravity = (Random.Range(0.5f, 2f));
                ns.currSpeed = (Random.Range(-3, 3f));
            }

        }
        else
        {
            brokePanel.SetActive(true);
            Invoke("flashBrokePanel", 1f);
        }
    }
    void ChangeXScale(float newXScale)
    {
        Vector3 newScale = timeBar.transform.localScale; // Get current scale
        newScale.x = newXScale; // Modify only the X scale
        timeBar.transform.localScale = newScale; // Apply the new scale
    }

    bool checkCurrency(int currency)
    {
        if(ps.normalScraps < currency)
        {
            return false;
        }
        return true;
    }

    void flashBrokePanel()
    {
        brokePanel.SetActive(false);
        exitMenu();
    }
    public void buyNextLevel()
    {
        if (ps.normalScraps >= 50)
        {
            SceneManager.LoadScene("Level 2");
        }
        else
        {
            brokePanel.SetActive(true);
            Invoke("flashBrokePanel", 1f);
        }
    }
    public void fixMachine()
    {
        if (ps.normalScraps >= 100 && SceneManager.GetActiveScene().name == "Level 2")
        {
            SceneManager.LoadScene("StartScene");
        }
        else
        {
            brokePanel.SetActive(true);
            Invoke("flashBrokePanel", 1f);
        }
    }
}
