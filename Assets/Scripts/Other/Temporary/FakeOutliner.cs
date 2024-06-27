using UnityEngine;

public class FakeOutliner : MonoBehaviour
{
    [SerializeField]
    private GameObject fakeOutline;

    [SerializeField]
    private SpriteRenderer objectItself;

    [SerializeField]
    private Material outlineOff;

    [SerializeField]
    private Material outlineON;


    public void OutlineON()
    {
        //fakeOutline.transform.localScale = Vector3.one * 1.3f;
        objectItself.material = outlineON;
    }

    public void OutlineOFF()
    {
        //fakeOutline.transform.localScale = Vector3.one * 0.9f;
        objectItself.material = outlineOff;
    }
}
