using UnityEngine;

public class Block : MonoBehaviour
{
    private TowerManager towerManager;

    void Start()
    {
        towerManager = FindObjectOfType<TowerManager>();  // Reference to TowerManager
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;  // Stop falling
            towerManager.OnBlockLanded(this.gameObject);  // Notify the tower manager
        }
    }
}
