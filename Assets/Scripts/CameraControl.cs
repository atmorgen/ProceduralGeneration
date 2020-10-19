﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    float mainSpeed = 100.0f; //regular speed
    float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    float maxShift = 1000.0f; //Maximum speed when holdin gshift
    private float totalRun = 1.0f;

    float minFov = 15f;
    float maxFov = 90f;
    float sensitivity = 10f;

    void Update() {

        //Mouse Scroll
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);

        if(fov > 30) { fov = 30; }
        Camera.main.fieldOfView = fov;

        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();

        if (Input.GetKey(KeyCode.LeftShift)) {
            totalRun += Time.deltaTime;
            p = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        } else {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
        }

        p = p * Time.deltaTime;
        Vector3 newPosition = transform.position;
        if (Input.GetKey(KeyCode.Space)) { //If player wants to move on X and Z axis only
            transform.Translate(p);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        } else {
            transform.Translate(p);
        }
        PreventOffScreen();
    }

    private void PreventOffScreen() {

        if (transform.position.z > 300) {
            transform.position = new Vector3(transform.position.x, transform.position.y, 300);
        }

        if ( transform.position.z < -300) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -300);
        }

        if (transform.position.x < -150) {
            transform.position = new Vector3(-150, transform.position.y, transform.position.z);
        }

        if( transform.position.x > 150) {
            transform.position = new Vector3(150, transform.position.y, transform.position.z);
        }
    }

    private Vector3 GetBaseInput() { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W)) {
            p_Velocity += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.S)) {
            p_Velocity += new Vector3(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.A)) {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D)) {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
