using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteListChangeUIHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject notePrefab;

    [SerializeField]
    NoteManager noteManager;

    [SerializeField]
    private int orderInUI = 0;

    [SerializeField]
    private TextMeshProUGUI textField;

    [SerializeField]
    private Image m_Image;

    private void Start()
    {
        if (noteManager == null)
        {

            noteManager = GameObject.FindGameObjectWithTag("NoteManager").GetComponent<NoteManager>();
        }

        if (orderInUI != gameObject.transform.GetSiblingIndex())
        {
            orderInUI = gameObject.transform.GetSiblingIndex();
            Debug.LogWarning("Sibling Order Changed by script " + gameObject.name);
        }
        textField.text = "";
    }

    public void Change()
    {
        var myNote = noteManager.GetINoteAtIndex(orderInUI);


        if (textField != null)
        {
            if (myNote == default)
            {
                textField.text = "";
            }
            else
            {
                if (textField.text == "")
                {
                    var result = Instantiate(notePrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(-30, 30)));
                    result.transform.localScale = (Vector3.one * 0.8f) + (Random.insideUnitSphere * 0.4f);
                }
                textField.text = myNote.ToArrowString();
            }
        }

        if (noteManager.IsLastNote(orderInUI))
        {
            transform.DOShakePosition(0.2f, 0.2f, 5, 30);
        }
    }

    public void OnSongFilled()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.DOAnchorPosY(0.3f, 0.2f).SetDelay(orderInUI * 0.1f).SetLoops(2, LoopType.Yoyo).OnComplete(ClearUI);
    }


    private void ClearUI()
    {
        textField.text = "";
    }
}
