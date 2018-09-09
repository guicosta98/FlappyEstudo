using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipe : MonoBehaviour {
    player player;
    public float speed = 2f;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("player").GetComponent<player>();
	}
	
	// Update is called once per frame
	void Update () {
        if(player.isAlive == true)
        {
            Vector3 pos = transform.position;
            pos.x = pos.x - speed * Time.deltaTime;
            transform.position = pos;

            if(transform.position.x < -5f)
            {
                GameObject.Destroy(gameObject);
            }
        }
        
	}
}
