using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStartMusic : MonoBehaviour
{
    public BackgroundMusicPlayer bmp;
    public AudioClip musicAudioClip;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Check", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Check()
    {
        if (Services.Get<DataManager>().PlayerData.isFinishedNormalEnd)
        {
            //bmp.musicAudioClip = musicAudioClip;
            bmp.ChangeMusic(musicAudioClip);
            bmp.Play();
        }
        else
        {
            bmp.Play();
        }
    }
}
