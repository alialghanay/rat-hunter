using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 6f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUi playerUI;
    private InputManger inputManger;
    private Interactable lastInteractable;
    private Interactable currentStepInteractable;

    private Material originalMaterial;
    private Color outlineColor = Color.red;

    private float textTimer;

    private float x;
    private float z;

    
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUi>();
        inputManger = GetComponent<InputManger>();
        playerUI.UpdateText(string.Empty);
    }

    void Update()
    {
        x = transform.position.x;
        z = transform.position.z;

        textTimer += Time.deltaTime;
        if(textTimer >= 2f) {
            playerUI.UpdateText(string.Empty);
            textTimer = 0;
        }
        
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                playerUI.UpdateText(interactable.promptMessage);

                if (lastInteractable != interactable)
                {
                    ResetOutline();
                    lastInteractable = interactable;
                    originalMaterial = hitInfo.collider.GetComponent<Renderer>().material;
                    hitInfo.collider.GetComponent<Renderer>().material.SetColor("_OutlineColor", outlineColor);
                }

                if (inputManger.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }

            }
            else
            {
                ResetOutline();
            }
        }
        else
        {
            ResetOutline();
        }
    }

    private void ResetOutline()
    {
        if (lastInteractable != null)
        {
            lastInteractable.GetComponent<Renderer>().material = originalMaterial;
            lastInteractable = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            currentStepInteractable = interactable;
            interactable.BaseInteract();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentStepInteractable != null && other.GetComponent<Interactable>() == currentStepInteractable)
        {
            currentStepInteractable = null;
        }
    }
}
