using DG.Tweening;
using UnityEngine;

//TODO music manager needs to know the music and which scene we are on and when to change musics.
public class MusicManager : MonoBehaviour
{


    [SerializeField]
    private float fadeInTimer = 1f;
    [SerializeField]
    private float fadeOutTimer = 1f;

    private AudioSource musicPlayer;
    public static MusicManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        musicPlayer = GetComponent<AudioSource>();

    }

    private void Start()
    {
        FadeIN();
    }

    public void FadeIN()
    {
        musicPlayer.DOFade(1, fadeInTimer);
        musicPlayer.Play();
    }

    public void FadeOUT()
    {
        musicPlayer.DOFade(0, fadeOutTimer);
        musicPlayer.Stop();
    }

    public void ChangeClip(AudioClip clip)
    {
        musicPlayer.clip = clip;
    }
}
