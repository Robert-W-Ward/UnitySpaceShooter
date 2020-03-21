using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Triple shot = 0
// speed = 1
//shields = 2

public class PowerUps : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    private AudioSource _PowerUpSound;

    [SerializeField]
    private int powerupID;
    private void Start()
    {
        _PowerUpSound = GameObject.Find("PowerUpSound").GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(powerupID == 1)
        {
            _speed += .01f;
            transform.Translate(Vector3.down * _speed * Time.deltaTime) ;
        }
        if (transform.position.y <= -6.84)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _PowerUpSound.Play();
            Player _player = other.transform.GetComponent<Player>();
            if(_player!= null)
            {
                switch (powerupID)
                {
                    case 0:
                        _player.ActivateTripleShot();
                        break;
                    case 1:
                        _player.ActivateSpeedBoost();
                        break;
                    case 2:
                        _player.ActivateShield();
                        break;
                }

            }


            Destroy(this.gameObject);
        }
    }
}
