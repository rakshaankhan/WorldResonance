using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelEventsManager : MonoBehaviour
{
    public static LevelEventsManager Instance;
    public int level { get; private set; }
    public bool canGlide = false;
    
    PlayerManager playerManager;

    private void Awake()
    {
        if (LevelEventsManager.Instance != null) { Destroy(gameObject); return; }
        else { Instance = this; }
        level = SceneManager.GetActiveScene().buildIndex;
        if (GameObject.Find("Player").TryGetComponent(out PlayerManager pm) )
        {
            playerManager = pm;
            playerManager.enabled = true;
           
        }

    }

    private void OnEnable()
    {
        onPauseActivity += playerManager.PausePlayerActivity;
        onUnPauseActivity += playerManager.UnPausePlayerActivity;
 
    }

    private void OnDisable()
    {
        onPauseActivity -= playerManager.PausePlayerActivity;
        onUnPauseActivity -= playerManager.UnPausePlayerActivity;
 
    }


    
    

    public event Action onJumpCancel;
    public void JumpCancel()
    {
        if (onJumpCancel != null) { onJumpCancel(); }

    }




    public event Action onInteract;
    public void Interact()
    {
        if(onInteract != null) { onInteract(); }
    }

    public event Action onPauseActivity;
    public void PauseActivity()
    {
        if(onPauseActivity != null) { onPauseActivity(); }
    }

    public event Action OnUnPauseActivity;
    public void UnPauseActivity()
    {
        if (onUnPauseActivity != null) { onUnPauseActivity(); }
    }
    public delegate void ResetLevel();
    public static ResetLevel resetLevel;
    public Action onUnPauseActivity;
}
