using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {
    public Texture[] pictureArray;
    private Texture picture; 
    private int delay = 0;
    private int count = 0;
	public Text text;
	float timer;
	int score;
	int enemyScore;
	// Use this for initialization
	void Awake () {
		score = GameManager.playerScore;
		enemyScore = GameManager.enemyScore;
        if (score > enemyScore) {
        	text.text =  "You win!";
        } else if (score < enemyScore) {
        	text.text =  "You lose!";
        } else {
        	text.text =  "Draw!";
        }
	}

	void OnGUI() {
        //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), picture);
        //
		//GUI.Label(new Rect(100, 100, 1000, 100), "Scores: " + score + " vs " + enemyScore);
		//if (score > enemyScore) {
		//	GUI.Label (new Rect (100, 120, 1000, 100), "You win!");
		//} else if (score < enemyScore) {
		//	GUI.Label (new Rect (100, 120, 1000, 100), "You lose!");
		//} else {
		//	GUI.Label (new Rect (100, 120, 1000, 100), "Draw!");
		//}
        //
		//GUI.Label (new Rect (100, 140, 1000, 100), "Tap to play again!");
        //delay++;     
        //if(delay % 20 == 0)
        //{
        //    count++;
        //    if(count == pictureArray.Length)
        //        count = 0;
        //    picture = pictureArray[count];           
        //    delay = 0;           
        //}

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
		//timer += Time.deltaTime * 2;
		//if (timer >= 3) {
			Color colour = text.color;
            colour.a = 1;
			text.color = colour;
		//}	
	}
	void Restart() {
		Application.LoadLevel ("SplashScene");
	}
}
