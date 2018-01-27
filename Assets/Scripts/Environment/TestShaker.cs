using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShaker : Shakeable
{

    [SerializeField]
    private AudioClip soundClip;
    private AudioSource audioSource;
    private Vector3 startPos;
    private float jiggle;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startPos = transform.position;
    }

    protected override void EnvObjectSound()
    {
        if (audioSource)
            audioSource.PlayOneShot(soundClip);
    }
    protected override void EnvObjectAnimate()
    {
        jiggle = 2.0f;
    }

    private void Update()
    {
        if (jiggle >= 1.5f)
        {
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
            jiggle -= Time.deltaTime;
        }
        else if (jiggle >= 1.0f)
        {
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
            jiggle -= Time.deltaTime;
        }
        else if (jiggle >= 0.5f)
        {
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime);
            jiggle -= Time.deltaTime;
        }
        else if (jiggle >= 0.0f)
        {
            transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime);
            jiggle -= Time.deltaTime;
        }
    }
}
