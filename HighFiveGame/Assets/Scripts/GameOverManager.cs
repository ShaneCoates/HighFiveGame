using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {
	int score;
	int enemyScore;
	// Use this for initialization
	void Awake () {
		score = GameManager.playerScore;
		enemyScore = GameManager.playerScore;
		Debug.Log (score + " " + enemyScore);
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
		if (Input.GetMouseButtonDown (0)) {
			Restart();
		}
		#else
		if(Input.touchCount > 0) {
			Touch myTouch = Input.touches[0];
			if(myTouch.phase == TouchPhase.Began) {
				Restart();
			}
		}
		#endif
	}
	void Restart() {
		Application.LoadLevel ("SplashScene");
	}
}
