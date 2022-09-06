using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Pointer pointer;
    [SerializeField] Transform[] wheels;
    [SerializeField] Transform[] pointerWaypoints;

    [Header("General Settings")]
    [SerializeField] AnimationCurve curve;
    [SerializeField] int rotateCycle;
    [SerializeField] float rotateTime;
    [SerializeField] float reSpinDelay;

    public bool IsSpinning { get; private set; }

    int _wheelIndex;

    void Start()
    {
        pointer.transform.position = pointerWaypoints[_wheelIndex].position;
    }

    public void Spin()
    {
        StartCoroutine(nameof(ProcessSpin));
    }

    IEnumerator ProcessSpin()
    {
        IsSpinning = true;

        //Play wheel animation
        wheels[_wheelIndex].GetComponent<Animator>().SetTrigger("ShowOff");

        //Defining variables for spin
        float startAngle = wheels[_wheelIndex].transform.eulerAngles.z;
        float currentTime = 0f;
        float targetAngle = ((rotateCycle * 360f) + Random.Range(0f, 360f)) - startAngle;

        //Process wheel spin
        while (currentTime < rotateTime)
        {
            yield return new WaitForEndOfFrame();

            currentTime += Time.deltaTime;

            //Calculating spin motion by curve from inspector
            float currentAngle = targetAngle * curve.Evaluate(currentTime / rotateTime);

            wheels[_wheelIndex].transform.eulerAngles = new Vector3(0, 0, -currentAngle + startAngle);
        }

        Debug.Log(pointer.CurrentReward);

        //Any float value under 0f means 'Next Wheel' piece came up
        if (pointer.CurrentReward < 0f)
        {
            ProceedNextWheel();
            yield break;
        }

        //Reset pointer position when spin completed on upper wheels
        if (_wheelIndex >= 1)
        {
            yield return new WaitForSeconds(reSpinDelay);

            _wheelIndex = 0;

            pointer.GetComponent<SpriteRenderer>().sortingOrder = 59;
            pointer.transform.position = pointerWaypoints[_wheelIndex].position;
        }

        IsSpinning = false;
    }

    void ProceedNextWheel()
    {
        _wheelIndex++;

        //Move pointer to the upper wheel
        pointer.GetComponent<SpriteRenderer>().sortingOrder -= 10;
        pointer.transform.position = pointerWaypoints[_wheelIndex].position;

        Spin();
    }
}
