using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;

    protected override void Interact()
    {
        doorOpen = false;
        door.GetComponent<Animator>().SetBool("isOpen", doorOpen);
    }
}


