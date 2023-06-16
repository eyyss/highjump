using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hips : MonoBehaviour
{
    public RacerController racer;
    public GameObject hitArrow;
    private bool oneHit;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            if (collision.collider.TryGetComponent(out Point pointScript)&&!oneHit) 
            {
                Instantiate(hitArrow,collision.GetContact(0).point,Quaternion.identity);
                GameManager.Instance.SetPoint(pointScript.GetPoint());
                oneHit = true;
            }
            racer.isforwardForce = false;
            foreach (Rigidbody rb in racer.bodyRigidbodyList)
            {
                rb.drag = 0;
                if (rb != racer.bodyRigidbodyList[0]) rb.useGravity = true;
            }
        }
    }
}
