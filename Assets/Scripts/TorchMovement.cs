using UnityEngine;

public class TorchMovement : MonoBehaviour
{

    public Transform TorchPos;
    //public Transform orientation;
    //float distance_x, distance_y, distance_z;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //distance_x =  Mathf.Abs (orientation.transform.position.x - TorchPos.transform.position.x);
        //distance_y = Mathf.Abs (orientation.transform.position.y - TorchPos.transform.position.y);
        //distance_z = Mathf.Abs(orientation.transform.position.z - TorchPos.transform.position.z);
    }


    // Update is called once per frame
    void Update()
    {

        transform.parent.position = TorchPos.position;

        //transform.parent.position = new (
        //    Mathf.Cos(orientation.rotation.eulerAngles.y) * distance_x,
        //    distance_y,
        //    Mathf.Cos(orientation.rotation.eulerAngles.y) * distance_z
        //);
    }
}
