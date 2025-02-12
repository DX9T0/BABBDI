using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour
{
    private GameObject player;

    public AudioClip hold;
    public AudioClip hit;

    public bool trigger;
    private bool hitb;


    public bool touch;
    private bool wall;


    private float slopForce;
    
    


    [SerializeField] private float wallImpactForce = 25;
    [SerializeField] private float groundImpactForce = 18;

    void Awake(){
        player = GameObject.Find("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        slopForce = player.GetComponent<FirstPersonController>().slopeForce;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<InteractObject>().inHands && !player.GetComponent<FirstPersonController>().pause)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                touch = true;
            }


            if (Input.GetButtonUp("Fire1")) touch = false;

            if (player.GetComponent<FirstPersonController>().frontRay && !player.GetComponent<FirstPersonController>().characterController.isGrounded)
                wall = true;
            else if (player.GetComponent<FirstPersonController>().rotationX < 45 && player.GetComponent<FirstPersonController>().characterController.isGrounded)
                wall = true;
            else wall = false;
        }
        
    }


    void OnTriggerStay(Collider collisionInfo)
    {

        if (touch && collisionInfo.tag != "Player" && GetComponent<InteractObject>().inHands)
        {
            SoundManager.Instance.PlaySound(hit);

            touch = false;

            if (!player.GetComponent<FirstPersonController>().clubRay && player.GetComponent<FirstPersonController>().rotationX > -75 && !player.GetComponent<FirstPersonController>().characterController.isGrounded && !player.GetComponent<FirstPersonController>().rightRay && !player.GetComponent<FirstPersonController>().leftRay && player.GetComponent<FirstPersonController>().rotationX < 45 && !player.GetComponent<FirstPersonController>().downRay)
                player.GetComponent<FirstPersonController>().moveDirection.y = groundImpactForce / 1.5f;

            else if (wall && (player.GetComponent<FirstPersonController>().frontRay || player.GetComponent<FirstPersonController>().clubRay))
                player.GetComponent<FirstPersonController>().AddForce(new Vector3(-player.GetComponent<FirstPersonController>().playerCamera.transform.forward.x, 0, -player.GetComponent<FirstPersonController>().playerCamera.transform.forward.z), wallImpactForce);

            else if (!player.GetComponent<FirstPersonController>().backRay && !player.GetComponent<FirstPersonController>().rightRay && !player.GetComponent<FirstPersonController>().characterController.isGrounded)
                player.GetComponent<FirstPersonController>().AddForce(-player.GetComponent<FirstPersonController>().playerCamera.transform.forward, groundImpactForce);
            else if (player.GetComponent<FirstPersonController>().characterController.isGrounded && player.GetComponent<FirstPersonController>().downRay){
                player.GetComponent<FirstPersonController>().slopeForce = 0;
                player.GetComponent<FirstPersonController>().moveDirection.y = 0;
                player.GetComponent<FirstPersonController>().AddForce(-player.GetComponent<FirstPersonController>().playerCamera.transform.forward, groundImpactForce);
                

            }
                
            else{
                
                player.GetComponent<FirstPersonController>().AddForce(-player.GetComponent<FirstPersonController>().playerCamera.transform.forward, 0f);
            }
                
        }
        

    }

    void OnTriggerExit()
    {
        player.GetComponent<FirstPersonController>().slopeForce = slopForce;
    }

}
