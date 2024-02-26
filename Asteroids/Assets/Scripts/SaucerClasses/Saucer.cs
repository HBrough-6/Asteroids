using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 2/20/24
// Base Saucer Class

public abstract class Saucer : MonoBehaviour
{
    // how fast the saucer moves
    protected float speed = 3;
    // how often the saucer shoots
    protected float shootDelay;
    // bullet prefab reference
    public GameObject SaucerBullet;
    // how many points the Saucer is worth
    protected int points = 75;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        StartCoroutine(MoveSwapTimer());
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected void Move()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    protected IEnumerator MoveSwapTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            transform.rotation = Quaternion.Euler(GameManager.Instance.RandZRotationBased(transform.rotation, 10));
        }
    }

    protected virtual void Shoot()
    {

    }
}
