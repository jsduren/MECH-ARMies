﻿
using UnityEngine;

public class CameraController : MonoBehaviour
{
   
    public GameObject Player;
    private Vector3 offset;

	// Use this for initialization
	void Start ()
	{
        //offset = transform.position;
        offset = new Vector3(0,150,-100.0f);
	}
	
	// Update is called once per frame
    private void LateUpdate()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
        {
            transform.position = Player.transform.position + offset;
        }
    }

    //void changeview()
    //{
    //    player = GameObject.FindGameObjectsWithTag("Player");
    //}
    
}
