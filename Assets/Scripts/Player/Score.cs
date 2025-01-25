using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int health = 100;       
    public int maxHealth = 100;   
    public int power = 100;       
    public int maxPower = 100;    

    // Referencias a las barras de la UI
    public Slider healthBar;
    public Slider powerBar;

    private void Start()
    {
        // Configura los valores máximos de las barras al iniciar
        healthBar.maxValue = maxHealth;
        powerBar.maxValue = maxPower;

        // Actualiza las barras al iniciar
        healthBar.value = health;
        powerBar.value = power;
    }

    private void UpdateUI()
    {
        // Actualiza los valores de las barras
        healthBar.value = health;
        powerBar.value = power;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto es "Wine", intenta sumar vida
        if (other.gameObject.name.Contains("Wine"))
        {
            if (health < maxHealth)
            {
                health += 10;
                health = Mathf.Clamp(health, 0, maxHealth);
                Debug.Log("Vida aumentada. Vida actual: " + health);
            }
            else
            {
                Debug.Log("La vida ya está al máximo.");
            }

            Destroy(other.gameObject);
            UpdateUI(); // Actualiza la barra de vida
        }
        // Si el objeto es "Bread", intenta sumar poder
        else if (other.gameObject.name.Contains("Bread"))
        {
            if (power < maxPower)
            {
                power += 10;
                power = Mathf.Clamp(power, 0, maxPower);
                Debug.Log("Poder aumentado. Poder actual: " + power);
            }
            else
            {
                Debug.Log("El poder ya está al máximo.");
            }

            Destroy(other.gameObject);
            UpdateUI(); // Actualiza la barra de poder
        }
    }
}
