using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.Player);	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 newPosition = new Vector3(
            player.transform.position.x,
            this.transform.position.y,
            player.transform.position.z
            );
        this.transform.position = newPosition;
	}
}
