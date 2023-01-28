using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mSens = 250f;
    public Transform pBody;
    float xRotate = 0f;
    float mX, mY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate(){
        mX = Input.GetAxis("Mouse X") * mSens * Time.deltaTime;
        mY = Input.GetAxis("Mouse Y") * mSens * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {

        xRotate -= mY;
        xRotate = Mathf.Clamp(xRotate, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotate, 0f, 0f);
        pBody.Rotate(Vector3.up * mX);


    }
}
