using System;
using UnityEngine;

public class cameracontrol : MonoBehaviour
{
    [SerializeField] private float speed;

    private float currentposx;
    private Vector3 velocity = Vector3.zero;
    [SerializeField]private Transform player;
    [SerializeField] private float aheaddistance;
     [SerializeField] private float cameraSpeed;
     private float lookahead;


    private void Update()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            new Vector3(currentposx, transform.position.y, transform.position.z), ref velocity, speed );
            //follow the player
            transform.position=new Vector3(player.position.x+lookahead, transform.position.y, transform.position.z);
            //lookahead
            lookahead=Mathf.Lerp(lookahead,(aheaddistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform newroom)
    {
        currentposx = newroom.position.x;
    }

    internal void moveToNewRoom(Transform nextroom)
    {
        throw new NotImplementedException();
    }
}