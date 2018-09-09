using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeCreator : MonoBehaviour {

    player player;
    public GameObject pipeBase;
    float clock = 10f;
    public float spawnTime = 4f;
    public float range = 2f;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("player").GetComponent<player>();
	}
	
	// Update is called once per frame
	void Update () {
        if(player.isAlive == true && player.isGameStarted == true)
        {
            clock += Time.deltaTime;
            if(clock >= spawnTime)
            {
                clock = 0;
                Vector3 pos = transform.position;
                pos.y = pos.y - Random.Range(-range, range);
                GameObject.Instantiate(pipeBase, pos, Quaternion.identity);
            }
        }
        
	}
}
