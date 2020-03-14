using UnityEngine;


public class Laser_Behavior : MonoBehaviour
{
    [SerializeField]
    public float Speed = 8;

    
    void Update()
    {
        transform.Translate((Vector3.up * Speed) * Time.deltaTime);
        if (transform.position.y > 6.3)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
