using UnityEngine;
/// <summary>
/// mahrukh sameen mirza 
/// april 7, 2017
/// this is player shooting script
/// </summary>
public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20; // damage inflicted by each bullet
    public float timeBetweenBullets = 0.15f; // damage between each shot 
    public float range = 100f; // distance the gun can fire 


    float timer; // timer to determine when to fire 
    Ray shootRay = new Ray(); // ray from gun end forwards 
    RaycastHit shootHit; // raycast hit to get infor about what was hit 
    int shootableMask; // layer mask 
    ParticleSystem gunParticles; // refernce to the particle system 
    LineRenderer gunLine; // reference to the line renderer 
    AudioSource gunAudio; // reference to the audio source 
    Light gunLight; // refrence to the light component 
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {   // create a layer mask for the shootable layer 
        shootableMask = LayerMask.GetMask ("Shootable");
        // set up the references 
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {   // add time since Update was last called to the timer 
        timer += Time.deltaTime;
        // if Fire1 is being pressed and its time to fire shoot the gun
		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }
        /* if the timer has exceeded the proportion of time BetweenBullets that 
         * the effects should be displayed for  */
        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {   // disable the effects 
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {   // disbale the line renderer and the light 
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {   // reset the timer 
        timer = 0f;
        // play the gun shot audioClip 
        gunAudio.Play ();
        // enable the light 
        gunLight.enabled = true;
        // stop the [articles from playing if they were, then start the particles
        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        // perform the raycast against gameobjects  and if it hits something 
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {   // try and find an EnemyHealth script on the gameobject hit 
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null) // if the emeny health component exist
            {   // the the enemy should take damage 
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            // set the second position of the line renderer to the point the raycast hit 
            gunLine.SetPosition (1, shootHit.point);
        }
        /* if the raycast didnt hit anything on the shootable layer set the second 
         * position of the line renderer to fullest extent of gun range */
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
