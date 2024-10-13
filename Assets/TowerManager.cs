using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public int score = 0;                         // Current score
    public Transform lastBlock;                    // Reference to the last placed block
    public float alignmentTolerance = 1.0f;       // Increased tolerance for block alignment
    public GameObject alignmentIndicatorPrefab;     // Prefab for visual alignment feedback

    private int perfectAlignmentCount = 0;        // Counter for perfectly aligned blocks
    public int requiredPerfectAlignments = 3;     // Number of perfect alignments required to stabilize the tower
    public GameObject completionIndicator;         // Prefab for visual feedback when the tower is completed

    private void Start()
    {
        // Optionally, initialize other components or state if necessary
    }

    public void OnBlockLanded(GameObject block)
    {
        // Check alignment with the last block
        if (lastBlock != null)
        {
            float misalignment = Mathf.Abs(block.transform.position.x - lastBlock.position.x);

            if (misalignment > alignmentTolerance)  // Check if the new block is misaligned
            {
                Debug.Log("Block is misaligned. Tower stability decreases.");
                // Reset perfect alignment count on misalignment
                perfectAlignmentCount = 0;
                ShowAlignmentIndicator(block, Color.red);
            }
            else
            {
                Debug.Log("Block is perfectly aligned! Tower stability increases!");
                perfectAlignmentCount++;
                ShowAlignmentIndicator(block, Color.green);

                // Check if the required number of perfect alignments has been reached
                if (perfectAlignmentCount >= requiredPerfectAlignments)
                {
                    CompleteTower(block);
                }
            }
        }

        // Increase the score when a block lands
        score++;
        Debug.Log("Score: " + score);

        // Set the current block as the lastBlock for future alignment checks
        lastBlock = block.transform;

        // Update the camera target to the new last block
        Camera.main.GetComponent<CameraFollow>().target = lastBlock;

        // Optionally, you could also manage the visibility or destruction of the current block here
    }


    private void CompleteTower(GameObject block)
    {
        Debug.Log("Tower is completed and stabilized!");
        ShowCompletionIndicator(block);

        // Disable any further block stacking or release logic here
        // You might also want to implement game end logic or reset logic.
    }

    private void ShowAlignmentIndicator(GameObject block, Color color)
    {
        // Instantiate an alignment indicator at the block's position
        GameObject indicator = Instantiate(alignmentIndicatorPrefab, block.transform.position, Quaternion.identity);
        indicator.GetComponent<Renderer>().material.color = color;

        // Optionally, destroy the indicator after a short duration
        Destroy(indicator, 1f);
    }

    private void ShowCompletionIndicator(GameObject block)
    {
        // Instantiate a completion indicator at the block's position
        Instantiate(completionIndicator, block.transform.position, Quaternion.identity);

        // Optionally, you could display a message or trigger a celebration animation here
    }
}
