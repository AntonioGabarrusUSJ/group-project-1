using System;
using System.Collections;
using UnityEngine;

public class SlabController : MonoBehaviour
{
    public GameObject CollsionDetector;
    public Transform LowPoint;
    public Transform HighPoint;
    bool goDown, goUp;
    public float SlabSpeed;
    bool lightOn;
    public GameObject ControlledLight;
    public float SecondsOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightOn = false;
        this.CollsionDetector.SetActive(true);
        goDown = false;
        goUp = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.CollsionDetector.SetActive(false);
        goUp = false;
        goDown = true;
        //Vector3.MoveTowards(transform.position, LowPoint.position, 0.1f);
        //StartCoroutine(AnimateSlab(miliseconds));
        StartCoroutine(LightControl(SecondsOn));
    }

    private void OnCollisionExit(Collision collision)
    {
        goDown = false;
        goUp = true;
    }

    private IEnumerator LightControl(float seconds)
    {
        lightOn = true;
        yield return new WaitForSeconds(seconds);
        lightOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (goDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, LowPoint.position, Time.deltaTime * SlabSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, HighPoint.position, Time.deltaTime * SlabSpeed);
        }
        ControlledLight.SetActive(lightOn);
    }
}
