using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int coins;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCoin()
    {
        coins++;
        Debug.Log("Coins: " + coins);
    }
}