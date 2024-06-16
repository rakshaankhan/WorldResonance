using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomOneShot : MonoBehaviour
{

    public AudioClip[] audioClips;
    int Audio;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void triggerPull()
	{
		Audio = Random.Range(0, audioClips.Length);
		audioSource.PlayOneShot(audioClips[Audio]);
	}

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonUp("Jump"))
        //{

            //Audio = Random.Range(0, audioClips.Length);
            //audioSource.PlayOneShot(audioClips[Audio]);
            //Debug.Log("Jump input detected! " + Audio);
        //}
    }
}