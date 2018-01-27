using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shakeable : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        EnvObjectSound();
        EnvObjectAnimate();
    }

    protected virtual void EnvObjectSound(){ }
    protected virtual void EnvObjectAnimate() { }
}
