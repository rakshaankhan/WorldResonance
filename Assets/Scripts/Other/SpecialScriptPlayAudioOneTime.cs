using UnityEngine;

public class SpecialScriptPlayAudioOneTime : MonoBehaviour
{
    [SerializeField]
    private LastSceneInfo lastSceneInfo;


    [SerializeField]
    private SceneField PlayWhenfromScene;

    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        if (lastSceneInfo.lastSceneName.Equals(PlayWhenfromScene.SceneName))
        {
            audioSource.enabled = true;
        }
    }

}
