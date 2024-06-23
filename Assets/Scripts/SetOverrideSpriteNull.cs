using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class SetOverrideSpriteNull : MonoBehaviour
{
    private Image mImage;
    private void Awake()
    {
        mImage = GetComponent<Image>();
    }

    public void SetOverrideSpriteNULL()
    {
        mImage.overrideSprite = null;

    }
}
