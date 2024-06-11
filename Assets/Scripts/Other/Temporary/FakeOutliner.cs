using UnityEngine;

public class FakeOutliner : MonoBehaviour
{
    [SerializeField]
    private GameObject fakeOutline;

    public void OutlineON()
    {
        fakeOutline.transform.localScale = Vector3.one * 1.3f;
    }

    public void OutlineOFF()
    {
        fakeOutline.transform.localScale = Vector3.one * 0.9f;
    }
}
