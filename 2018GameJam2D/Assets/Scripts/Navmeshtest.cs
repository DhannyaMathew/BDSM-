using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navmeshtest : MonoBehaviour {
	public GameObject test;
	public Transform parent;
	// Use this for initialization
	void Start () {
		Instantiate (test, new Vector3 (0, -2, -1), Quaternion.identity, parent);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
