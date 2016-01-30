using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : NetworkBehaviour {
    public float moveSpeed = 10f;
    public float jumpSpeed = 4f;
    public float gravity = 20f;
    public int maxJumps = 2;
    public int currentJumps = 0;
    public Transform weaponMount;
    public Transform weaponPivot;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Animator animator;

    public GameObject currentBullet;
    
    void Start() {
        controller = GetComponent<CharacterController>();
        currentBullet = GGJ.Manager.bulletSimple;
        animator = GetComponent<Animator>();
    }

    void Update() {
        if(!isLocalPlayer) {
            return;
        }

        #region Look at Target
        Vector3 dir = Input.mousePosition - GGJ.Manager.mainCam.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weaponMount.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        animator.SetFloat("DirectionX", dir.x);
        animator.SetFloat("DirectionY", dir.y);
        animator.SetFloat("Rotation", weaponMount.eulerAngles.z);

        // debug
        string _dir = "";
        if(dir.x > 0) _dir = "right"; else _dir = "left";        
        if(dir.y > 0) _dir += " - top"; else _dir += " - down";
        GGJ.Manager.debug.text = dir.ToString();
        GGJ.Manager.debug.text = GGJ.Manager.debug.text + "\n" + _dir;
        // - debug

        #endregion

        #region Movement
        moveDirection.x = new Vector3(Input.GetAxis("Horizontal"), 0, 0).x;
        moveDirection.x = transform.TransformDirection(moveDirection).x;
        moveDirection.x *= moveSpeed;

        if (controller.isGrounded) {
            currentJumps = 0;
        }
        if (Input.GetButtonDown("Jump")) {
            if (currentJumps < maxJumps) {
                currentJumps++;
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        #endregion

        #region Attacks
        if (Input.GetButtonDown("Fire1")) {
            GameObject _bullet = Instantiate(currentBullet, weaponPivot.position, weaponPivot.rotation) as GameObject;
            _bullet.transform.SetParent(GGJ.Manager.transform);
            Bullet _bulletScript = _bullet.GetComponent<Bullet>();
            _bulletScript.rigidBody.AddForce(_bullet.transform.forward * _bulletScript.fireSpeed);
        }
        #endregion

    }

}
