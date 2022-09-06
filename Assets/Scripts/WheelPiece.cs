using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WheelPiece : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] float rewardValue;
    [SerializeField] string nextLevelText;
    [SerializeField] bool isNextLevel;


    void Start()
    {
        if (isNextLevel)
        {
            GetComponentInChildren<TextMeshPro>().text = nextLevelText;
        }
        else
        {
            GetComponentInChildren<TextMeshPro>().text = "x" + rewardValue.ToString();
        }
    }

    public float ReadValue()
    {
        if (isNextLevel)
        {
            return -100f;     //Any float value under 0f means move on to the next wheel
        }
        else
        {
            return rewardValue;
        }
    }
}
