using UnityEngine;

public class enemy_side : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    private bool movingLeft;
    private float leftedge;
    private float rightedge;

    private void Awake()
    {
        leftedge = transform.position.x - movementDistance;
        rightedge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        if (movingLeft)
        {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            if (transform.position.x <= leftedge)
                movingLeft = false;
        }
        else
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            if (transform.position.x >= rightedge)
                movingLeft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health h = collision.GetComponent<health>();
            if (h != null)
                h.takedamage(damage);
        }
    }
}