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



    private Collider2D mycollider;
    private void Awake()
    {
        mycollider = GetComponent<Collider2D>();

    }
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


    private void OnDrawGizmos()
    {
        if (mycollider == null)
        {
            mycollider = GetComponent<Collider2D>();
        }

        if (mycollider != null)
        {
            Gizmos.color = Color.green;

            if (mycollider is BoxCollider2D)
            {
                BoxCollider2D boxCollider = (BoxCollider2D) mycollider;
                Vector2 size = boxCollider.size;
                Vector2 offset = boxCollider.offset;

                Vector3 topLeft = transform.TransformPoint(new Vector3(offset.x - size.x / 2, offset.y + size.y / 2, 0));
                Vector3 topRight = transform.TransformPoint(new Vector3(offset.x + size.x / 2, offset.y + size.y / 2, 0));
                Vector3 bottomLeft = transform.TransformPoint(new Vector3(offset.x - size.x / 2, offset.y - size.y / 2, 0));
                Vector3 bottomRight = transform.TransformPoint(new Vector3(offset.x + size.x / 2, offset.y - size.y / 2, 0));

                Gizmos.DrawLine(topLeft, topRight);
                Gizmos.DrawLine(topRight, bottomRight);
                Gizmos.DrawLine(bottomRight, bottomLeft);
                Gizmos.DrawLine(bottomLeft, topLeft);
            }
        }
    }
}