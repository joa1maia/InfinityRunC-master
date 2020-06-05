using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObj : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource _audioSource;
    public bool _playStart;
    public float _timeAudio;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    public void StartSound(AudioClip _audioClip)
    {

        if (_audioClip != null)
        {
            _audioSource.clip = _audioClip;
            _audioSource.Play();
        }
    }
    public void StartAudio()
    {
        _audioSource.Play();
    }
    void OnEnable()
    {
        if (_playStart)
        {
            Invoke("StartAudio", _timeAudio);
        }
    }
}
