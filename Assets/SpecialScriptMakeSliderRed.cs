using UnityEngine;
using UnityEngine.UI;

public class SpecialScriptMakeSliderRed : MonoBehaviour
{
    [SerializeField]
    private float warningDelta = 0.2f;

    [SerializeField]
    private Image ourImage;
    [SerializeField]
    private Slider ourSlider;

    [SerializeField]
    private Slider enemySlider;



    private void OnEnable()
    {

        enemySlider.onValueChanged.AddListener(delegate { Check(); });
    }

    private void OnDisable()
    {
        enemySlider.onValueChanged.RemoveListener(delegate { Check(); });
    }


    public void Check()
    {
        if (enemySlider.value - ourSlider.value > warningDelta)
        {
            ourImage.color = Color.red;
        }
        else
        {
            ourImage.color = Color.white;
        }
    }

    public void CheckOurs(float x)
    {
        Check();
    }
}
