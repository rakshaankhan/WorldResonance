using DG.Tweening;
using UnityEngine;

public class SpecialScriptMoveBird : MonoBehaviour
{
    [SerializeField]
    private GameObject flyingNotesPrefab;

    [SerializeField]
    private GameObject bird;

    [SerializeField]
    private Transform birdTarget;
    [SerializeField]
    private float birdFlyTime;

    [SerializeField]
    private NoteManager noteManager;
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated == true) return;
        activated = true;
        MoveBird();

    }


    private void MoveBird()
    {
        bird.transform.DOMove(birdTarget.position, birdFlyTime).OnComplete(Sing);
    }

    private void Sing()
    {
        bird.GetComponent<SpriteRenderer>().flipX = true;
        Sequence s = DOTween.Sequence();


        s.AppendInterval(1f);

        Append(s, PlayerInstrument.Note.A, 0.3f);
        Append(s, PlayerInstrument.Note.B, 0.3f);
        Append(s, PlayerInstrument.Note.C, 0.5f);
        Append(s, PlayerInstrument.Note.D, 1.0f);
        Append(s, PlayerInstrument.Note.B, 0.2f);
        Append(s, PlayerInstrument.Note.B, 0.2f);




        //s.AppendCallback(() => noteManager.SpecialEnque(PlayerInstrument.Note.B));
        //s.AppendInterval(0.2f);
        //s.AppendCallback(() => noteManager.SpecialEnque(PlayerInstrument.Note.C));
        //s.AppendInterval(0.3f);
        //s.AppendCallback(() => noteManager.SpecialEnque(PlayerInstrument.Note.D));
        //s.AppendInterval(0.6f);
        //s.AppendCallback(() => noteManager.SpecialEnque(PlayerInstrument.Note.B));
        //s.AppendInterval(0.2f);
        //s.AppendCallback(() => noteManager.SpecialEnque(PlayerInstrument.Note.B));

    }

    private void Append(Sequence s, PlayerInstrument.Note note, float delay)
    {
        s.AppendCallback(() =>
        {
            noteManager.SpecialEnque(note);
            var result = Instantiate(flyingNotesPrefab, transform.position + MyRandomLoc() * 4, Quaternion.Euler(0, 0, Random.Range(-15, 15)));
            result.transform.localScale = (Vector3.one * 0.6f) + (Random.insideUnitSphere * 0.4f);

        });
        s.AppendInterval(delay);
        // Instantiate(flyingNotesPrefab, transform.position, Quaternion.identity);

    }

    private Vector3 MyRandomLoc()
    {
        Vector3 vector3 = Vector3.zero;

        vector3.x = Random.Range(-1f, 1);
        vector3.y = Random.Range(0.3f, 1);

        return vector3;
    }
}
