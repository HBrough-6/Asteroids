using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 2/8/2024
// handles the screenwrapping of objects going out of bounds

public class Walls : MonoBehaviour
{
    // if true, the wall with this script attached is a side wall
    // if false, it is a top or bottom wall
    [SerializeField]
    private bool isLeftOrRight = true;

    int layermask = 1 << 2;
    // Start is called before the first frame update
    private void Awake()
    {
        // invert layermask to make sure it ignores layer 2
        layermask = ~layermask;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Wall"))
        {
            Debug.Log("Hit");
            switch (isLeftOrRight)
            {
                // is left or right
                case true:
                    // move the object to the other side of the screen, left or right
                    other.transform.position = new Vector3(-other.transform.position.x, other.transform.position.y, other.transform.position.z);
                    break;
                // is top or bottom
                default:
                    // move the object to the other side of the screen, top or bottom
                    other.transform.position = new Vector3(other.transform.position.x, -other.transform.position.y, other.transform.position.z);
                    break;
            }
        }
        
    }
}
