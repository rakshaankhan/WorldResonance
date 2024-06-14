using UnityEngine;
using UnityEngine.SceneManagement;
public class Mainmenu : MonoBehaviour
{
    [SerializeField]
    private SceneField optionsScene;

    // This method is called when the Play button is pressed
    public void PlayGame()
    {
        // Load the next scene by build index (assuming the main menu is at index 0)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // This method is called when the Options button is pressed
    public void OpenOptions()
    {
        // Load the options scene (assuming options are at build index 2)
        SceneManager.LoadScene(optionsScene);
        Debug.Log("Options");
    }

    // This method is called when the Quit button is pressed
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
