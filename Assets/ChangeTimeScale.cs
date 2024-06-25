using UnityEngine;

public class ChangeTimeScale : MonoBehaviour
{
    public void ChangeTimeScaleOfGame(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
