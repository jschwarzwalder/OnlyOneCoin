using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float FollowDistance;
    public float MaxSpeed = 10;
    public float MaxAngularSpeed = 20;

    public float MinAngleY = -180;
    public float MaxAngleY =  180;
    public float MinAngleX = -60;
    public float MaxAngleX =  60;

    private float angleY;
    private float angleX;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        angleY = eulerAngles.y;
        angleX = eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
