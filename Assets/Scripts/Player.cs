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
    public Transform weaponMount;
    public Transform weaponPivot;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Camera mainCam;

    public GameObject currentBullet;
    
    void Start() {
        controller = GetComponent<CharacterController>();
        mainCam = GGJ.Manager.mainCam;
        currentBullet = GGJ.Manager.bulletSimple;
    }

    void Update() {

        #region Look at Target
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weaponMount.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
            Bullet _bulletScript = _bullet.GetComponent<Bullet>();
            _bulletScript.rigidBody.AddForce(_bullet.transform.forward * _bulletScript.fireSpeed);
            GGJ.Manager.debug.text = (Vector3.forward * _bulletScript.fireSpeed).ToString();
        }
        #endregion

    }

}
