using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectInstrumentUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform mainRotate;

    [SerializeField]
    private RectTransform slaveRotate;

    [SerializeField]
    private List<Transform> animationPath;
    [SerializeField]
    private AnimationCurve curveX;
    [SerializeField]
    private AnimationCurve curveY;


    [SerializeField]
    private List<Image> images;

    [SerializeField]
    private List<Sprite> ovverideSprites;

    [SerializeField]
    private List<Material> materials;

    private PlayerInstrument playerInstrument;

    private void Start()
    {
        // playerInstrument = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInstrument>();

    }

    private void OnEnable()
    {
        images[0].overrideSprite = ovverideSprites[0];
    }

    public void OnInstrumentChange()
    {
        if (playerInstrument == null)
        {
            playerInstrument = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInstrument>();
        }
        var type = playerInstrument.selectedInstrument.instrumentType;

        foreach (var image in images)
        {
            image.material = materials[0];
            image.overrideSprite = null;
        }
        images[(int) type].material = materials[1];
        images[(int) type].overrideSprite = ovverideSprites[(int) type];

        AnimateInstrumentUI((int) type);
    }


    private void AnimateInstrumentUI(int index)
    {

        mainRotate.DOLocalRotate((new Vector3(0, 0, 120 * index)), 1f);
        slaveRotate.DOLocalRotate((new Vector3(0, 0, 120 * index) * -1), 1f, RotateMode.FastBeyond360);
        images[index].transform.DOScale(Vector3.one * 2, 1f);

        for (int i = 0; i < images.Count; i++)
        {
            if (i == index) continue;
            images[i].transform.DOScale(Vector3.one, 1f);
        }
    }


}
