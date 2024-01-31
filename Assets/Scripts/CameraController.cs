using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public float cameraSpeed = 1.0f;

    public GameObject worm;

    private void Update()
    {
        /*Vector3 dir = worm.transform.position - this.transform.position;
        Vector3 moveDir = new Vector3(dir.x * cameraSpeed * Time.deltaTime,
                                      0.0f,
                                      dir.y * cameraSpeed * Time.deltaTime);
        this.transform.Translate(moveDir);*/

        this.transform.position = new Vector3(worm.transform.position.x, transform.position.y, worm.transform.position.z);
    }
}