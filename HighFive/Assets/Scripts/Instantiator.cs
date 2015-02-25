using UnityEngine;
using System.Collections;

public class Instantiator : MonoBehaviour {
	public GameObject obj;
	float timer;
	Vector3 pos;
	// Use this for initialization
	void Start () {
		Instantiate (obj);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= 0.5f) {
			pos = new Vector3(Random.Range (-0.1f, 0.1f), Random.Range (-0.0f, 0.1f), 0);
			Instantiate (obj, pos, new Quaternion());
			timer = 0f;
		}

	}
}
