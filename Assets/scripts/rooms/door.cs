using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] private Transform previousroom;
    [SerializeField] private Transform nextroom;
    [SerializeField] private cameracontrol cam;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            triggered = true;

            if (transform.position.x > collision.transform.position.x)
                cam.MoveToNewRoom(nextroom);
            else
                cam.MoveToNewRoom(previousroom);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            triggered = false;
        }
    }
}