using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Mainmenu : MonoBehaviour
{
    [SerializeField]
    private SceneField optionsScene;

    [SerializeField]
    private Button conButton;

    [SerializeField]
    private EventSystem eventSystem;

    private void Start()
    {
        //TODO 
        conButton.interactable = !DataManager.instance.isNewGame();//NOT
        if (conButton.IsInteractable())
        {
            eventSystem.firstSelectedGameObject = conButton.gameObject;
        }
    }

    public void Continue()
    {
        var sceneTOLoad = DataManager.instance.GetLastScene();
        SceneManager.LoadScene(sceneTOLoad);
    }
    // This method is called when the Play button is pressed
    public void PlayGame()
    {
        DataManager.instance.ResetGameState();
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
