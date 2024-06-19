using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectInstrumentUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> images;

    [SerializeField]
    private List<Material> materials;

    private PlayerInstrument playerInstrument;
    private void Start()
    {
        playerInstrument = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInstrument>();
    }
    public void OnInstrumentChange()
    {
        var type = playerInstrument.selectedInstrument.instrumentType;

        foreach (var image in images)
        {
            image.material = materials[0];
        }
        //images[(int) type].material.SetFloat("_Outline", 1);
        images[(int) type].material = materials[1];
    }


}
