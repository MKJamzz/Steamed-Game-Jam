using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTimer;
    public float currTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;

        if (currTime > spawnTimer)
        {
            float randomLocation = Random.Range(transform.position.x -20f, transform.position.x + 20);
            float randomY = Random.Range(transform.position.y - 20, transform.position.y + 20);
            Instantiate(enemy, new Vector3(randomLocation, 1.448218f, 0), Quaternion.identity);
            currTime = 0;
        }
    }
}
