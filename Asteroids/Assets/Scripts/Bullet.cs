using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 2/20/24
// base bullet class

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected int speed = 8;
    [SerializeField]
    protected int damage;
    protected float timeToDestroy = 1.2f;

    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    protected IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the bullet hits an asteroid
        if (other.CompareTag("Asteroid"))
        {
            // pass the rotation and damage through to break the asteroid
            other.GetComponent<Asteroid>().AsteroidBreak(transform.rotation, damage);
            Destroy(gameObject);
        }
    }
}
