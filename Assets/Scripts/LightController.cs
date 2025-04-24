using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightController : MonoBehaviour
{
    [Header("Lightning Options")]
    public Light ControlledSpotlight;


    [Header("Slab Params")]
    public float maxDistanceTravelled;
    bool goingUp = false;
    float initialPosition;
    
    bool triggerable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = GetComponent<GameObject>().transform.position.y;
        triggerable = true;
        ControlledSpotlight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerable)
        {
            StartCoroutine(LightCoroutine());
        }

    }

    IEnumerator LightCoroutine()
    {
        triggerable = false;
        ControlledSpotlight.enabled = true;
        yield return new WaitForSeconds(2.0f);
        ControlledSpotlight.enabled = false;
        triggerable = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
