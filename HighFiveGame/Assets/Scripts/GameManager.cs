using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public Camera mainCamera;
	public GameObject obj;
	public static int playerScore = 0;
	public int playerConsecutive;
	public int playerMultiplier = 1;

	public static int enemyScore = 0;
	public int enemyConsecutive;
	public int enemyMultiplier = 1;

	public Text scoreText;
	float timeLeft = 20f;
	float timer = 0f;
	float count = 0;
	Vector3 pos;

	// Use this for initialization
	void Awake () {
		playerScore = 0;
		enemyScore = 0;
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {

		if (timeLeft <= 0f && !GameObject.Find("Person(Clone)")) {
			Application.LoadLevel ("GameOverScene");
		} else {
			timer -= Time.deltaTime;
			timeLeft -= Time.deltaTime;
		}

		if (GameObject.Find ("Person(Clone)")) {
			Debug.Log ("There is a person!");
		} else {
			Debug.Log ("There is no person!");
		}

		if (timer <= 0f && timeLeft > 0f) {
			pos = new Vector3(Random.Range (-5f, 5f), obj.transform.lossyScale.y * 0.5f , 30);
			while(pos.x < 1.5f && pos.x > -1.5f) {
				pos.x = Random.Range(-5f, 5f);
			}
			Instantiate (obj, pos, new Quaternion());
			timer = 1.0f - (count * 0.01f);
			count++;
		}
		if (playerConsecutive > 10) {
			playerConsecutive = 0;
			++playerMultiplier;
		}
		if (enemyConsecutive > 10) {
			enemyConsecutive = 0;
			++enemyMultiplier;
		}
		scoreText.text = "Score: " + playerScore + " - Multiplier: " + playerMultiplier;
	}
}
