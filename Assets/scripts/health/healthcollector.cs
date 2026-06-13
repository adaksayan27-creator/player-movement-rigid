using UnityEngine;

public class healthcollector : MonoBehaviour
{
    [SerializeField] private float healthamount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<health>().AddHealth(healthamount);

            gameObject.SetActive(false);
        }
    }
}