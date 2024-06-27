using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour, IDataSaveLoad
{
    [SerializeField]
    private GameEvent updateUI;


    private List<int> itemCounts;


    private PlayerInstrument playerInstrument;

    private void Awake()
    {
        playerInstrument = GetComponent<PlayerInstrument>();
        itemCounts = DataManager.instance.GetItemCounts();
    }
    void Start()
    {

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

    public int GetSum()
    {
        if (itemCounts == null) return 0;

        return itemCounts.Sum();
    }
    void UpdateUI()
    {
        playerInstrument.CheckInventoryForInstruments();
        updateUI.TriggerEvent();
    }

    public void Save(PersistentGameData gameData)
    {
        gameData.collectedInstruments = itemCounts;
    }

    public void Load(PersistentGameData gameData)
    {
        itemCounts = gameData.collectedInstruments;
    }
}