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
    }

    private Inventory playersInventory;

    private void Start()
    {
        playersInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void ChangeText()
    {
        textField.text = " " + playersInventory.GetItemCount(itemIdRepresented);
    }
}
