using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SplashScreen : MonoBehaviour {
	public Image splashScreen;
	public Text splashText;
	float timer;
	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
		timer += Time.deltaTime * 2;
		if (timer >= 3) {
			Color colour = splashText.color;
			colour.a = Mathf.Cos(timer);
			splashText.color = colour;
		}
	}

	void PlayGame() {
		Application.LoadLevel ("GameScene");
	}
}
