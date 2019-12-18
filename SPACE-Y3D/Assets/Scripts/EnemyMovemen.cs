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
        distance = player.transform.position - Enemy.transform.position;
        //Debug.Log(distance.x + " " + distance.y + " " + distance.z);
        magDistance = distance.magnitude;
        Debug.Log("outside " + magDistance);
        if (magDistance > 2)
        {
            Debug.Log("inside if" + magDistance);
            Enemy.transform.position = Vector3.MoveTowards(transform.position, distance, moveSpeed * Time.deltaTime);
        }
    }
}
