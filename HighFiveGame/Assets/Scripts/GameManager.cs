﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public Camera mainCamera;
	public GameObject obj;
	public static int playerScore = 0;
	public int playerConsecutive;
    public bool playerMultiplierCheck;
	public int playerMultiplier = 1;

	public static int enemyScore = 0;
	public int enemyConsecutive;
	public int enemyMultiplier = 1;

	float timeLeft = 20f;
	float timer = 0f;
	float count = 0;
	Vector3 pos;

	float alphaFadeValue = 0.99f;
	bool fadingOut = false;
	public static Texture2D Fade;

    float sunflowerTimer = 0f;

	// Use this for initialization
	void Awake () {
		playerScore = 0;
		enemyScore = 0;
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		if (Fade == null) {
			Fade = new Texture2D (1, 1);
			Fade.SetPixel (0, 0, new Color (1, 1, 1, 1));
		}
        Screen.orientation = ScreenOrientation.Landscape;
        SoundManager.Instance.StartGame();
	}
	
	// Update is called once per frame
	void Update () {

		if (timeLeft <= 0f && !GameObject.Find("Person(Clone)")) {
			fadingOut = true;
		} else {
			timer -= Time.deltaTime;
			timeLeft -= Time.deltaTime;
		}

        if (sunflowerTimer <= 0){
            sunflowerTimer = Random.Range(1.0f, 3.0f);
            SoundManager.Instance.SunFlower();
        } else {
            sunflowerTimer -= Time.deltaTime;
        }

		if (alphaFadeValue >= 1) {
            SoundManager.Instance.Stop();
			Application.LoadLevel("GameOverScene");
		}

		if (timer <= 0f && timeLeft > 0f) {
            pos = new Vector3(Random.Range(2.5f, 6f), 0, 30);
            
            if(Random.Range(0, 2) == 1)
            {
                pos.x *= -1;
            }
            
            
			Instantiate (obj, pos, new Quaternion());
			timer = 0.8f - (count * 0.01f);
			count++;
		}
        playerMulti();
	}
	void OnGUI ()
	{
		if (fadingOut) {
			alphaFadeValue += Time.deltaTime;
			if (alphaFadeValue != 0) {
				GUI.color = new Color (1, 1, 1, alphaFadeValue);
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Fade);
			}
		} else if (alphaFadeValue > 0) {
			alphaFadeValue -= Time.deltaTime;
			GUI.color = new Color (1, 1, 1, alphaFadeValue);
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Fade);
		}
	}

    public void playerMulti()
    {
        if (playerConsecutive > 10)
        {
            playerConsecutive = 0;
            ++playerMultiplier;
        }
    }

    public void playerReset()
    {
        playerMultiplier = 1;
        playerConsecutive = 0;
    }

    public void enemyMulti()
    {
        if (enemyConsecutive > 10)
        {
            enemyConsecutive = 0;
            ++enemyMultiplier;
        }
    }

    public void enemyReset()
    {
        enemyMultiplier = 1;
        enemyConsecutive = 0;
    }

}
