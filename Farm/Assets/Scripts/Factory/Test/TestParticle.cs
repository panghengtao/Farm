using UnityEngine;
using System.Collections;

public class TestParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        particleSystem.renderer.sortingLayerID = 11;
        particleSystem.renderer.sortingOrder = 3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
