using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {
	public Text text;
	float timer;
	int score;
	int enemyScore;
	// Use this for initialization
	void Awake () {
		score = GameManager.playerScore;
		enemyScore = GameManager.enemyScore;
		Debug.Log (score + " " + enemyScore);
	}

	void OnGUI() {
		GUI.Label(new Rect(100, 100, 1000, 100), "Scores: " + score + " vs " + enemyScore);
		if (score > enemyScore) {
			GUI.Label (new Rect (100, 120, 1000, 100), "You win!");
		} else if (score < enemyScore) {
			GUI.Label (new Rect (100, 120, 1000, 100), "You lose!");
		} else {
			GUI.Label (new Rect (100, 120, 1000, 100), "Draw!");
		}

		GUI.Label (new Rect (100, 140, 1000, 100), "Tap to play again!");
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
		timer += Time.deltaTime * 2;
		if (timer >= 3) {
			Color colour = text.color;
			colour.a = Mathf.Cos(timer);
			text.color = colour;
		}	
	}
	void Restart() {
		Application.LoadLevel ("SplashScene");
	}
}
