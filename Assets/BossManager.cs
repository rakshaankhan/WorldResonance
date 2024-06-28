using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> platforms;

    [SerializeField]
    private NoteManager noteManager;

    [SerializeField]
    private float songNoteInterval = 0.2f;

    private AudioSource audioSource;


    [SerializeField]
    private Light2D eyeLight2D;

    [Header("Boos Fight Paramaters")]
    [SerializeField]
    private int turnCount = 20;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        DOVirtual.DelayedCall(3f, PlayRandomSong).SetLoops(turnCount);
    }

    [ContextMenu("Play random Note")]
    public void PlayRandomNote()
    {
        var clip = noteManager.GetRandomNoteClip();
        audioSource.PlayOneShot(clip);
    }

    [ContextMenu("Play random Song")]
    public void PlayRandomSong()
    {
        Sequence s = DOTween.Sequence();
        for (int i = 0; i < 6; i++)
        {

            s.AppendCallback(PlayRandomNote);
            s.AppendInterval(songNoteInterval);
            transform.DOPunchPosition(Random.insideUnitCircle, 1f);
        }

        FindHighestPlatform(platforms).transform.DOBlendableMoveBy(Vector3.down * Random.Range(3, 10), 1f).SetEase(Ease.OutBounce);

        var y = FindAverageYValue(platforms);
        transform.DOMoveY(y, 2f).SetEase(Ease.InElastic);

        //old
        DOTween.To(() => eyeLight2D.intensity, x => eyeLight2D.intensity = x, 200f, 0.2f).SetLoops(2, LoopType.Yoyo); ;
        DOTween.To(() => eyeLight2D.color, x => eyeLight2D.color = x, Color.black, 0.2f).SetLoops(2, LoopType.Yoyo); ;
        //  eyeLight2D.DOIntensity(1, 1);
        //  PlayRandomNote();
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

}
