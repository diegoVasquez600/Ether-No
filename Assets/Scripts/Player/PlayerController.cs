using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(-1.5f, 0.5f, -44f);

    void Start()
    {
        transform.position = startPosition;
    }
}
