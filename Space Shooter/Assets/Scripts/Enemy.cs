using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player p1;

    [SerializeField]
    private float _speed = 4f;
    private BoxCollider2D _2dCollider;
    private Animator DeathAnimation;
    private AudioSource _LaserSound;

    [SerializeField]
    private GameObject EnemyLaserPrefab;
   
   
    private void Start()
    {
        p1 = GameObject.Find("Player").GetComponent<Player>();
        if(p1 == null)
        {
            Debug.LogError("Player is null");        
        }
        DeathAnimation = GetComponent<Animator>();
        if (DeathAnimation == null)
        {
            Debug.LogError("Animator is null");
        }
        _LaserSound = GameObject.Find("ExplosionSound").GetComponent<AudioSource>();
        StartCoroutine(EnemyFireCooldown());
    }
    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }
    void CalculateMovement()
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
        if (other.CompareTag("Player"))
        {
            Player p1 = other.transform.GetComponent<Player>();
            
            if (p1 != null)
            {
                p1.Damage();
            }
            DeathAnimation.SetTrigger("OnEnemyDeath");
            _LaserSound.Play();
            GetComponent<BoxCollider2D>().enabled = false;
            _speed = 0f;
            Destroy(this.gameObject,3f);

        }
        //Destorys laser and enemy object//
        if (other.CompareTag("Laser") || other.CompareTag("PlayerLaser"))
        {
            
            Destroy(other.gameObject);
            if (p1 != null)
            {
                p1.IncreaseScore();
            }

            DeathAnimation.SetTrigger("OnEnemyDeath");
            _LaserSound.Play();
            GetComponent<BoxCollider2D>().enabled = false;
            
            _speed = 0f;
            Destroy(this.gameObject,3f);
       }

    }
    void FireLaser()
    {
        Instantiate(EnemyLaserPrefab,transform.position + new Vector3(0,-.8f, 0),Quaternion.identity);
        
    }
   IEnumerator EnemyFireCooldown()
    {
        while (true)
        {
            FireLaser();
            yield return new WaitForSeconds(Random.Range(3f,7f));
        }
    }
   
}   
