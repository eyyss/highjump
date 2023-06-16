using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class JumpoMeter : MonoBehaviour
{
    public GameObject arrowGO;
    private Animator arrowAnimator;


    public float amount;
    public void Rotate()
    {
        amount = arrowGO.GetComponent<RectTransform>().eulerAngles.z;
    }
    public float GetJumpoMeterAmount()
    {
        return amount;
    }
    public void StopJumpoMeter()
    {
        arrowAnimator = arrowGO.GetComponent<Animator>();
        arrowAnimator.enabled = false;
    }
}
