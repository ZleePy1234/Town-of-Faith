using UnityEngine;

public class SpriteSheetAnimator : MonoBehaviour
{
    public Material material;
    public int tilesX = 4; // Columns
    public int tilesY = 4; // Rows
    public float framesPerSecond = 10f;

    private int totalFrames;
    private float frameTime;
    private int currentFrame;

    void Start()
    {
        totalFrames = tilesX * tilesY;
        frameTime = 1f / framesPerSecond;

        // Set tiling
        material.mainTextureScale = new Vector2(1f / tilesX, 1f / tilesY);
    }

    void Update()
    {
        int index = (int)(Time.time * framesPerSecond) % totalFrames;
        if (index != currentFrame)
        {
            currentFrame = index;

            int u = index % tilesX;
            int v = index / tilesX;

            // Flip vertically if needed
            material.mainTextureOffset = new Vector2((float)u / tilesX, 1f - ((float)(v + 1) / tilesY));
        }
    }
}