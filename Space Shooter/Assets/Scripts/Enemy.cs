using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player p1;

    [SerializeField]
    private float _speed = 4f;
    private void Start()
    {
        p1 = GameObject.Find("Player").GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
       //Enemy Movement//
    transform.Translate(Vector3.down*_speed*Time.deltaTime);
        
        if (transform.position.y <= -9)
        {
            transform.position = new Vector3(Random.Range(-9f, 9f), 8, 0);
        }
    }
    //Enemy Collison//
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Destroy Enemy Object and damages player//
        if (other.tag == "Player")
        {
            Player p1 = other.transform.GetComponent<Player>();
            
            if (p1 != null)
            {
                p1.Damage();
            }

            Destroy(this.gameObject);

        }
        //Destorys laser and enemy object//
        if (other.tag == "Laser")
        {            
            Destroy(other.gameObject);
            if (p1 != null)
            {
                p1.IncreaseScore();
            }
            

            Destroy(this.gameObject);
       }
    }
}   
