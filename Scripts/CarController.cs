using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class CarController : MonoBehaviour
{
    float inputX,inputY;

    public float steerAngle,motorTorque,breakeTorque,handBreakeTorque;

    public List<WheelCollider> wheelColliderList;
    public List<GameObject> wheelMeshList;
    public Material lightMaterial;
    private Rigidbody rb;
    private Color startLightColor;

    private bool canDrive=true;

    private void Start()
    {
        startLightColor = lightMaterial.GetColor("_EmissionColor");
        rb= GetComponent<Rigidbody>();  
    }
    public void Update()
    {
        if (!canDrive) return;
        HandleInput();
        ApplyMotor();
        ApplyBreak();
        ApplySteer();
        UpdateWheel();
        ResetCarRotation();
    }



    private void FixedUpdate()
    {
        UpdateWheel();
    }

    private void UpdateWheel()
    {
        Vector3 pos = Vector3.zero;
        Quaternion rot = Quaternion.identity;
        for (int i = 0; i < wheelColliderList.Count; i++)
        {
            wheelColliderList[i].GetWorldPose(out pos, out rot);
            wheelMeshList[i].transform.position = pos;
            wheelMeshList[i].transform.rotation= rot;
        }
    }

    private void ApplyBreak()
    {

        if (Input.GetKey(KeyCode.S))
        {
            LigthMaterialChangeColor(Color.red);
            if (rb.velocity.z > .3f)
            {
                if (inputY < 0) SetBreakeTorque(breakeTorque);
            }
            else 
            {
                SetBreakeTorque(0);
                LigthMaterialChangeColor(Color.red);
            }
        }
        if (Input.GetKeyUp(KeyCode.S)) 
        {
            SetBreakeTorque(0);
            LigthMaterialChangeColor(startLightColor);
        }


        if (Input.GetKey(KeyCode.Space)) 
        {
            rb.drag = .5f;
            SetBreakeTorque(handBreakeTorque,2);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.drag = 0f;
            SetBreakeTorque(0,2);
        }

    }

    private void ApplySteer()
    {
        for (int i = 0; i < wheelColliderList.Count; i++)
        {
            if (i>1)
            {
                break;
            }
            wheelColliderList[i].steerAngle = inputX;
        }
    }

    private void ApplyMotor()
    {
        foreach (var wheel in wheelColliderList)
        {
            wheel.motorTorque = inputY;
        }
    }

    private void HandleInput()
    {
        inputX = Input.GetAxis("Horizontal") * steerAngle;
        inputY = Input.GetAxisRaw("Vertical") * motorTorque * Time.deltaTime;
    }
    private void ResetCarRotation()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Quaternion rot = Quaternion.LookRotation(Camera.main.transform.forward);
            transform.rotation= rot;
        }
        
    }
    public void SetBreakeTorque(float torque,float wheelCount=4)
    {
        for (int i = 0; i < wheelColliderList.Count; i++)
        {
            if (wheelCount > 2) wheelColliderList[i].brakeTorque = torque;
            else
            {
                if (i>2)
                {
                    break;
                }
                wheelColliderList[i].brakeTorque = torque;
            }
        }

    }
    public void LigthMaterialChangeColor(Color color)
    {
        lightMaterial.SetColor("_EmissionColor", color);
    }

    private void OnApplicationQuit()
    {
        LigthMaterialChangeColor(startLightColor);
    }



    // events
    private void OnEnable()
    {
        RacerController.OnPlayerJump += OnPlayerJump;
    }

    private void OnPlayerJump()
    {
        SetBreakeTorque(breakeTorque);
        canDrive = false;
    }

    private void OnDisable()
    {
        RacerController.OnPlayerJump -= OnPlayerJump;
    }

}
