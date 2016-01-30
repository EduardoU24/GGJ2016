using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public float moveSpeed = 10f;
    public float jumpSpeed = 4f;
    public float gravity = 20f;
    public int maxJumps = 2;
    public int currentJumps = 0;
    public Vector3 moveDirection = Vector3.zero;
    public CharacterController controller;
    public Transform weaponMount;
    public Transform weaponPivot;

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        if (controller.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            currentJumps = 0;
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
        }
        if (Input.GetButtonDown("Jump")) {
            if (currentJumps < maxJumps) {
                currentJumps++;
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

}
