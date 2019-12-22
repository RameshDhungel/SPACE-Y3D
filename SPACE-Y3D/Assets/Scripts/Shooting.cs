using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    private bool rightClick = false;

    private Vector3 mousePos;

    Camera mainCam;
    public Transform firepoint;
    public Transform weapon;

    public GameObject bulletPrefab;
    public GameObject crosshair;
    public GameObject rocketPrefab;

    float rotateAmount;
    float senseY = 300;

    void Start()
    {
        mainCam = Camera.main;
    }

   
    void Update()
    {
        Shoot();
    }

    public void Shoot()
    {

        if (Input.GetKeyDown("mouse 1"))
        {
            rightClick = true;
        }
        if (Input.GetKeyUp("mouse 1"))
        {
            rightClick = false;
        }
        if (rightClick)
        {
            mainCam.transform.localPosition = new Vector3(1.5f, 0.5f, -3);
            crosshair.SetActive(true);

            //Vertical Rotation of the camera
            rotateAmount += Input.GetAxis("Mouse Y") * Time.deltaTime * senseY;
            rotateAmount = Mathf.Clamp(rotateAmount, -20, 16);
            mainCam.transform.localEulerAngles = Vector3.left * rotateAmount;
            mousePos = mainCam.transform.localEulerAngles;

            //Firepoint follows crosshair
            weapon.transform.localEulerAngles = Vector3.left * (rotateAmount + 1);
            //weapon.rotation = new Quaternion(0, 90, mainCam.transform.rotation.x, 1);
            //weapon.rotation = mainCam.transform.localEulerAngles;

            if (Input.GetKeyDown("mouse 0"))
            {
                if (weapon.name != "Rocketlauncher")
                {
                    GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
                }
                else
                {
                    GameObject rocket = Instantiate(rocketPrefab, firepoint.position, firepoint.rotation);
                }


            }

        }
        else
        {
            mainCam.transform.localPosition = new Vector3(0f, 0.5f, -5);
            mainCam.transform.localEulerAngles = new Vector3(10, 0, 0);
            rotateAmount = 0f;
            crosshair.SetActive(false);
        }


    }

}
