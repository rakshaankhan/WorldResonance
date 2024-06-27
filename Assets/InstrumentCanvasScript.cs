using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class InstrumentCanvasScript : MonoBehaviour
{

    [SerializeField]
    private GameObject mainInstrumentPanel;

    [SerializeField]
    private List<GameObject> instrumentImages = new List<GameObject>();

    private static InstrumentCanvasScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnUIChange()
    {
        var result = DataManager.instance.GetItemCounts();
        if (result.Count == 0) return;

        mainInstrumentPanel.SetActive(result.Sum() > 0);
        for (int i = 0; i < result.Count; i++)
        {
            instrumentImages[i].SetActive(result[i] > 0);
        }
    }
}
