using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyScript : MonoBehaviour
{
    public bool stopMovement;
    GameObject player;
    Vector3[] distance = new Vector3[4];
    Vector3[] distanceToPlayer = new Vector3[4];
    float[] mag = new float[4];
    float[] magPlayer = new float[4];



    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastCheck();
    }
    public void RaycastCheck()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 10f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.green);

            if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), 10))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 10, Color.black);
                Debug.Log("not hit");

                distance[0] = this.transform.position + transform.TransformDirection(Vector3.up) * 10;
                mag[0] = distance[0].magnitude;
                distanceToPlayer[0] = player.transform.position - distance[0];
                magPlayer[0] = distanceToPlayer[0].magnitude;
            }
            if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.Normalize(new Vector3(-45f, -45f, -45f))), 10))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.Normalize(new Vector3(-45f, -45f, -45f))) * 10, Color.yellow);
                Debug.Log("not hitX-45");

                Debug.Log(transform.TransformDirection(Vector3.Normalize(new Vector3(-45f, -45f, -45f))) * 10);

                distance[1] = this.transform.position + transform.TransformDirection(Vector3.Normalize(new Vector3(-45f, -45f, -45f))) * 10;
                mag[1] = distance[1].magnitude;
                distanceToPlayer[1] = player.transform.position - distance[1];
                magPlayer[1] = distanceToPlayer[1].magnitude;
                Debug.Log(distance[1]);
            }
            if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.Normalize(new Vector3(45f, 45f, 45f))), 10))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.Normalize(new Vector3(45f, 45f, 45f))) * 10, Color.blue);
                Debug.Log("not hitX45");

                distance[2] = this.transform.position + transform.TransformDirection(Vector3.Normalize(new Vector3(45f, 45f, 45f))) * 10;
                mag[2] = distance[2].magnitude;
                distanceToPlayer[2] = player.transform.position - distance[2];
                magPlayer[2] = distanceToPlayer[2].magnitude;
            }
            if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right),10))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 10, Color.red);
                Debug.Log("not hitRight");

                distance[3] = this.transform.position + transform.TransformDirection(Vector3.right) * 10;
                mag[3] = distance[3].magnitude;
                distanceToPlayer[2] = player.transform.position - distance[2];
                magPlayer[2] = distanceToPlayer[2].magnitude;
            }

            float small = Mathf.Min(magPlayer);
            Debug.Log(small);
            for (int i = 0; i < mag.Length; i++)
            {
                if (small == magPlayer[i])
                {
                    this.transform.position = Vector3.MoveTowards(transform.position, distance[i], 10 * Time.deltaTime);
                    Vector3 pos = player.transform.position - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(pos);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.5f * Time.deltaTime);
                }
            }

            for (int i = 0; i < mag.Length; i++)
            {
                
                magPlayer[i] = 100000000;
            }


        }
        else
        {
           Vector3 distance = this.transform.position - player.transform.position;
           float magDistance = distance.magnitude;
           MoveTowardsPlayer(magDistance, distance, 10f);
        }
    }
    public void MoveTowardsPlayer(float magDistance, Vector3 distance, float moveSpeed)
    {
        if (magDistance > 3)
        {
            // Debug.Log("inside if" + magDistance);
            this.transform.position = Vector3.MoveTowards(transform.position, -distance, moveSpeed * Time.deltaTime);
            //this.transform.LookAt(player.transform);
            Vector3 pos = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(pos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.5f * Time.deltaTime);
        }
    }
}
