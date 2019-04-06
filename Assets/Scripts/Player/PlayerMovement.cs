
/// <summary>
/// MAHRUKH sAMEEN MIRZA
/// april 6, 2019
/// this file has code for player movement in our game 
/// </summary>
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f; // the speed of the player 
    Vector3 movement; // vector to store the direction of the players movement 
    Animator anim; //reference to the animator component
    Rigidbody playerRigidbody; // reference to the players rigidbody
    int floorMask; // a layer mask so that a ray can be cast just at gameobjects on the floor layer
    float camRayLength = 100f; // the length of the ray from the camera into the scene
    private void Awake()
    {   //create a layer mask for the floor layer
        floorMask = LayerMask.GetMask("Floor");
        //set up references
        anim = GetComponent<Animator>(); 
        playerRigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        // store the input axis
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // move the player around the scene
        Move(h, v);
        // turn the player to face the mouse cursor
        Turning();
        // Animate the player
        Animating(h, v);
    }
    void Move (float h,float v)
    {
        // set the movement vector based on the axis input 
        movement.Set(h, 0f, v);
        /* normalise the movement vector and make it 
         propotional to the speed per second */
        movement = movement.normalized * speed * Time.deltaTime;
        // move the player to its current position plus the movement 
        playerRigidbody.MovePosition(transform.position + movement);
    }
    void Turning()
    {
        // create a ray from the mouse cursor on screen in direction of camera
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        /* creaye a RaycastHit variable to stpre information about what was hit 
        by the ray */
        RaycastHit floorHit;
        // perform the raycast and if it hits something on the floor layer 
        if (Physics.Raycast(camRay,out floorHit,camRayLength, floorMask))
        {
            /* create a vector from the player to the point on the floor
            the raycast from the mouse hit */
            Vector3 playerToMouse = floorHit.point - transform.position;
            // ensure the vector is entirely along the floor plane 
            playerToMouse.y = 0f;
            /* create a quaternion (rotation) based on looking down the vector
              from the player to the mouse */
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            //set the players rotation to this new rotation 
            playerRigidbody.MoveRotation(newRotation);
        }
    }
    void Animating (float h, float v)
    {
        //create a boolean that is true if either of the input axis is non zero
        bool walking = h != 0f || v != 0f;
        //tell the animator whether or not the player is walking 
        anim.SetBool("IsWalking", walking);
    }
}
