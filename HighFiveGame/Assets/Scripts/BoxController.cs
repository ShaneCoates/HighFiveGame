using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoxController : MonoBehaviour {
	public int score;
	public GameObject hand;
	public ParticleSystem particles;
	bool inRange = false;
	bool dissapearing = false;
	Color colour = new Color(1, 1, 1, 0);
	Color handColour = new Color (1, 1, 1, 0);
	Vector3 direction = new Vector3 (0, 0, -7);

	Ray ray;
	RaycastHit hit;
	// Use this for initialization
	void Awake () {
		this.GetComponent<Renderer>().material.color = colour;
		hand.GetComponent<Renderer>().material.color = handColour;

		Vector3 handPos = this.transform.position;
		if (this.transform.position.x < 0)
			handPos.x += Random.Range(0.5f, 2f);
		else 
			handPos.x -= 1.5f;
		handPos.y += Random.Range (0, 2f);
		handPos.z -= 0.01f;
		hand.transform.position = handPos;
		particles.emissionRate = 0;
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.position += direction * Time.deltaTime;

		CheckIfInRange ();

		CheckCollision ();

		AdjustOpacity ();

		this.GetComponent<Renderer>().material.color = colour;
		hand.GetComponent<Renderer>().material.color = handColour;
	}

	void CheckIfInRange() {
		if (this.transform.position.z < 12 && this.transform.position.z > 5)
			inRange = true;
		else
			inRange = false;
		
		if (inRange)
			handColour.g = 0.5f;
		else if(!dissapearing)
			handColour.g = 1f;
	}

	void AdjustOpacity() {
		if (!dissapearing) {
			if (colour.a < 1.0f) {
				colour.a += Time.deltaTime;
				handColour.a += Time.deltaTime;
			}
		}
		else {
			handColour.a = handColour.a - Time.deltaTime;
			if(handColour.a <= 0f)
				Destroy();
		}
	}

	void CheckCollision() {
		Vector2 inputPos = new Vector2 (-1, -1);
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
		if(Input.GetMouseButtonDown (0))
			inputPos = Input.mousePosition;
#else
		if(Input.touchCount > 0)
		{
			Touch myTouch = Input.touches[0];
			if(myTouch.phase == TouchPhase.Began) 
				inputPos = myTouch.position;
		}
#endif
		ray = Camera.main.ScreenPointToRay (inputPos);
		if (Physics.Raycast (ray, out hit)) {
			if (hit.collider.gameObject == hand)
				if(inputPos.x != -1)
				Collide();
		}
	}

	void Collide() {
		if (inRange && !dissapearing) {
			dissapearing = true;
			++GameManager.instance.consecutive;
			//particles.Emit(1000);
		}
	}

	void Destroy() {
		if (dissapearing) {
			GameManager.instance.score += 1 * GameManager.instance.multiplier;
		}
		else
			GameManager.instance.multiplier = 1;
		
		GameObject.Destroy(gameObject);
	}

	void OnBecameInvisible() {
		if(!dissapearing)
			Destroy ();
	}
	
	
}
