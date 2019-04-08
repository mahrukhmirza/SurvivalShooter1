using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// mahrukh sameen mirza 
/// april 7, 2019
/// this is a script for score manager 
/// </summary>

public class ScoreManager : MonoBehaviour
{
    public static int score; // the players score 


    Text text; // reference to the text component 


    void Awake ()
    {   // set up the reference 
        text = GetComponent <Text> ();
        // reset the score 
        score = 0;
    }


    void Update ()
    {   //set the displayed text to the word "Score" followed by score value
        text.text = "Score: " + score;
    }
}
