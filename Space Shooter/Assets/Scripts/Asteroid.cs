using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 3f;

    [SerializeField]
    private GameObject Explosion;

    private AudioSource _LaserSound;
    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _LaserSound = GameObject.Find("ExplosionSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      
        transform.Rotate(Vector3.forward * _rotateSpeed *Time.deltaTime);
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerLaser")
        {
            Destroy(other.gameObject);

            Instantiate(Explosion,transform.position,Quaternion.identity);
            _LaserSound.Play();
            _spawnManager.StartSpawning();

            Destroy(this.gameObject,.25f);
        }
    }





}
