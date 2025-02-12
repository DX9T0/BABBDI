using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color tempColor;
    [SerializeField] private float fadeSpeed;

    public float timer = 0;


    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        tempColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
         if (timer < -4f)
        {
            tempColor.a -= Time.deltaTime * fadeSpeed * 6;
        image.color = tempColor;
        }
        else if (timer < -3f)
        {
            tempColor.a -= Time.deltaTime * fadeSpeed * 4;
        image.color = tempColor;
        }
        else if (timer < -2f)
        {
            tempColor.a -= Time.deltaTime * fadeSpeed * 2;
        image.color = tempColor;
        }
        else if (timer < 0f)
        {
            tempColor.a -= Time.deltaTime * fadeSpeed;
        image.color = tempColor;
        }
        
    }
}
