using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private InnerBalance innerBalance;
    private Renderer playerRenderer;
    private Terrain terrain;

    private void Start()
    {
        innerBalance = GetComponent<InnerBalance>();
        playerRenderer = GetComponent<Renderer>();
        terrain = FindObjectOfType<Terrain>();
        if (innerBalance == null)
        {
            Debug.LogError("InnerBalance component not found on the Player!");
        }
        if (playerRenderer == null)
        {
            Debug.LogError("Renderer component not found on the Player!");
        }
        if (terrain == null)
        {
            Debug.LogError("Terrain not found in the scene!");
        }
    }

    private void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Keep player on terrain surface
        if (terrain != null)
        {
            Vector3 position = transform.position;
            position.y = terrain.SampleHeight(position) + 1f; // 1f is an offset to keep player slightly above ground
            transform.position = position;
        }

        // Balance changes
        if (Input.GetKeyDown(KeyCode.Q))
        {
            innerBalance.UpdateBalance(0.2f, -0.2f, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            innerBalance.UpdateBalance(-0.2f, 0.2f, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            innerBalance.UpdateBalance(0, 0, 0.2f, -0.2f);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            innerBalance.UpdateBalance(0, 0, -0.2f, 0.2f);
        }

        UpdatePlayerColor();
    }

    private void UpdatePlayerColor()
    {
        if (playerRenderer != null && innerBalance != null)
        {
            Color newColor = new Color(
                innerBalance.Fire,
                innerBalance.Earth,
                innerBalance.Water,
                1f
            );
            playerRenderer.material.color = newColor;
        }
    }
}