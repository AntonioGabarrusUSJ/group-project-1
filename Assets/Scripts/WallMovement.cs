using UnityEngine;

public class WallMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject halfWallLeft;
    public GameObject halfWallRight;
    public float detectionDistance = 2f;
    public float rotationSpeed = 3f;

    private bool isWallClosed = false;

    private Quaternion openedLeft;
    private Quaternion openedRight;
    private Quaternion closed;

    public GameObject CloseAuto;

    private void Start()
    {
        Debug.Log(transform.rotation.eulerAngles);
        openedLeft = Quaternion.Euler(0f, -90f + transform.rotation.eulerAngles.y, 0f);
        openedRight = Quaternion.Euler(0f, 90f + transform.rotation.eulerAngles.y, 0f);
        closed = Quaternion.Euler(0f, 0f + transform.rotation.eulerAngles.y, 0f);
}

    void Update()
    {
        if (this.CloseAuto.activeSelf)
        {
            bool shouldCloseWall = IsPlayerClose();
            isWallClosed = shouldCloseWall;

            Quaternion targetRotationLeft = isWallClosed ? closed : openedLeft;
            Quaternion targetRotationRight = isWallClosed ? closed : openedRight;
            halfWallLeft.transform.rotation = Quaternion.Lerp(
                halfWallLeft.transform.rotation,
                targetRotationLeft,
                Time.deltaTime * rotationSpeed
            );

            halfWallRight.transform.rotation = Quaternion.Lerp(
                halfWallRight.transform.rotation,
                targetRotationRight,
                Time.deltaTime * rotationSpeed
            );
        }
    }

    private bool IsPlayerClose()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        return dist <= detectionDistance;
    }
}

