using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farmework;
public class Particle : MonoBehaviourSimply {
    ParticleSystem _PS;

    public ParticleSystem GetPS {
        get {
            return _PS;
        }
    }

    // Use this for initialization
    IEnumerator Start () {
        _PS = GetComponent<ParticleSystem>();
        while (_PS.isPlaying) {
            yield return new WaitForEndOfFrame();
        }
        GameObject.Destroy(gameObject);
    }


    protected override void OnBeforeDestroy(){

    }
}
