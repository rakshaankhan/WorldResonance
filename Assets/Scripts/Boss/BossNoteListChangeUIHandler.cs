using DG.Tweening;
using TMPro;
using UnityEngine;

public class BossNoteListChangeUIHandler : MonoBehaviour
{
    [SerializeField]
    private int orderInUI = 0;

    [SerializeField]
    private TextMeshProUGUI textField;

    [SerializeField]
    private BossManager bossManager;

    private void Awake()
    {
        textField = GetComponentInChildren<TextMeshProUGUI>();
        bossManager = transform.parent.GetComponentInParent<BossManager>();
    }
    void Start()
    {
        if (orderInUI != gameObject.transform.GetSiblingIndex())
        {
            orderInUI = gameObject.transform.GetSiblingIndex();
        }
        bossManager.changedNotes.AddListener(Change);
    }

    private void OnDestroy()
    {
        bossManager.changedNotes.RemoveListener(Change);
    }
    private void ClearUI()
    {
        textField.text = "";
    }

    public void Change()
    {
        PlayerInstrument.Note myNote = bossManager.GetINoteAtIndex(orderInUI);
        DOVirtual.DelayedCall(orderInUI * 0.1f, () => ChangeSymbols(myNote));
    }

    private void ChangeSymbols(PlayerInstrument.Note myNote)
    {
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
                    //var result = Instantiate(notePrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(-30, 30)));
                    //result.transform.localScale = (Vector3.one * 0.8f) + (Random.insideUnitSphere * 0.4f);
                }
                textField.text = myNote.ToArrowString();
            }
        }
    }
}
