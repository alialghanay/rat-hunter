using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    private float xSenitivity = 100f;
    private float ySenitivity = 100f;

    public void ProcessLook(Vector2 input) {
        float mouseX = input.x;
        float mouseY = input.y;
        xRotation -= (mouseY * Time.deltaTime) * ySenitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0 , 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSenitivity);
    }
}
