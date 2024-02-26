using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Brough, Heath
// 2/20/24
// controls the movement and shooting of the player

public class PlayerController : Singleton<PlayerController>
{
    // input actions reference
    InputActions inputActions;

    public bool isInvincible = false;

    /// <summary>
    /// How fast the player moves forwards
    /// </summary>
    private float speed = 4;
    
    /// <summary>
    /// how fast the player rotates
    /// </summary>
    private float rotationSpeed = 5;

    /// <summary>
    /// Tells if the player can shoot or not
    /// </summary>
    private bool canShoot = true;

    /// <summary>
    /// How long you have to wait before shooting again
    /// </summary>
    private float shootDelay = 0.4f;

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

    private float currentMove = 0;
    private Vector3 currentRotate = Vector3.zero;
    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }

    private void Start()
    {
        StartCoroutine(GoInvincible());
    }

    // shoots a bullet out the front of the ship
    private void OnShoot()
    {
        if (canShoot)
        {
            Instantiate(bulletRef, transform.GetChild(0).transform.position, transform.rotation);
            StartCoroutine(ShootDelay());
        }
    }

    private IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }


    private void FixedUpdate()
    {
        //gameObject.GetComponent<Rigidbody>().AddForce(currentMove, ForceMode.Force);
        transform.Translate(Vector3.up * speed * Time.deltaTime * currentMove);
        transform.Rotate(currentRotate);
    }

    // places the player in a random spot on screen after a short delay
    private void OnWarp()
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
        yield return new WaitForSeconds(warpCooldown - warpAwayDelay);
        canWarp = true;
    }

    private void OnRotate(InputValue input)
    {
        currentRotate =new Vector3(0, 0, rotationSpeed * -input.Get<Vector2>().x);
    }

    private void OnThrust(InputValue input)
    {
        currentMove = input.Get<Vector3>().y;
    }

    // makes the player invincible
    public IEnumerator GoInvincible()
    {   
        // make player invincible
        isInvincible = true;

        // iterates 5 times for a total of 2.5 seconds
        for (int duration = 0; duration < 5; duration++)
        {
            // disables the mesh
            gameObject.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.2f);
            // enables the mesh
            gameObject.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.3f);

        }
        // make sure the mesh is enabled
        gameObject.GetComponent<Renderer>().enabled = true;
        // player is no longer invincible
        isInvincible = false;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // if a player hits an asteroid, enemy, or bullet
        if ((other.CompareTag("Asteroid") || other.CompareTag("Saucer") || other.CompareTag("EnemyBullet")) && !isInvincible) ;
        {
            // take damage
            PlayerData.Instance.GetComponent<PlayerData>().TakeDamage();
        }
    }
}
