using TMPro;
using UnityEngine;

public class DistanceUI : MonoBehaviour
{
    [SerializeField] private TMP_Text distanceText;
    [SerializeField] private Transform player;

    private float startX;

    private void Start()
    {
        startX = player.position.x;
    }

    private void Update()
    {
        float distance = player.position.x - startX;
        distanceText.text = "DISTANCE: " + Mathf.FloorToInt(distance) + "m";
    }
}