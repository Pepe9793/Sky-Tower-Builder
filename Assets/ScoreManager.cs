using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private TowerManager towerManager;

    void Start()
    {
        towerManager = FindObjectOfType<TowerManager>();
        scoreText.text = "Score: 0";
    }

    void Update()
    {
        scoreText.text = "Score: " + towerManager.score.ToString();
    }
}
