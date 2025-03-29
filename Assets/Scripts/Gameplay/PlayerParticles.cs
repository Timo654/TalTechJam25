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

    private void PlayParticles(EntityType entityType, ItemType __, int ___)
    {
        switch (entityType)
        {
            case EntityType.Scooter:
            case EntityType.Pedestrian:
                // good entity
                break;
            case EntityType.Trash:
                // bad entity
                break;
        }
        m_particleSystem.Play();
    }
}
