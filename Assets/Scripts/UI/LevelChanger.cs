using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField]
    private LastSceneInfo lastSceneInfo;
    [SerializeField]
    private GameEvent sceneIsChanging;

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
    [SerializeField]
    private bool disabled = true;
    [SerializeField]
    private bool fadeOut = false;
    [SerializeField]
    private bool fadeIn = false;
    [SerializeField]
    private bool teleportPlayer = true;


    private void Start()
    {


        if (nextScene.SceneName.Equals(lastSceneInfo.lastSceneName) && teleportPlayer == true)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;

            if (fadeIn == true)
            {
                animator.SetTrigger("FadeIn");
            }
        }
        else
        {
            disabled = false;
        }

    }

    public void FadeToNextLevel()
    {
        activated = true;
        animator.SetTrigger("FadeOut");

    }

    public void OnFadeComplete()
    {
        //TODO assumes SceneLoading is going to be done.
        lastSceneInfo.lastSceneName = SceneManager.GetActiveScene().name;
        sceneIsChanging.TriggerEvent();

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
        if (disabled == true) return;
        if (activated == true) return;

        if (fadeOut == true)
        {
            FadeToNextLevel();
        }
        else
        {
            OnFadeComplete();
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        disabled = false;

    }
}