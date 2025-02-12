using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private float trainSpeed;
    [SerializeField] private Vector3 direction;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("Player").GetComponent<FirstPersonController>().pause) transform.position += Vector3.right * trainSpeed * Time.deltaTime;

        if ((trainSpeed < 0 ? transform.position.x < endPos.position.x : transform.position.x > endPos.position.x) && gameManager.requestTrain == 0) transform.position = new Vector3(startPos.position.x, transform.position.y, transform.position.z);
    }
}
