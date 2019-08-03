using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float FollowDistance;
    public float MouseSensitivity = 5;
    public float SmoothTime = 0.3f;
    
    [SerializeField]
    private float minAngleX = 0;
    [SerializeField]
    private float maxAngleX = 60;

    private float minAngleXRad;
    private float maxAngleXRad;

    private float angleY;
    private float angleX;

    private float angleYVelocity;
    private float angleXVelocity;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        angleY = Mathf.Deg2Rad * eulerAngles.y;
        angleX = Mathf.Deg2Rad * eulerAngles.x;

        Debug.Log(eulerAngles);

        minAngleXRad = Mathf.Deg2Rad * minAngleX;
        maxAngleXRad = Mathf.Deg2Rad * maxAngleX;

        updatePosition();
    }

    private void updatePosition()
    {

        float offsetY = FollowDistance * Mathf.Sin(angleX);
        Vector3 offsetOnXZPlane = new Vector3(-Mathf.Sin(angleY), 0, -Mathf.Cos(angleY));
        offsetOnXZPlane *= (FollowDistance * Mathf.Cos(angleX));

        Vector3 offset = new Vector3(offsetOnXZPlane.x, offsetY, offsetOnXZPlane.z);
        transform.position = Target.position + offset;
        transform.LookAt(Target);

        Debug.Log("offset: " + offset);
        Debug.Log("Distance: " + offset.magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        float targetAngleY = angleY + mouseX * MouseSensitivity;
        float targetAngleX = angleX + mouseY * MouseSensitivity;
        targetAngleX = Mathf.Clamp(targetAngleX, minAngleXRad, maxAngleXRad);

        angleY = Mathf.SmoothDampAngle(angleY, targetAngleY, ref angleYVelocity, SmoothTime);
        angleX = Mathf.SmoothDampAngle(angleX, targetAngleX, ref angleXVelocity, SmoothTime);

        updatePosition();
    }
}
