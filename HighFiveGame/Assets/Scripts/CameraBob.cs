using UnityEngine;
using System.Collections;

public class CameraBob : MonoBehaviour {

	private float timer = 0.0f;
	float bobbingSpeed = 0.18f;
	float bobbingAmount = 0.1f;
	float midpoint = 2.0f;
	
	void Update () {
		float waveslice = 0.0f;
		
		Vector3 cSharpConversion = transform.localPosition; 
		waveslice = Mathf.Sin(timer);
		timer = timer + bobbingSpeed;
		if (timer > Mathf.PI * 2) {
			timer = timer - (Mathf.PI * 2);
		}
		if (waveslice != 0) {
			float translateChange = waveslice * bobbingAmount;
			cSharpConversion.y = midpoint + translateChange;
		}
		else {
			cSharpConversion.y = midpoint;
		}
		
		transform.localPosition = cSharpConversion;
	}
}
