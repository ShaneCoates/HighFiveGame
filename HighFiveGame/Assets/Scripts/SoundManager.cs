using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	// Use this for initialization
	private static SoundManager instance = null;
    public AudioSource backgroundSource;
    public AudioSource foregroundSource;
    public AudioClip[] slaps;
    public AudioClip[] sunflowers;
    public AudioClip ambience;
    public AudioClip running;
    public AudioClip start;
    public static SoundManager Instance {
       get { return instance; }
    }
    void Awake() {
    if (instance != null && instance != this) {
        Destroy(this.gameObject);
        return;
    } else {
        instance = this;
    }
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Slap() {
        foregroundSource.PlayOneShot(slaps[Random.Range(0, slaps.Length - 1)]);
    }
    public void SunFlower() {
        foregroundSource.PlayOneShot(sunflowers[Random.Range(0, sunflowers.Length - 1)]);
    }
    public void StartGame() {
        foregroundSource.PlayOneShot(start);
        foregroundSource.PlayOneShot(ambience);
        foregroundSource.PlayOneShot(running);
    }
    public void Stop(){
        foregroundSource.Stop();
    }
    
}
