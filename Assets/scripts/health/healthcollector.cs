using UnityEngine;

public class healthcollector : MonoBehaviour
{
    [SerializeField] private float healthamount;
    [SerializeField]private AudioClip collector;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(collector);
            collision.GetComponent<health>().AddHealth(healthamount);

            gameObject.SetActive(false);
        }
    }
}