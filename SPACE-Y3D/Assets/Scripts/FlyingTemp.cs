using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingTemp : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float rotationalDamp = .5f;

    [SerializeField] float detectionDistance = 1f;
    [SerializeField] float rayCastOffset = 2f;
    public GameObject firePoint;
    public GameObject bulletPrefab;
    EnemyAudioScript enemyAudio;

    private float waitTime = 0.5f;
    private float timeCounter = 0;

    // Update is called once per frame
    void Update()
    {
        enemyAudio = this.gameObject.GetComponent<EnemyAudioScript>();
        pathFinding();
        //Turn();
        Move();

        if ((transform.position - target.position).magnitude < 15 && (timeCounter < Time.time))
        {
                //Debug.Log("in here");
                EnemyShoot();
                timeCounter = waitTime + Time.time;
        }
     
    }

    void Turn()
    {
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
        //transform.LookAt(target.transform);
        /*float magDistance = pos.magnitude;
        MoveTowardsPlayer(magDistance, pos, 10f);*/
    }

    void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    void pathFinding()
    {
        RaycastHit hit;
        Vector3 raycastOffset = Vector3.zero;

        Vector3 left = transform.position - transform.right * rayCastOffset;
        Vector3 right = transform.position + transform.right * rayCastOffset;
        Vector3 up = transform.position + transform.up * rayCastOffset;
        Vector3 down = transform.position - transform.up * rayCastOffset;

        Debug.DrawRay(left, transform.forward * detectionDistance, Color.black);

        if (Physics.Raycast(left, transform.forward, out hit, detectionDistance))
        {
            raycastOffset += Vector3.right;
        }
        else if(Physics.Raycast(up, transform.forward, out hit, detectionDistance))
        {
            raycastOffset -= Vector3.right;
        }
        if(Physics.Raycast(up, transform.forward, out hit, detectionDistance))
        {
            raycastOffset -= Vector3.up;
        }
        else if (Physics.Raycast(down, transform.forward, out hit, detectionDistance))
        {
            raycastOffset += Vector3.up;
        }

        if(raycastOffset != Vector3.zero)
        {
            transform.Rotate(raycastOffset * 5f * Time.deltaTime);
        }
        else
        {
            Turn();
        }


    }

    public void MoveTowardsPlayer(float magDistance, Vector3 pos, float movementSpeed)
    {
        if (magDistance > 3)
        {
            // Debug.Log("inside if" + magDistance);
            this.transform.position = Vector3.MoveTowards(transform.position, pos, movementSpeed * Time.deltaTime);
            this.transform.LookAt(target.transform);

        }
    }

    public void EnemyShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        bullet.GetComponent<BulletBehavior>().Enemydamage = this.gameObject.GetComponent<Enemy>().DealDamage();
        //enemyAudio.PlayAudio();
    }
}

    



