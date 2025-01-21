using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip hoverSound;  // Sonido al pasar el cursor
    public AudioClip clickSound; // Sonido al hacer clic

    private AudioSource audioSource;

    private void Awake()
    {
        // Obtener el componente AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        // Conectar los eventos del botón
        Button button = GetComponent<Button>();
        button.onClick.AddListener(PlayClickSound);
    }

    public void PlayHoverSound()
    {
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
