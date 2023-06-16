using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerController : MonoBehaviour
{
    private Animator aniamtor;
    public GameObject hips;
    public JumpoMeter jumpoMeter;
    public List<Rigidbody >bodyRigidbodyList;
    public float glidingForce;

    public static Action OnPlayerJump;

    [NonSerialized]public bool isforwardForce;
    public float forwardSpeed = 100f;


    private void Start()
    {
        aniamtor = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl)&& !isforwardForce)
        {
            Time.timeScale = .4f;
            jumpoMeter.gameObject.SetActive(true);
            jumpoMeter.Rotate();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Time.timeScale = 1f;
            jumpoMeter.StopJumpoMeter();
            aniamtor.CrossFade("Jump Over", .2f);
            transform.parent = null;
        }

        if (Input.GetKey(KeyCode.Backspace))
        {
            Time.timeScale = -1;
        }
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            Time.timeScale = 1;
        }

    }
    public void OpenRaggdol()
    {
        OnPlayerJump?.Invoke();
        aniamtor.enabled = false;
        hips.SetActive(true);
        isforwardForce = true;
        foreach (Rigidbody rb in bodyRigidbodyList) 
        {
            Vector3 force = transform.forward + Vector3.up * jumpoMeter.GetJumpoMeterAmount() * 7;
            force = force * UnityEngine.Random.Range(1, 4);
            rb.AddForce( force,ForceMode.Impulse);
            rb.drag = 2f;
        }

    }
    private void FixedUpdate()
    {
        if (isforwardForce)
        {
            bodyRigidbodyList[0].AddForce(transform.forward* forwardSpeed*Time.fixedDeltaTime
                , ForceMode.Acceleration);
            float x = Input.GetAxisRaw("Horizontal");float y = Input.GetAxisRaw("Vertical");
            Vector3 inputDirection = new Vector3(x, 0, y);
            inputDirection = Camera.main.transform.TransformDirection(inputDirection);
            bodyRigidbodyList[0].AddForce(inputDirection * glidingForce*Time.fixedDeltaTime, ForceMode.Force);
        }
    }

}
