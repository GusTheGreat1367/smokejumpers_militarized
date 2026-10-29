using UnityEngine;

public class bulletTracer : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("The direction line the shader should move along.")]
    public Vector2 moveDirection = new Vector2(1f, 0.5f); 
    
    [Tooltip("How fast the texture moves along the line.")]
    public float speed = 2.0f;

    private Material targetMaterial;

    void Start()
    {
        // Get the renderer component and grab its material
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Note: Using .material creates a unique local instance so 
            // it doesn't affect other objects using the same material file.
            targetMaterial = renderer.material;
        }
        else
        {
            Debug.LogError("No Renderer found on this GameObject!");
        }
    }

    void Update()
    {
        if (targetMaterial != null)
        {
            // Normalize the direction vector so the speed remains consistent 
            // no matter how large the X or Y values are set in the Inspector.
            Vector2 normalizedDirection = moveDirection.normalized;

            // Convert Vector2 to a Vector4 (matching the shader's property type)
            Vector4 shaderVector = new Vector4(normalizedDirection.x, normalizedDirection.y, 0, 0);

            // Pass the data to the shader using the exact string names from the shader Properties block
            targetMaterial.SetVector("_MoveDirection", shaderVector);
            targetMaterial.SetFloat("_Speed", speed);
        }
    }
}
