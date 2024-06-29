using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class BossManager : MonoBehaviour
{
    [Range(0, 2)]
    [SerializeField]
    private int selectedInstrumentIndex = 2;

    [SerializeField]
    private BossRockSpawner rockSpawner;

    public UnityEvent changedNotes;

    [SerializeField]
    private List<GameObject> platforms;

    [SerializeField]
    private NoteManager noteManager;

    [SerializeField]
    private Interactables myInteractable;

    [SerializeField]
    private float songNoteInterval = 0.2f;

    private AudioSource audioSource;


    [SerializeField]
    private Light2D eyeLight2D;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [Header("Boos Fight Paramaters")]
    [SerializeField]
    private int turnCount = 20;
    [SerializeField]
    private float turnDelay = 10;
    [SerializeField]
    private int winCounter = 10;

    [SerializeField]
    private int difficultyRampThreshold = 4;

    private bool countered = false;
    private int counteredCount = 0;

    public Queue<PlayerInstrument.Note> notes = new(NoteManager.NOTE_SIZE);
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        myInteractable = GetComponent<Interactables>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        PlayRandomSong();

        DOVirtual.DelayedCall(turnDelay, PlayRandomSong).SetLoops(turnCount).SetEase(Ease.InSine);
    }


    public void PlayRandomNote()
    {
        var clip = noteManager.GetRandomNoteClipFromInstrument(instrumentIndex: selectedInstrumentIndex);
        audioSource.PlayOneShot(clip);
    }


    public void PlayRandomSong()
    {
        if (counteredCount > difficultyRampThreshold)
        {
            rockSpawner.SpawnRockRandom();

        }
        notes.Clear();
        myInteractable.acceptedNoteOrder.Clear();
        myInteractable.activated = false;
        Sequence s = DOTween.Sequence();
        for (int i = 0; i < 6; i++)
        {
            var note = (PlayerInstrument.Note) Random.Range(1, 5);
            notes.Enqueue(note);
            myInteractable.acceptedNoteOrder.Add(note);

            s.AppendCallback(PlayRandomNote);
            s.AppendInterval(songNoteInterval);
            transform.DOPunchPosition(Random.insideUnitCircle, 1f);
        }
        changedNotes.Invoke();
        if (countered && counteredCount < 5)
        {
            countered = false;
            return;
        }

        FindHighestPlatform(platforms).transform.DOBlendableMoveBy(Vector3.down * Random.Range(minInclusive: 5, 15), 1f).SetEase(Ease.OutBounce);

        var y = FindAverageYValue(platforms);
        transform.DOMoveY(y, 2f).SetEase(Ease.InElastic);


        DOTween.To(() => eyeLight2D.intensity, x => eyeLight2D.intensity = x, 200f, 0.2f).SetLoops(2, LoopType.Yoyo); ;
        DOTween.To(() => eyeLight2D.color, x => eyeLight2D.color = x, Color.black, 0.2f).SetLoops(2, LoopType.Yoyo); ;

    }


    private GameObject FindHighestPlatform(List<GameObject> platforms)
    {
        return platforms.OrderByDescending(platform => platform.transform.position.y).FirstOrDefault();
    }

    private float FindAverageYValue(List<GameObject> platforms)
    {
        if (platforms == null || platforms.Count == 0)
            return float.NaN; // or any other value indicating an invalid result

        float totalY = platforms.Sum(platform => platform.transform.position.y);
        float averageY = totalY / platforms.Count;
        return averageY;
    }

    public void CounterBossSong()
    {
        countered = true;
        counteredCount++;
        transform.DOPunchPosition(Vector3.left * 1, 0.3f);

        spriteRenderer.color = Color.Lerp(Color.red, Color.green, counteredCount / winCounter);

        rockSpawner.disabled = true;

        notes.Clear();
        changedNotes.Invoke();

        //transform.DOShakePosition(0f, 10f,);
    }

    internal PlayerInstrument.Note GetINoteAtIndex(int orderInUI)
    {
        if (notes.Count == 0) return PlayerInstrument.Note._;
        return notes.ElementAt(orderInUI);
    }
}
