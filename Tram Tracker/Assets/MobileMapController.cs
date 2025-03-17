using UnityEngine;

public class MobileMapController : MonoBehaviour
{
    [Header("Assign your Grass Parent here")]
    public Transform grassParent; // Parent of all your grass/ground tiles

    [Header("Camera Movement Settings")]
    public float dragSpeed = 0.1f; // Adjust for faster/slower drag
    public float edgePadding = 10f; // Prevent camera from hitting the very edge

    private Vector2 minBounds;
    private Vector2 maxBounds;

    private Vector3 dragOrigin;

    void Start()
    {
        CalculateCombinedBounds();
    }

    void Update()
    {
        HandleTouchDrag();
    }

    /// <summary>
    /// Calculates the combined bounds of all grass tiles under the grassParent.
    /// This determines the min and max X/Z boundaries of the map.
    /// </summary>
    void CalculateCombinedBounds()
    {
        Renderer[] renderers = grassParent.GetComponentsInChildren<Renderer>();

        if (renderers.Length == 0)
        {
            Debug.LogWarning("No renderers found in grassParent!");
            return;
        }

        // Start with the first renderer's bounds
        Bounds combinedBounds = renderers[0].bounds;

        // Expand the bounds to include all child renderers
        foreach (Renderer renderer in renderers)
        {
            combinedBounds.Encapsulate(renderer.bounds);
        }

        // Set min and max bounds (X and Z axes)
        minBounds = new Vector2(combinedBounds.min.x, combinedBounds.min.z);
        maxBounds = new Vector2(combinedBounds.max.x, combinedBounds.max.z);

        Debug.Log($"Calculated Bounds: Min {minBounds}, Max {maxBounds}");
    }

    /// <summary>
    /// Handles touch dragging to move the camera within the defined map bounds.
    /// </summary>
    void HandleTouchDrag()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Where the drag starts (world point)
                dragOrigin = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.y));
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 currentPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.y));

                Vector3 difference = dragOrigin - currentPos;

                // Calculate new camera position
                Vector3 newPosition = Camera.main.transform.position + new Vector3(difference.x, 0, difference.z) * dragSpeed;

                // Clamp new position with edge padding applied
                newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x + edgePadding, maxBounds.x - edgePadding);
                newPosition.z = Mathf.Clamp(newPosition.z, minBounds.y + edgePadding, maxBounds.y - edgePadding);

                // Apply the position to the camera
                Camera.main.transform.position = newPosition;
            }
        }
    }

    /// <summary>
    /// Optional helper: visualize the calculated bounds in Scene view (for debugging)
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (grassParent != null)
        {
            Gizmos.color = Color.green;

            Vector3 center = new Vector3(
                (minBounds.x + maxBounds.x) / 2,
                grassParent.position.y,
                (minBounds.y + maxBounds.y) / 2
            );

            Vector3 size = new Vector3(
                (maxBounds.x - minBounds.x),
                0.1f,
                (maxBounds.y - minBounds.y)
            );

            Gizmos.DrawWireCube(center, size);
        }
    }
}
