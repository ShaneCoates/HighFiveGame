﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public GameObject obj;
	public int score = 0;
	public int consecutive;
	public int multiplier = 1;
	public Text scoreText;
	float timer = 0f;
	int count = 0;
	Vector3 pos;
	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0f) {
			pos = new Vector3(Random.Range (-5f, 5f), 0, 30);
			while(pos.x < 1.5f && pos.x > -1.5f) {
				pos.x = Random.Range(-5f, 5f);
			}
			Instantiate (obj, pos, new Quaternion());
			timer = 1f - (count * 0.001f);
			count++;
		}
		if (consecutive > 10) {
			consecutive = 0;
			++multiplier;
		}

		scoreText.text = "Score: " + score + " - Multiplier: " + multiplier;
	}


}