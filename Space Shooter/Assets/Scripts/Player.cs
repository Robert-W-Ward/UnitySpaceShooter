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
    private float _firerate = .5f;

    private float _canFire = -1f;
    
    [SerializeField]
    private int initLives = 3;

    [SerializeField]
    public int score = 00;

    [SerializeField]
    private GameObject LaserPrefab;
 
    [SerializeField]
    private GameObject TripleShotPrefab;

    [SerializeField]
    private GameObject ShieldSprite;

   

    private SpawnManager _spawnManager;

    private UIManager _uIManager;
   
    private bool isTripShotactive = false; 

    private bool isShieldActive = false;

    void Start()
    {
        //Set current position to new position
        transform.position = new Vector3(0 , -5, 0);

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is null");
        }
        if(_uIManager == null)
        {
            Debug.LogError("UImanager is Null");
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

        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-9.4f,9.4f), Mathf.Clamp(transform.position.y, -4.75f, 4.75f), 0);

    }

    void FireLaser()
    {
        _canFire = Time.time + _firerate;

        
        if (isTripShotactive == true)
        {
            Instantiate(TripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(LaserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
    }
   

    public void Damage()
    {
        if (isShieldActive == true)
        {
            isShieldActive = false;
            
            ShieldSprite.SetActive(false);

            return;
        }
        initLives--;
        _uIManager.UpdateLives(initLives);
       

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

    public void ActivateSpeedBoost()
    {      
        speed +=3;

        StartCoroutine(SpeedBoostCooldown());

    }
     IEnumerator SpeedBoostCooldown()
    {
        yield return new WaitForSeconds(5f);

        speed = 5f;     
    }

    public void ActivateShield()
    {
        isShieldActive = true;

        ShieldSprite.SetActive(true);
    }
    public void IncreaseScore()
    {
        score += 1;
        _uIManager.UpdateScore(score);
    }

}
