using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shakeable : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        EnvObjectSound();
        EnvObjectAnimate();
    }

    protected void EnvObjectSound(){ }
    protected void EnvObjectAnimate() { }
}
