using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Interactable
{
    [SerializeField]
    private GameObject player;


    protected override void Interact() {

        // Equip the weapon to the player
        if (player != null)
        {
            transform.parent = player.transform;
            transform.localPosition = new Vector3(0.45f, -0.2f, 1f); 
            transform.localRotation = Quaternion.Euler(-45f, 0f, 0f);
        }
    }
}