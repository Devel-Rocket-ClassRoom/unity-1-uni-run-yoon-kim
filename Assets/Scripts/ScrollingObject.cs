using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public static float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }
}
