using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    [SerializeField] private health playerhealth;
    [SerializeField] private Image totalhealth;
    [SerializeField] private Image currenthealth;

    private void Start()
    {
        totalhealth.fillAmount = 1;
    }

    private void Update()
    {
        currenthealth.fillAmount =
            playerhealth.currenthealth / playerhealth.startinghealth;
    }
}