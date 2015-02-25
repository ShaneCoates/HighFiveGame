using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.renderer.material.color = new Color(1, 1, 1);

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
		{
			this.renderer.material.color = new Color(1, 0, 1);
		}

		if(Input.GetMouseButtonDown(0))
		{
			this.renderer.material.color = new Color(1, 0, 1);
		}
	}

}
