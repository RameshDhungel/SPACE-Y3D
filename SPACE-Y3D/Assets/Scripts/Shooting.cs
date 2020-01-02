﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    private bool rightClick = false;
    private bool emptyMag = false;
    private bool reloading = false;
    private float currentAmmo;
    private float totalAmmo;
    private float mag;

    private Vector3 mousePos;

    Camera mainCam;
    public Transform firepoint;
    public Transform weapon;

    public GameObject bulletPrefab;
    public GameObject crosshair;
    public GameObject rocketPrefab;
    public GameObject currentAmmoImg;
    public GameObject totalAmmoImg;

    public Animator anim;

   


    float rotateAmount;
    float senseY = 300;

    void Start()
    {
        mainCam = Camera.main;
        mag = 20f;
        currentAmmo = mag;
        totalAmmo = 50f;
        currentAmmoImg.GetComponent<Text>().text = currentAmmo.ToString();
        totalAmmoImg.GetComponent<Text>().text = totalAmmo.ToString();
        

    }

   
    void Update()
    {
        currentAmmoImg.GetComponent<Text>().text = currentAmmo.ToString();
        totalAmmoImg.GetComponent<Text>().text = totalAmmo.ToString();
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("here");
            StartCoroutine(Reload());
        }
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

            if (Input.GetKeyDown("mouse 0") && !emptyMag && !reloading)
            {
              
                if (weapon.name != "Rocketlauncher")
                {
                    GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
                }
                else
                {
                    GameObject rocket = Instantiate(rocketPrefab, firepoint.position, firepoint.rotation);
                }

                if (totalAmmo > 0 || currentAmmo > 0)
                {
                    if (currentAmmo > 0)
                    {
                        currentAmmo--;
                        currentAmmoImg.GetComponent<Text>().text = currentAmmo.ToString();
                    }
                    else
                    {
                        StartCoroutine(Reload());
                        
                    }
                }
                else
                {
                    emptyMag = true;
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

    IEnumerator Reload()
    {
        anim.SetBool("isReloading", true);
        reloading = true;
        
        yield return new WaitForSeconds(2f);
        if(currentAmmo < 1)
        {
            float addingAmmoAmount = mag - currentAmmo;
            if(totalAmmo >= addingAmmoAmount){
                currentAmmo = mag;
                totalAmmo -= addingAmmoAmount;
            }
            else
            {
                Debug.Log("here");
                currentAmmo = currentAmmo+addingAmmoAmount;
                totalAmmo = 0;
            }
        }
        else
        {
            if((totalAmmo - mag) > 0)
            {
                currentAmmo = mag;
                totalAmmo -= mag;
            }
            else
            {
                currentAmmo = mag;
                totalAmmo = 0;
            }
        }
        currentAmmoImg.GetComponent<Text>().text = currentAmmo.ToString();
        totalAmmoImg.GetComponent<Text>().text = totalAmmo.ToString();

        anim.SetBool("isReloading", false);
        reloading = false;
    }

}
