using DG.Tweening;
using TMPro;
using UnityEngine;

public class InventoryUITextHandler : MonoBehaviour
{
    [SerializeField]
    private int itemIdRepresented;

    TextMeshProUGUI textField;

    private void Awake()
    {
        textField = GetComponent<TextMeshProUGUI>();
        playersInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private Inventory playersInventory;


    public void ChangeText()
    {
        textField.text = " " + playersInventory.GetItemCount(itemIdRepresented);
        transform.DOShakeScale(0.2f);
    }
}
