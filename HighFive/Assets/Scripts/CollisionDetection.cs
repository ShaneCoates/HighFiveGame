using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {
	bool dissapearing = false;
	Color colour = new Color(1, 1, 1);
	Vector3 direction;
	// Use this for initialization
	void Start () 
	{
		direction =  this.transform.position;
		direction.Normalize ();
		direction *= 3;
		direction.z = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.position += direction * Time.deltaTime;
		if (dissapearing) {
			colour.a = colour.a - Time.deltaTime;
			
			if(colour.a <= 0f)
				OnBecameInvisible();
		}

		this.renderer.material.color = colour;
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButton (0)) {
			dissapearing = true;

		}
	}

	void OnBecameInvisible()
	{
		GameObject.Destroy(gameObject);
	}


}
