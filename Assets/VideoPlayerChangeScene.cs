using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoPlayerChangeScene : MonoBehaviour
{

    private VideoPlayer player;



    private void Start()
    {
        player = GetComponent<VideoPlayer>();

        Invoke("ChangeLevel", (float) player.clip.length);

    }


    private void ChangeLevel()
    {
        SceneManager.LoadScene(2);
    }

}
