using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] private Transform previousroom;
    [SerializeField] private Transform nextroom;
    [SerializeField] private cameracontrol cam;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || triggered)
            return;

        triggered = true;

        // Skip if references are missing
        if (cam == null || previousroom == null || nextroom == null)
            return;

        if (transform.position.x > collision.transform.position.x)
        {
            cam.MoveToNewRoom(nextroom);

            Room next = nextroom.GetComponent<Room>();
            Room prev = previousroom.GetComponent<Room>();

            if (next != null) next.ActivateRoom(true);
            if (prev != null) prev.ActivateRoom(false);
        }
        else
        {
            cam.MoveToNewRoom(previousroom);

            Room prev = previousroom.GetComponent<Room>();
            Room next = nextroom.GetComponent<Room>();

            if (prev != null) prev.ActivateRoom(true);
            if (next != null) next.ActivateRoom(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            triggered = false;
    }
}