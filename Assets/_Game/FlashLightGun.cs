using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightGun : MonoBehaviour{
    public LayerMask flashlightLayer;

    public GameObject obj;

    private void FixedUpdate(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, flashlightLayer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            obj.SetActive(true);
            obj.transform.position = hit.point;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            obj.SetActive(false);
        }
    }
}
