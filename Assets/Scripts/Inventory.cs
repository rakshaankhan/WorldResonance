using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameEvent updateUI;


    private List<int> itemCounts;


    void Start()
    {
        itemCounts = new List<int> { 10, 10, 10 };

        UpdateUI();
    }


    public void AddItem(int itemID)
    {
        if (itemID < 0 || itemID >= itemCounts.Count) return;

        itemCounts[itemID]++;
        UpdateUI();
    }

    public void UseItem(int itemID)
    {
        if (itemID < 0 || itemID >= itemCounts.Count) return;

        if (itemCounts[itemID] > 0)
        {
            Debug.Log("Used Item " + (itemID + 1));
            itemCounts[itemID]--;
        }
        UpdateUI();
    }

    public int GetItemCount(int itemID)
    {
        if (itemID < 0 || itemID >= itemCounts.Count) return 0;

        return itemCounts[itemID];
    }
    void UpdateUI()
    {
        updateUI.TriggerEvent();
    }
}