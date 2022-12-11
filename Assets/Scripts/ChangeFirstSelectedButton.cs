using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeFirstSelectedButton : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeButton(GameObject button)
    {
        if (Input.GetJoystickNames().Length != 0){
        if (Input.GetJoystickNames()[0] != "")
        {
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(button, new BaseEventData(eventSystem));
        }
        }

    }
}
