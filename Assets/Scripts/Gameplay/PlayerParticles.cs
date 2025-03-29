using System;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    private ParticleSystem m_particleSystem;

    private void Awake()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        EntityScript.EntityAttacked += PlayParticles;
    }

    private void OnDisable()
    {
        EntityScript.EntityAttacked -= PlayParticles;
    }

    private void PlayParticles(EntityType _, ItemType __)
    {
        m_particleSystem.Play();
    }
}
