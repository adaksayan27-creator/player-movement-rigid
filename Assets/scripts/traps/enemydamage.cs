using UnityEngine;

public class enemydamage : MonoBehaviour
{
    [SerializeField]protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<health>().takedamage(damage);
        }
    }
}
