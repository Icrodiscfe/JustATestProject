using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log((int)(player.transform.position.x / 10) + " - - " + (int)(player.transform.position.z / 10));
	}
}
