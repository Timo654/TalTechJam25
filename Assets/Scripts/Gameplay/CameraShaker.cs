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

    private void Shake(EntityType _)
    {
        _animator.SetTrigger("Shake");
    }
}
