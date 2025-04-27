using UnityEngine;

public class LightController1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject ControlledLight;



    private void Start()
    {
        ControlledLight.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        ControlledLight.SetActive(true);
    }
}
