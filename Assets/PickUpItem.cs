using UnityEngine;
using UnityEngine.Events;


public class PickUpItem : MonoBehaviour, IDataSaveLoad
{
    [SerializeField]
    private int itemID;

    private bool inRange;

    private bool isPicked;

    public UnityEvent GettingInRange;
    public UnityEvent GettingOutOfRange;

    private GameObject player;
    private Inventory inventory;

    private void Awake()
    {
        if (isPicked) gameObject.SetActive(false);
    }

    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        if (isPicked) gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPicked == true) return;

        inRange = true;
        InvokeChange();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPicked == true) return;

        inRange = false;
        InvokeChange();
    }


    private void InvokeChange()
    {
        if (inRange == true)
        {
            GettingInRange.Invoke();
        }
        else
        {
            GettingOutOfRange.Invoke();
        }

    }

    public void Interact()
    {
        if (inRange == false) return;

        if (isPicked == false)
        {
            Debug.Log("Pick Up");
            inventory.AddItem(itemID);
            gameObject.SetActive(false);
        }

    }


    public void Save(PersistentGameData gameData)
    {
        if (TryGetComponent(out GuidID guidID))
        {
            if (gameData.collectedItemGuids.Contains(guidID.id) == false)
            {
                gameData.collectedItemGuids.Add(guidID.id);
            }

        }

    }

    public void Load(PersistentGameData gameData)
    {
        if (TryGetComponent(out GuidID guidID))
        {
            if (gameData.collectedItemGuids.Contains(guidID.id))
            {
                isPicked = true;
            }

        }
    }
}
