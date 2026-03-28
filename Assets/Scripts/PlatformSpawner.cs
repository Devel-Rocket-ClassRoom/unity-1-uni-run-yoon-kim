using UnityEditor.ShortcutManagement;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    public GameObject startPlatform;
    private int count;

    private float spawnPosX;
    private float spawnPosY;
    private GameObject newTile;
    private GameObject oldTile;
    private int index;

    private void Start()
    {
        count = platformPrefabs.Length;
        spawnPosX = 18.4f;
        spawnPosY = -4;
        Destroy(startPlatform, 4f);
        newTile = Instantiate(platformPrefabs[0], new Vector3(spawnPosX, spawnPosY, 0), transform.rotation);
    }

    private void Update()
    {
        if (newTile.transform.position.x < 0)
        {
            index = Random.Range(0, count);
            Destroy(oldTile);
            oldTile = newTile;
            newTile = Instantiate(platformPrefabs[index], new Vector3(spawnPosX, spawnPosY, 0), transform.rotation);
        }
    }
}
