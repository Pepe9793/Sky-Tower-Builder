using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;  // Assign your block prefab here
    private GameObject currentBlock;

    private float spawnPosX = 0f;    // X position of the rope/hook
    private float spawnPosY = 5f;    // Y position to release the block
    private float pendulumSpeed = 2f;  // Speed of the pendulum swing
    private float swingAmplitude;  // This will be calculated based on screen size

    private bool isSwinging = true;  // Flag to indicate whether the block is still swinging
    private float spawnDelay = 1f;   // Delay before spawning the next block

    void Start()
    {
        // Calculate the swing amplitude based on the screen width
        float screenWidthInWorldUnits = Camera.main.orthographicSize * Camera.main.aspect * 2f;
        swingAmplitude = screenWidthInWorldUnits / 3f;  // Swing amplitude is 1/3 of the screen width

        // Spawn the first block when the game starts
        SpawnBlock();
    }

    void Update()
    {
        // Handle swinging and release logic only if there's a current block
        if (currentBlock != null && isSwinging)
        {
            PendulumSwing();

            if (Input.GetMouseButtonDown(0))  // Tap to release the block
            {
                ReleaseBlock();
            }
        }
    }

    void SpawnBlock()
    {
        currentBlock = Instantiate(blockPrefab, new Vector2(spawnPosX, spawnPosY), Quaternion.identity);
        Rigidbody2D rb = currentBlock.GetComponent<Rigidbody2D>();

        // Ensure the block doesn't rotate while swinging
        rb.freezeRotation = true;
        isSwinging = true;
    }

    void PendulumSwing()
    {
        float swingAngle = Mathf.Sin(Time.time * pendulumSpeed) * swingAmplitude;  // Calculate swing angle
        currentBlock.transform.position = new Vector2(spawnPosX + swingAngle, spawnPosY);  // Apply swinging motion
    }

    void ReleaseBlock()
    {
        Rigidbody2D rb = currentBlock.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;  // Enable gravity to drop the block
        rb.freezeRotation = false;  // Allow block to rotate freely after release
        rb.angularDrag = 0.05f;
        isSwinging = false;  // Stop the pendulum swing

        // Start the delayed spawning of the next block
        Invoke("SpawnNextBlock", spawnDelay);
    }

    // Method to delay the next block spawn
    void SpawnNextBlock()
    {
        currentBlock = null;  // Mark the current block as released
        SpawnBlock();  // Spawn the next block after delay
    }
}
