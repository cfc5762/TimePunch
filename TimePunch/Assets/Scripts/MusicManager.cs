using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.SoundManagerNamespace;

public class MusicManager : MonoBehaviour
{
    public AudioSource[] MusicAudioSources;
    public Toggle PersistToggle;


    private void PlayMusic(int index)
    {
        MusicAudioSources[index].PlayLoopingMusicManaged(1.0f, 1.0f, PersistToggle.isOn);
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
