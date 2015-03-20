using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SplashScreen : MonoBehaviour {
	public Image splashScreen;
	float timer = 1.5f;
	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0f) {
			PlayGame ();
		}
		if (timer <= 1f) {
			float alpha = Mathf.Lerp (0, 1, timer);
			splashScreen.color = new Color (1, 1, 1, alpha);
		}
		#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
		if (Input.GetMouseButtonDown (0)) {
			PlayGame();
		}
		#else
		if(Input.touchCount > 0) {
			Touch myTouch = Input.touches[0];
			if(myTouch.phase == TouchPhase.Began) {
				PlayGame();
			}
		}
		#endif
	}

	void PlayGame() {
		Application.LoadLevel ("GameScene");
	}
}
