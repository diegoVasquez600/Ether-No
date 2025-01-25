using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(-1.5f, 0.5f, -44f);
    private Score healthPlayer;
    private Score powerPlayer;

    public int health;
    public int power;

    private void Awake()
    {
        healthPlayer = GetComponent<Score>();
        powerPlayer = GetComponent<Score>();
    }
    void Start()
    {
        transform.position = startPosition;
    }

    private void Update()
    {
        health = healthPlayer.health;
        power = powerPlayer.power;
    }
   
}
