using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovemen : MonoBehaviour
{
    private float moveSpeed = 10f;
    GameObject player;
    GameObject Enemy;
    Rigidbody enemyRb;
    Vector3 distance;
    float magDistance;
    public GameObject bulletPrefab;
    private float waitTime = 1.5f;
    private float timeCounter = 0;
    public GameObject firePoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Enemy = this.gameObject;
        enemyRb = this.gameObject.GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        distance =  Enemy.transform.position - player.transform.position;
        //Debug.Log(distance.x + " " + distance.y + " " + distance.z);
        magDistance = distance.magnitude;
        //Debug.Log("outside " + magDistance);
        if (magDistance > 3)
        {
           // Debug.Log("inside if" + magDistance);
            Enemy.transform.position = Vector3.MoveTowards(transform.position, -distance, moveSpeed * Time.deltaTime);
            Enemy.transform.LookAt(player.transform);
            
        }if(magDistance < 3)
        {
            if (timeCounter < Time.time)
            {
                Debug.Log("in here");
                EnemyShoot();
                timeCounter = waitTime + Time.time;
            }
        }
    }
    public void EnemyShoot()
    {
      GameObject bullet =  Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        bullet.GetComponent<BulletBehavior>().Enemydamage = this.gameObject.GetComponent<Enemy>().DealDamage();
    }
}
