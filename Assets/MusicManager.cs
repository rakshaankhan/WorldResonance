using DG.Tweening;
using UnityEngine;

//TODO music manager needs to know the music and which scene we are on and when to change musics.
public class MusicManager : MonoBehaviour
{


    [SerializeField]
    private float fadeInTimer = 1f;
    [SerializeField]
    private float fadeOutTimer = 1f;



    [Header("Note FadeOuts-Change in Editor Mode only")]
    [SerializeField]
    private float fadeInTimerForNotes = 1f;
    [SerializeField]
    private float fadeOutTimerForNotes = 1f;

    [Range(0f, 1f)]
    [SerializeField]
    private float fadeInValueForNotes = 1f;
    [Range(0f, 1f)]
    [SerializeField]
    private float fadeOutValueForNotes = 1f;


    private AudioSource musicPlayer;
    public static MusicManager instance;


    private Sequence fadeoutForNotesTween;
    private Sequence fadeInForNotesTween;
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
        fadeoutForNotesTween = DOTween.Sequence().SetAutoKill(false);
        fadeInForNotesTween = DOTween.Sequence().SetAutoKill(false);

        fadeoutForNotesTween.Append(musicPlayer.DOFade(fadeOutValueForNotes, fadeOutTimerForNotes)).Pause();
        fadeInForNotesTween.Append(musicPlayer.DOFade(fadeInValueForNotes, fadeInTimerForNotes)).Pause();
        //theSequence.Append
        //fadeInForNotesTween
        //theSequence.Play();
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

    public void FadeOutForNotes()
    {
        if (fadeInForNotesTween != null && fadeInForNotesTween.IsPlaying() == true) return;
        if (fadeoutForNotesTween != null && fadeoutForNotesTween.IsPlaying() == true) return;
        fadeoutForNotesTween.Restart();
        fadeoutForNotesTween.Play();
    }

    public void FadeInForNotes()
    {
        fadeoutForNotesTween.Pause();
        fadeInForNotesTween.Restart();
        fadeInForNotesTween.Play();
    }
}
