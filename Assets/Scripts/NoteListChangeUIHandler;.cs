using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteListChangeUIHandler : MonoBehaviour
{
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
                textField.text = myNote.ToString();
            }


        }
    }
}
