using UnityEngine;

public class CeilingLoop : MonoBehaviour
{
    public float width = 18.4f;

    private void Update()
    {
        if (transform.position.x < -width)
        {
            transform.position += new Vector3(width * 2f, 0, 0);
        }
    }
}
