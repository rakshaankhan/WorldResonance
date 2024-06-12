using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    /// <summary>
    /// We first check name for the next level
    /// </summary>
    [Tooltip("You only need to enter level index or name")]
    [SerializeField]
    private string nextLevelname;
    /// <summary>
    ///
    /// </summary>
    [Tooltip("You only need to enter level index or name")]
    [SerializeField]
    private int nextLevelindex;

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

        if (string.IsNullOrEmpty(nextLevelname) == false)
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