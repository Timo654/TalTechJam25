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

    private void PlayParticles(EntityType entityType, ItemType __, int ___, int hitLane)
    {
        switch (entityType)
        {
            case EntityType.Scooter:
            case EntityType.Pedestrian:
                // good entity
                m_particleSystem.startColor = Color.white;
                break;
            case EntityType.Trash:
                // bad entity
                Color color = new Color(0.5f,0f , 1f);
                m_particleSystem.startColor = color;
                break;
        }
        m_particleSystem.Play();
    }
}
