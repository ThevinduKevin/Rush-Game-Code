using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{
    public AudioSource footstepsSound;
   //public AudioSource sprintSound;

    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
            if (Input.GetKey(KeyCode.LeftShift))
            {
                footstepsSound.enabled = false;
            }
            else
            {
                footstepsSound.enabled = true;
            }
        }
        else
        {
            footstepsSound.enabled = false;
        }
    }
}