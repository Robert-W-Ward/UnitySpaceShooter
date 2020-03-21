using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

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

    [SerializeField]
    private GameObject _LeftEngine;
    [SerializeField]
    private GameObject _RightEngine;
    [SerializeField]
    private GameObject Explosion;

    private SpawnManager _spawnManager;

    private AudioSource _LaserSound;
    private AudioSource _ExplosionSound;

    private UIManager _uIManager;
   
    private bool isTripShotactive = false; 

    private bool isShieldActive = false;

    void Start()
    {
        //Set current position to new position
        transform.position = new Vector3(0 , -5, 0);

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _LaserSound = GameObject.Find("Laser_SoundEffect").GetComponent<AudioSource>();
        _ExplosionSound = GameObject.Find("ExplosionSound").GetComponent<AudioSource>();
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

#if UNITY_IOS
        if (Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire") && Time.time > _canFire)
        {
            FireLaser();
        }
#else 
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
#endif
    } 

    void CalculateMovement()
    {
        float _HMoveInputM = CrossPlatformInputManager.GetAxis("Horizontal");// Input.GetAxis("Horizontal");
        float _VMoveInputM = CrossPlatformInputManager.GetAxis("Vertical");//Input.GetAxis("Vertical");

        float _HMoveInputPC = Input.GetAxis("Horizontal");
        float _VMoveInputPC = Input.GetAxis("Vertical");
#if UNITY_IOS
        Vector3 _DirectionM = new Vector3(_HMoveInputM, _VMoveInputM, 0);
        transform.Translate(_DirectionM * speed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.4f, 9.4f), Mathf.Clamp(transform.position.y, -4.75f, 4.75f), 0);
#endif
        Vector3 _Direction = new Vector3(_HMoveInputPC, _VMoveInputPC, 0);

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
        _LaserSound.Play();

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
        if(initLives == 2)
        {
            _LeftEngine.SetActive(true);
        }else if (initLives == 1)
        {
            _LeftEngine.SetActive(true);
            _RightEngine.SetActive(true);
        }


        if (initLives <= 0)
        {
            _ExplosionSound.Play();
            Instantiate(Explosion, transform.position, Quaternion.identity);
            _spawnManager.OnPlayerDeath();

            Destroy(gameObject);
           
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
