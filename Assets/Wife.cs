using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wife : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().secondPart == 1)
        {
            gameObject.layer = 0;
        }
    }
}
