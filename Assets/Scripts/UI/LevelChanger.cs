using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{


    [Header("Enter your preffered way to go to next scene")]
    [Header("Entering only one of them is enough")]
    [Space()]
    [Tooltip("This uses CustomPropertyDrawer so you can select scene directly, should eliminate string errors")]
    [SerializeField]
    private SceneField nextScene;

    [Space()]
    [Tooltip("Simple name")]
    [SerializeField]
    private string nextLevelname;

    [Space()]
    [Tooltip("Simple Level index")]
    [SerializeField]
    private int nextLevelindex;
    [Space()]
    [Header("")]

    [SerializeField]
    public Animator animator;
    private bool activated = false;




    public void FadeToNextLevel()
    {
        activated = true;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        if (nextScene != null && string.IsNullOrEmpty(nextScene.SceneName) == false)
        {
            SceneManager.LoadScene(nextScene);
        }
        else if (string.IsNullOrEmpty(nextLevelname) == false)
        {
            SceneManager.LoadScene(nextLevelname);
        }
        else if (nextLevelindex > 0)
        {
            SceneManager.LoadScene(nextLevelindex);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated == true) return;

        FadeToNextLevel();

    }
}