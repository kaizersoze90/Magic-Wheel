using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public float CurrentReward { get; private set; }

    void OnTriggerEnter2D(Collider2D collision)
    {
        CurrentReward = collision.GetComponent<WheelPiece>().ReadValue();
    }
}
