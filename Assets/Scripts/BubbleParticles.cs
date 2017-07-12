using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleParticles : MonoBehaviour {

	ParticleSystem particles;

	// Use this for initialization
	void Start () {

		particles = GetComponent<ParticleSystem>();
		
	}
	
  void OnMouseEnter() {

  	particles.Play();

  }

  void OnMouseExit() {

  	particles.Stop();


  }

}
