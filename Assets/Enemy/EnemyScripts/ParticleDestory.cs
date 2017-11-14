using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestory : MonoBehaviour {

    public ParticleSystem _particle;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(_particle != null && _particle.particleCount == 0)
        {
            ParticleDestory.Destroy(this.gameObject);
        }
	}
}
