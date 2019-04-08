using UnityEngine;
/// <summary>
/// mahrukh sameen mirza 
/// april 7, 2019
/// this is enemy health script 
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100; // the amount of health the enemy starts the game with 
    public int currentHealth; // the current health the enemy has 
    public float sinkSpeed = 2.5f; // the speed at which the enemy sinks through the floor when dead 
    public int scoreValue = 10; // the amount added to the players score when the enemy dies 
    public AudioClip deathClip; // the sound to play when the enemy dies


    Animator anim; // reference to the animator 
    AudioSource enemyAudio; // reference to the audio source 
    ParticleSystem hitParticles; // reference to the particle system that plays when the enemy is damaged 
    CapsuleCollider capsuleCollider; // reference to the capsule collider 
    bool isDead; // whether the enemy is dead 
    bool isSinking; // whether the enemy has started sinking through the floor 


    void Awake ()
    {   // setting up the references 
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        // setting the current health when the enemy first spawns 
        currentHealth = startingHealth;
    }


    void Update ()
    {   // if the enemy should be sinking ...
        if(isSinking)
        {   // ... move the enemy down by the sinkSpeed per second 
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {   // if he enemy is dead ...
        if(isDead) // ... no need to take damage so exit 
            return;
        // play the hurt sound effect 
        enemyAudio.Play ();
        // reduce the current health by the amount of damage sustained 
        currentHealth -= amount;
        // set the position of the particle system to where the hit was sustained 
        hitParticles.transform.position = hitPoint;
        hitParticles.Play(); // and play the particles 
        // if the current health is less than or equal to zero then enemy is dead
        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {   // enemy is dead 
        isDead = true;
        // turn the collider into a trigger so shots can pass through it 
        capsuleCollider.isTrigger = true;
        // tell the animator that the enemy is dead 
        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {   // find and disable the Nav Mesh Agent 
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        // find the rigidbody component and make it kinematic
        GetComponent <Rigidbody> ().isKinematic = true;
        // the enemy should no sink 
        isSinking = true;
        // increase the score by the enemys score value 
        ScoreManager.score += scoreValue;
        // after 2 seconds destroy the enemy 
        Destroy (gameObject, 2f);
    }
}
