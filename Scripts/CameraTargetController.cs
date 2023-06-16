using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraTargetController : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private Transform racerHips;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        racerHips = FindObjectOfType<RacerController>().hips.transform;
    }

    private void Start()
    {
        RacerController.OnPlayerJump += OnPlayerJump;
    }
    
    private void OnPlayerJump()
    {
        cinemachineVirtualCamera.Follow = racerHips;
        cinemachineVirtualCamera.LookAt = racerHips;
    }




    private void OnDestroy()
    {
        RacerController.OnPlayerJump -= OnPlayerJump;
    }

}
