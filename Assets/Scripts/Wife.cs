using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wife : MonoBehaviour
{
    [SerializeField] private GameObject corpseParticles;
    [SerializeField] private GameObject corpseParticles2;
    [SerializeField] private AudioSource flies;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.secondPart == 1)
        {
            gameObject.layer = 0;
            if (corpseParticles != null) corpseParticles.SetActive(true);
            if (corpseParticles2 != null) corpseParticles2.SetActive(true);
            flies.mute = false;
        }
        else    {
            if (corpseParticles != null) corpseParticles.SetActive(false);
            if (corpseParticles2 != null) corpseParticles2.SetActive(false);
            flies.mute = true;
        }
            
    }
}
