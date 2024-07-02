using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(GuidID))]
public class PersistentEvent : MonoBehaviour, IDataSaveLoad
{

    [SerializeField]
    public UnityEvent Activated;

    private bool isActivated;

    [SerializeField]
    private bool isOriginal = true;


    public void Load(PersistentGameData gameData)
    {
        if (TryGetComponent(out GuidID guidID))
        {
            if (string.IsNullOrEmpty(guidID.id)) return;

            Debugger.Log("Checking for Loading object name " + gameObject.name + " and GUID " + guidID.id, Debugger.PriorityLevel.Low);
            if (gameData.collectedItemGuids.Contains(guidID.id))
            {
                Debugger.Log("Object Loaded" + guidID.id, Debugger.PriorityLevel.Low);
                isActivated = true;
                Activated.Invoke();
            }
            else
            {
                Debugger.Log("Object could not find" + guidID.id, Debugger.PriorityLevel.Low);
            }

        }
    }

    public void Save(PersistentGameData gameData)
    {
        if (isActivated == false) return;
        if (isOriginal == false) return;

        if (TryGetComponent(out GuidID guidID))
        {
            if (gameData.collectedItemGuids.Contains(guidID.id) == false)
            {
                gameData.collectedItemGuids.Add(guidID.id);
            }

        }
    }


    public void SetObjectActivated()
    {

        isActivated = true;
    }

}
