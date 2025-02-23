﻿using UnityEngine;
using System.Collections;

public class NonPassable : MonoBehaviour
{

    private GameObject theMenu;

    // Use this for initialization
    void Start()
    {
        theMenu = GameObject.FindGameObjectWithTag("MenuController");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other != null)
            if (other.GetComponentInParent<UnitController>() != null)
                if (other.GetComponentInParent<UnitController>().unitType.Equals("PlayerPlane"))
                {
                    other.GetComponentInParent<UnitController>().canDrop = false;
                    other.GetComponentInParent<UnitController>().ThisUnit._CanTransform = false;
                    theMenu.GetComponent<MenuController>().canOpen = true;
                }
    }

    //void OnTriggerStay(Collider other)
    //{
    //    if (other != null)
    //        if (other.GetComponentInParent<UnitController>() != null)
    //            if (other.GetComponentInParent<UnitController>().unitType.Equals("PlayerPlane"))
    //            {
    //                other.GetComponentInParent<UnitController>().ThisUnit._Life += 1;
    //            }
    //}

    void OnTriggerExit(Collider other)
    {
        if (other != null)
            if (other.GetComponentInParent<UnitController>() != null)
                if (other.GetComponentInParent<UnitController>().unitType.Equals("PlayerPlane"))
                {
                    other.GetComponentInParent<UnitController>().canDrop = true;
                    other.GetComponentInParent<UnitController>().ThisUnit._CanTransform = true;
                    theMenu.GetComponent<MenuController>().IsVisible = false;
                    theMenu.GetComponent<MenuController>().canOpen = false;
                }
    }
}
