using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private Ctrl ctrl;
    public AudioClip cursor, drop, control, lineClear, gameOver;
    private AudioSource audioSource;
    private GameObject bgAudio;

    private bool isMute;
    
    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
        bgAudio = GameObject.Find("BgAudio");
        ctrl = GetComponent<Ctrl>();
    }
    
    
    public void PlayCursor()
    {
        PlayAudio(cursor);
    }
    public void PlayDrop()
    {
        PlayAudio(drop);
    }
    public void PlayControl()
    {
        PlayAudio(control);
    }
    public void PlayLineClear()
    {
        PlayAudio(lineClear);
    }
    public void PlayGameOver()
    {
        PlayAudio(gameOver);
    }
    private void PlayAudio(AudioClip clip)
    {
        if(isMute)  return;
        audioSource.clip = clip;
        audioSource.Play(); 
    }
    public void OnAudioButtonClick()
    {
        isMute = !isMute;
        ctrl.view.SetMuteActive(isMute);
        bgAudio.SetActive(false);

        if(isMute == false)
        {
            PlayCursor();
            bgAudio.SetActive(true);
        }
    }
    
}
