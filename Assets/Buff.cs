using UnityEngine;
using System.Collections;

public class Buff : MonoBehaviour {

    public GameObject bullet;
    public bool giveBullet;

    void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            if(giveBullet)
                col.GetComponent<Player>().currentBullet = bullet.gameObject;

            Debug.Log("Add Buff" + bullet.name);
        }

    }
}
