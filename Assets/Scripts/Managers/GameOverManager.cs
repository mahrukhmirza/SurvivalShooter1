using UnityEngine;
/// <summary>
/// mahrukh sameen mirza 
/// april 8,2019
/// game over manager script 
/// </summary>
public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth; // reference to the players health 
    public float restartDelay; // time to wait before restarting the level
     Animator anim; // reference to the animator 
    float restartTimer; // Timer to count up to restarting level 


    void Awake()
    {   // set up the references 
        anim = GetComponent<Animator>();
    }


    void Update()
    {   // if the player has ran out out of health tell the game is over
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
            // ... increment a timer to count up to restarting 
            restartTimer += Time.deltaTime;
            //... if it reaches the restart delay ...
            if (restartTimer >= restartDelay)
            {
                //... then reload the currently loaded level 
                Application.LoadLevel(Application.loadedLevel);

            }
        }
    }
}
