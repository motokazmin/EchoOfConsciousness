using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public TerrainGenerator terrainGenerator;
    public Material terrainMaterial;
    public Light directionalLight;

    private InnerBalance playerBalance;

    private void Start()
    {
        playerBalance = FindObjectOfType<InnerBalance>();
        if (playerBalance == null)
        {
            Debug.LogError("Player with InnerBalance not found!");
            return;
        }

        playerBalance.OnBalanceChanged += UpdateWorld;
    }

    private void UpdateWorld()
    {
        UpdateTerrainColor();
        UpdateLighting();
    }

    private void UpdateTerrainColor()
    {
        if (terrainMaterial != null)
        {
            Color baseColor = new Color(
                Mathf.Lerp(0.2f, 0.8f, playerBalance.Fire),
                Mathf.Lerp(0.2f, 0.8f, playerBalance.Earth),
                Mathf.Lerp(0.2f, 0.8f, playerBalance.Water)
            );
            terrainMaterial.color = baseColor;
        }
    }

    private void UpdateLighting()
    {
        if (directionalLight != null)
        {
            float intensity = Mathf.Lerp(0.5f, 1.5f, playerBalance.Air);
            directionalLight.intensity = intensity;

            Color lightColor = Color.Lerp(Color.white, new Color(1f, 0.8f, 0.6f), playerBalance.Fire);
            directionalLight.color = lightColor;
        }
    }
}