using System;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        EntityScript.EntityAttacked += Shake;
    }

    private void OnDisable()
    {
        EntityScript.EntityAttacked -= Shake;
    }

    private void Shake(EntityType _, ItemType __, int ___, int ____)
    {
        _animator.SetTrigger("Shake");
    }
}
