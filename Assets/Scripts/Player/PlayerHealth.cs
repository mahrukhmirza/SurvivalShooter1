/// Mahrukh Sameen Mirza 
/// april 7,2019
///  this is the player health folder 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100; // the amount of health the player starts the game with
    public int currentHealth; // the current halth of the player 
    public Slider healthSlider; // reference to the UI's health bar
    public Image damageImage; // reference to an image to flash on the screen on being hurt 
    public AudioClip deathClip; // the audio clip to play when the player dies
    public float flashSpeed = 5f; // the speed the damageImage will fade at
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f); // the color of the damage image is set to


    Animator anim; // reference to the animator component 
    AudioSource playerAudio; // reference to the ausio source component 
    PlayerMovement playerMovement; // reference to the players movement 
    PlayerShooting playerShooting; // reference to the playerShooting script 
    bool isDead; // whether the player is dead
    bool damaged; // true when the player gets damaged 


    void Awake ()
    {   // setting up the references 
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        // set the initial hea;th of the player 
        currentHealth = startingHealth;
    }


    void Update ()
    {   // if the player has just been damaged 
        if(damaged)
        {   // set te colour of the damageImage to the flash color 
            damageImage.color = flashColour;
        }
        // otherwise 
        else
        {   // transition the color back to clear 
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        // reset the damaged flag 
        damaged = false;
    }


    public void TakeDamage (int amount)
    {   // set the damaged flag so the screen will flash 
        damaged = true;
        // reduce the health by the damage amount 
        currentHealth -= amount;
        // set the health bars value to the current value 
        healthSlider.value = currentHealth;
        // play the hurt sound effect 
        playerAudio.Play ();
        // if the player has lost all its health and the death flag hasnt been set yet 
        if(currentHealth <= 0 && !isDead)
        {   // it should die 
            Death ();
        }
    }


    void Death ()
    {   // set the death flag to this function wont be called again
        isDead = true;
        // turn off any remaining shooting effects 
        playerShooting.DisableEffects ();
        // tell the animator that the player is dead 
        anim.SetTrigger ("Die");
        // set the audio source to play death clip 
        playerAudio.clip = deathClip;
        playerAudio.Play ();
        // turn off the movement and shooting scripts 
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

}
