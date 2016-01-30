using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public Rigidbody rigidBody;
    public float fireSpeed = 0f;
    public float lifeTime = 3f;

    void Start() {
        StartCoroutine(DestroyOnLifeTime(lifeTime));
    }

    IEnumerator DestroyOnLifeTime( float _time) {
        yield return new WaitForSeconds(_time);
        DestroyImmediate(gameObject);
    }
}
