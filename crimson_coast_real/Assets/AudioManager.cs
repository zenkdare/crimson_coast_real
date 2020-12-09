using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public AudioSource portAmbience;
	public AudioSource oceanAmbience;
	public AudioSource tavernSong;
	public AudioSource sailingSong1;
	public AudioSource sailingSong2;
	private bool sailing = false;
	private int songNum = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (sailing && songNum == 1 && !sailingSong1.isPlaying){
        	sailingSong2.Play();
        	songNum = 2;
        }
        if (sailing && songNum == 2 && !sailingSong2.isPlaying){
        	sailingSong1.Play();
        	songNum = 1;
        }
    }

    public void portSound(bool active){
    	if (active){
    		portAmbience.Play();
    		tavernSong.Play();
    		oceanSound(false);
    	}
    	else{
    		portAmbience.Stop();
    		tavernSong.Stop();
    	}
    }

    public void oceanSound(bool active){
    	if (active){
    		oceanAmbience.Play();
    		sailingSong(true);
    		portSound(false);
    	}
    	else{
    		oceanAmbience.Stop();
    		sailingSong(false);
    	}
    }

    public void sailingSong(bool active){
    	if (active){
    		sailing = true;
    		songNum = Random.Range(1,3);
    		if (songNum == 1){
    			sailingSong1.Play();
    		}
    		else{
    			sailingSong2.Play();
    		}
    	}
    	else{
    		sailing = false;
    		sailingSong1.Stop();
    		sailingSong2.Stop();
    	}
    }
}
