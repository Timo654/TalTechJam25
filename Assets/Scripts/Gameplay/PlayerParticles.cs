using System;
using UnityEngine;
using System.Collections;
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
        // hitlane 0 0.64, 1 0.13, 2 -0.58 
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
        switch (hitLane)
        {
            case 0:
                m_particleSystem.transform.localPosition = new Vector3(0.64f, m_particleSystem.transform.localPosition.y, m_particleSystem.transform.localPosition.z);
                break;
            case 1:
                m_particleSystem.transform.localPosition = new Vector3(0.13f, m_particleSystem.transform.localPosition.y, m_particleSystem.transform.localPosition.z);
                break;
            case 2:
                m_particleSystem.transform.localPosition = new Vector3(-0.58f, m_particleSystem.transform.localPosition.y, m_particleSystem.transform.localPosition.z);
                break;
        }


        m_particleSystem.Play();
    }
}
