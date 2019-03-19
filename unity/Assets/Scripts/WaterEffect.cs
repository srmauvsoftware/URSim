using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEffect : MonoBehaviour {

	public float fps=30.0f;
	public Texture2D[] frames;

	private int frameIndex;
	private Projector projector;

	void Start(){
		projector = GetComponent<Projector> ();
		NextFrame();
		InvokeRepeating ("NextFrame", 1 / fps, 1 / fps);
	}

	void NextFrame(){
		projector.material.SetTexture ("_ShadowTex", frames [frameIndex]);
		frameIndex = (frameIndex + 1) % frames.Length;
	}

}
