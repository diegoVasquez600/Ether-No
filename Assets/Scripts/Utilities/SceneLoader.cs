using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.Log("El nombre de la Escena esta nula o vacio");
            return;
        }
        if (Application.CanStreamedLevelBeLoaded(sceneName))
            SceneManager.LoadScene(sceneName);
        else
            Debug.Log($"La escena {sceneName} no existe o no esta en el Build Settings");
    }
    public void LoadSceneByIndex(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("El índice de la escena es inválido.");
            return;
        }

        SceneManager.LoadScene(sceneIndex);
    }
}
