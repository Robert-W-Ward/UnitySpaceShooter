using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 0f;
    
    [SerializeField]
    private float RunSpeed = 0f;
    
    [SerializeField]
    private GameObject LaserPrefab;
    
    [SerializeField]
    private GameObject TripleShotPrefab;
  
    [SerializeField]
    private float _firerate = .5f;
  
    [SerializeField]
    
    private int initLives = 3;
    
    private float _canFire = -1f;
    
    private SpawnManager _spawnManager;

   

    [SerializeField]
    private bool isTripShotactive = false;

    void Start()
    {
        //Set current position to new position
        transform.position = new Vector3(0 , -5, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is null");
        }



    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
       
        

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();

        }
    } 

    void CalculateMovement()
    {
        float _HMoveInput = Input.GetAxis("Horizontal");

        float _VMoveInput = Input.GetAxis("Vertical");

        Vector3 _Direction = new Vector3(_HMoveInput, _VMoveInput, 0);

        transform.Translate(_Direction * speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.75f, 4.75f), 0);

        if (transform.position.x > 11.4)
        {
            transform.position = new Vector3(-11.4f, transform.position.y);
        }
        else if (transform.position.x < -11.4)
        {
            transform.position = new Vector3(11.4f, transform.position.y);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = RunSpeed;

        }
        else
        {
            speed = 5;
        }

    }

    void FireLaser()
    {
        _canFire = Time.time + _firerate;

        
        if (isTripShotactive == true)
        {
            Instantiate(TripleShotPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(LaserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
    }
   

    public void Damage()
    {
        initLives--;

        Debug.Log("Lives" + initLives);

        if (initLives <= 0)
        {

            _spawnManager.OnPlayerDeath();

            Destroy(this.gameObject);
           
        }
    }

    public void ActivateTripleShot()
    {
        isTripShotactive = true;
        StartCoroutine(TripleShotCoolDown());
    }

    IEnumerator TripleShotCoolDown()
    {
        
        yield return new WaitForSeconds(5.0f);
        isTripShotactive = false;
    }

}
