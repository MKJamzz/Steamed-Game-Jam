using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class ScoreManagerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int score;
    public int lives;
    public int normalScraps;
    public TMP_Text st;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;


    void Start()
    {
        score = 0;
        lives = 3;
        normalScraps = 0;
        life1.gameObject.SetActive(true);
        life2.gameObject.SetActive(true);
        life3.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        st.text = ("Scraps: " + normalScraps);

        if(lives == 2)
        {
            life1.gameObject.SetActive(false);
        }
        else if (lives == 1)
        {
            life1.gameObject.SetActive(false);
            life2.gameObject.SetActive(false);
        }
        else if (lives == 0)
        {
            life1.gameObject.SetActive(false);
            life2.gameObject.SetActive(false);
            life3.gameObject.SetActive(false);

            SceneManager.LoadScene("GameOver");
        }
        else
        {
            life1.gameObject.SetActive(true);
            life2.gameObject.SetActive(true);
            life3.gameObject.SetActive(true);
        }




    }

    public void buyScrap()
    {
        normalScraps += 10;
    }
}
