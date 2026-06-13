using UnityEngine;
using System.Collections;

public class firetraps : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("Firetrap Timer")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeDuration;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;
    private bool active;

    private void Awake()
    {
        anim=GetComponent<Animator>();
        spriteRend=GetComponent<SpriteRenderer>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            StartCoroutine(ActivateFiretrap());
        }
        if (active)
        {
            collision.GetComponent<health>().takedamage(damage);
        }
    }
    private IEnumerator ActivateFiretrap()
    {
        spriteRend.color = Color.red;
        triggered = true;
        yield return new WaitForSeconds(activationDelay);
         spriteRend.color = Color.white;
        active = true;
        yield return new WaitForSeconds(activeDuration);
        active = false;
        triggered = false;
    }
}
