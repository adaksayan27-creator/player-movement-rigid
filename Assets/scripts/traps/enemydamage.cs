using UnityEngine;

public class enemydamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health h = collision.GetComponent<health>();
            if (h != null)
                h.takedamage(damage);
        }
    }
}