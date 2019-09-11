using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiAnimationHandler : MonoBehaviour
{
    public ParticleSystem confetti;
    public GameObject confettiObject;

    private void Awake()
    {
        confetti.Stop();
    }

    public void PlayConfetti()
    {
        confetti.Stop();
        confetti.Play();
    }

    public void DisableConfetti()
    {
        confettiObject.SetActive(false);
    }

    public void EnableConfetti()
    {
        confettiObject.SetActive(true);
    }
}
