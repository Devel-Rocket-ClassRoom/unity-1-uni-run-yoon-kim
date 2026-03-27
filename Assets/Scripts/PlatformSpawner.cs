using UnityEditor.ShortcutManagement;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] platformPrefabs = new GameObject[3];
    private int count;

    private float spawnTime = 1.6f;
    private float spawnTimer = 0f;
    private float posX;
    private float posY;
    private GameObject newTile;
    private GameObject oldTile;
    private int index;

    private void Start()
    {
        posX = 15.3f;
        posY = -4;
        count = 3;
        oldTile = Instantiate(platformPrefabs[0], new Vector3(posX, posY, 0), transform.rotation);
    }

    private void Update()
    {
        if (spawnTimer > spawnTime)
        {
            index = Random.Range(0, count);
            oldTile = newTile;
            newTile = Instantiate(platformPrefabs[index], new Vector3(posX, posY, 0), transform.rotation);
            spawnTimer = 0;
        }

        spawnTimer += Time.deltaTime;
    }
}
