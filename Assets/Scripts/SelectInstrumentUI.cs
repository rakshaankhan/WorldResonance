using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectInstrumentUI : MonoBehaviour
{
    [SerializeField]
    private List<Transform> animationPath;
    [SerializeField]
    private AnimationCurve curveX;
    [SerializeField]
    private AnimationCurve curveY;


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
        //transform.DOPath();
        MoveElements(0, type);
    }

    private IEnumerator MoveElements(int index, PlayerInstrument.InstrumentType type)
    {
        float time = 0;
        time += Time.deltaTime;
        var y = curveY.Evaluate((float) type / 2) * 200;
        var x = curveX.Evaluate((float) type / 2) * 300;
        images[index].transform.parent.GetComponent<RectTransform>().DOAnchorPos3DY(y, 0.1f);
        images[index].transform.parent.GetComponent<RectTransform>().DOAnchorPos3DX(x, 0.1f);

        return null;
    }


}
