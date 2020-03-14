using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot_PowerUp : MonoBehaviour
{

    private int _speed = 3;

   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6.84)
        {
            Destroy(this.gameObject);
        }
        





    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player _player = other.transform.GetComponent<Player>();
            if(_player!= null)
            {
                _player.ActivateTripleShot();
            }

            Destroy(this.gameObject);
            
        }

    }




}
