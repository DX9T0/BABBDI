using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    // Start is called before the first frame update
    void Awake()
    {
        _slider.value = SoundManager.Instance._musicSource.volume;
    }
    void Start()
    {
        SoundManager.Instance.ChangeMasterVolume(_slider.value);
        _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMusicVolume(val));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
