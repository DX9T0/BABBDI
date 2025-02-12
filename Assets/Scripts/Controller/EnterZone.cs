using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterZone : MonoBehaviour
{

    private float indoortimer;
    private float outdoortimer;

    [SerializeField] private AudioClip hitTrainClip;

    [HideInInspector] public bool inLift;

    public AudioClip endGameClip;
    
    void Start()
    {
        //SoundManager.Instance._outdoorSource.Play();
            outdoortimer = 4;
            indoortimer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        indoortimer -= Time.deltaTime;
        outdoortimer -= Time.deltaTime;

        if(indoortimer > 0) SoundManager.Instance.IndoorBlend();
        //if (indoortimer > 3) SoundManager.Instance._indoorSource.Stop();
        if(outdoortimer > 0) SoundManager.Instance.OutdoorBlend();
        //if (outdoortimer > 3) SoundManager.Instance._outdoorSource.Stop();
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Door/Indoor")
        {
            //SoundManager.Instance._indoorSource.Play();
            indoortimer = 4;
            outdoortimer = 0;
        }
        if (collisionInfo.tag == "Door/Outdoor")
        {
            //SoundManager.Instance._outdoorSource.Play();
            outdoortimer = 4;
            indoortimer = 0;
        }
        if (collisionInfo.tag == "Subway/In")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().inSubway = true;
        }
        if (collisionInfo.tag == "Subway/Out")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().inSubway = false;
        }
        if (collisionInfo.tag == "Club/In")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().inClub = true;
        }
        if (collisionInfo.tag == "Club/Out")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().inClub = false;
        }
        if (collisionInfo.tag == "Achievement/wayClimber")
        {
            if (!GameObject.Find("GameManager").GetComponent<GameManager>().wayClimber) GameObject.Find("GameManager").GetComponent<GameManager>().Popup();
            GameObject.Find("GameManager").GetComponent<GameManager>().wayClimber = true;
            GameObject.Find("GameManager").GetComponent<GameManager>().wayClimberState = 1;
            GameObject.Find("GameManager").GetComponent<GameManager>().lastAchievement = "Way of the climber";
            SteamIntegration.Instance.UnlockAchivement("WayOfTheClimber");
        }
        if (collisionInfo.tag == "Train")
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().gameTime < 240 && !GameObject.Find("GameManager").GetComponent<GameManager>().gameUnder)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().Popup();
                GameObject.Find("GameManager").GetComponent<GameManager>().gameUnder = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().gameUnderState = 1;
                GameObject.Find("GameManager").GetComponent<GameManager>().lastAchievement = "Way of the rusher";
                Debug.Log("warusher");
                SteamIntegration.Instance.UnlockAchivement("WayOfTheRusher");
            }
            SoundManager.Instance.PlayMusicShot(endGameClip);
            GameObject.Find("GameManager").GetComponent<GameManager>().endGame = true;
        }
        if (collisionInfo.tag == "KillZ")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().ReturnHome();
        }
        if (collisionInfo.tag == "MusicZone" && collisionInfo.GetComponent<MusicZone>().timer < 0)
        {
            SoundManager.Instance.PlayMusicShot(collisionInfo.GetComponent<MusicZone>().music);
            collisionInfo.GetComponent<MusicZone>().timer = collisionInfo.GetComponent<MusicZone>().countown;
        }
        if (collisionInfo.tag == "MusicZonePartTwo" && GameObject.Find("GameManager").GetComponent<GameManager>().secondPart == 1 && collisionInfo.GetComponent<MusicZone>().timer < 0)
        {
            SoundManager.Instance.PlayMusicShot(collisionInfo.GetComponent<MusicZone>().music);
            collisionInfo.GetComponent<MusicZone>().timer = collisionInfo.GetComponent<MusicZone>().countown;
        }
        if (collisionInfo.tag == "MusicZonePartOne" && GameObject.Find("GameManager").GetComponent<GameManager>().secondPart == 0 && collisionInfo.GetComponent<MusicZone>().timer < 0)
        {
            SoundManager.Instance.PlayMusicShot(collisionInfo.GetComponent<MusicZone>().music);
            collisionInfo.GetComponent<MusicZone>().timer = collisionInfo.GetComponent<MusicZone>().countown;
        }
        if (collisionInfo.tag == "SavingZone")
        {
            GameObject.Find("GameManager").GetComponent<SaveLoadSystem>().Save();
            GameObject.Find("GameManager").GetComponent<GameManager>().savePopup = true;
        }
        if (collisionInfo.tag == "TrainDeath" && !GameObject.Find("GameManager").GetComponent<GameManager>().endGame)
        {
            if (!GameObject.Find("GameManager").GetComponent<GameManager>().trainDeath) GameObject.Find("GameManager").GetComponent<GameManager>().Popup();
            GameObject.Find("GameManager").GetComponent<GameManager>().trainDeath = true;
            GameObject.Find("GameManager").GetComponent<GameManager>().trainDeathState = 1;
            GameObject.Find("GameManager").GetComponent<GameManager>().lastAchievement = "Flat face";
            SteamIntegration.Instance.UnlockAchivement("FlatFace");
            transform.position = GameObject.Find("SpawnPoint").transform.position;
        }
        if (collisionInfo.tag == "TrainDeath" && !GameObject.Find("GameManager").GetComponent<GameManager>().endGame)
        {
            SoundManager.Instance.PlaySound(hitTrainClip);
            GameObject.Find("DeathAnim").GetComponent<DeathAnim>().Trigger();
            transform.position = GameObject.Find("SpawnPoint").transform.position;
        }
        if (collisionInfo.tag == "GirlZone")
        {
            inGirlZone = true;
        }

        
        
    }

    public bool inGirlZone;

    void OnTriggerStay(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Lift")
        {
            transform.SetParent(collisionInfo.GetComponent<SetLift>().lift.transform);
            inLift = true;
        }
        if (collisionInfo.tag == "Train")
        {
            transform.parent = collisionInfo.transform;
        }
        if (collisionInfo.tag == "GirlZone")
        {
            inGirlZone = true;
        }
    }

    void OnTriggerExit(Collider collisionInfo)
    {
        transform.parent = null;
        inLift = false; 
        inGirlZone = false;

    }
}
