using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController controller;
    private Vector3 playerVelocity;
    [SerializeField]

    public float speed = 5f;

    [SerializeField]
    private float runSpeed = 4f;
    private bool isGrounded;
    [SerializeField]

    public float gravity = -9.8f;
    [SerializeField]

    public float jumbHeight = 3f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
      isGrounded = controller.isGrounded;   
    }

    public void ProcessMove(Vector2 input) {
        Vector3 moveDirction = Vector3.zero;
        moveDirction.x = input.x;
        moveDirction.z = input.y;
        controller.Move(transform.TransformDirection(moveDirction) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void ProcessRun(Vector2 input) {
        Vector3 moveDirction = Vector3.zero;
        moveDirction.x = input.x;
        moveDirction.z = input.y;
        controller.Move(transform.TransformDirection(moveDirction) * speed * runSpeed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jumb() {
        if(isGrounded)
        playerVelocity.y = Mathf.Sqrt(jumbHeight * -3.0f * gravity);
    }
}
