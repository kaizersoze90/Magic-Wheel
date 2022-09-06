using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    WheelManager _wheelManager;
    Animator _animator;

    void Start()
    {
        _wheelManager = GetComponentInParent<WheelManager>();
        _animator = GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        if (!_wheelManager.IsSpinning)
        {
            _animator.SetTrigger("Click");
            _wheelManager.Spin();
        }
    }
}
