using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;

    [SerializeField] private TMP_Text scoreText;

    private float _pendingDistance;
    private int _pendingCoins;
    private bool _hasPendingScore;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        if (_hasPendingScore)
        {
            ApplyScore(_pendingDistance, _pendingCoins);
            _hasPendingScore = false;
        }
    }

    public void UpdateScore(float distance, int coins)
    {
        if (scoreText != null)
        {
            ApplyScore(distance, coins);
        }
        else
        {
            _pendingDistance = distance;
            _pendingCoins = coins;
            _hasPendingScore = true;
        }
    }

    private void ApplyScore(float distance, int coins)
    {
        scoreText.text =
            "Distance: " + Mathf.FloorToInt(distance) + "m\n" +
            "Coins: " + coins;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}