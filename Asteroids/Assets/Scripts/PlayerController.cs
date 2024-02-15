using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // input actions reference
    InputActions inputActions;

    /// <summary>
    /// How fast the player moves forwards
    /// </summary>
    private float speed = 4;
    
    /// <summary>
    /// how fast the player rotates
    /// </summary>
    private float rotationSpeed = 10;

    /// <summary>
    /// Tells if the player can shoot or not
    /// </summary>
    private bool canShoot = true;

    /// <summary>
    /// How long you have to wait before shooting again
    /// </summary>
    private float shootDelay = 0.5f;

    /// <summary>
    /// How long the ship is not on screen before it comes back
    /// </summary>
    private float warpAwayDelay = 0.3f;

    /// <summary>
    /// How long the player has to wait before warping again
    /// </summary>
    private float warpCooldown = 0.5f;

    /// <summary>
    /// Tells if the player can warp
    /// </summary>
    private bool canWarp = true;

    /// <summary>
    /// Reference to the bullet prefab the player will shoot out
    /// </summary>
    [SerializeField]
    private GameObject bulletRef;


    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }

    // shoots a bullet out the front of the ship
    public void Shoot()
    {
        // Instantiate(bulletRef, transform.position, transform.rotation);
    }

    /// <summary>
    /// pushes the players in the direction they are facing
    /// </summary>
    public void Thrust()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * Time.deltaTime * speed);
    }

    // rotates the player left
    public void RotateLeft(InputAction.CallbackContext context)
    {
        transform.Rotate(new Vector3(0, 0, -rotationSpeed));
        // for rotation calling the function with context.started will set the rotation
        // to the left and then the rotation should be changed in update/fixed update
        // try to do the same for movement
    }
    // rotates the player right
    public void RotateRight()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    // places the player in a random spot on screen after a short delay
    public void Warp()
    {
        if (canWarp)
        {
            // place the player off screen
            transform.position = new Vector3(-45, 45, 0);
            // waits for warpCooldown Seconds and then sets canWarp to true
            StartCoroutine(WarpDelay());
        }
    }

    private IEnumerator WarpDelay()
    {
        // stop the player from warping again
        canWarp = false;
        // wait for the time that the player will be off screen
        yield return new WaitForSeconds(warpAwayDelay);

        // generate random coordinates
        Vector2 randomCoords = GameManager.Instance.CreateRandomCoordinates();
        // places the player after the warpAwayDelay is over
        transform.position = new Vector3(randomCoords.x, randomCoords.y, 0);

        // wait for the cooldown
        yield return new WaitForSeconds(warpAwayDelay);
        canWarp = true;
    }


}
