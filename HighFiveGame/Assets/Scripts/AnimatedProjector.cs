using UnityEngine;
using System.Collections;
public class AnimatedProjector : MonoBehaviour { 
	public float fps = 30.0f; 
	public Texture2D[] frames;
	public int speed = 10;
	private int frameIndex;
	private Projector projector;

	void Start() {
		projector = GetComponent<Projector>();
		NextFrame();
		InvokeRepeating("NextFrame", 1 / fps, speed / fps);
	}

	void NextFrame() {
		projector.material.SetTexture("_MainTex", frames[frameIndex]);
		frameIndex = (frameIndex + 1) % frames.Length;
		Debug.Log((frameIndex + 1) % frames.Length);
	}
}