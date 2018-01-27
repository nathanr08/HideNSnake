using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShaker : Shakeable {

    [SerializeField]
    private AudioClip soundClip;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void EnvObjectSound()
    {
        if( audioSource )
            audioSource.PlayOneShot(soundClip);
    }
    protected override void EnvObjectAnimate()
    {

    }
}
