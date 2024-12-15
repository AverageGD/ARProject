using UnityEngine;

public class ShakeDetector : MonoBehaviour
{
    public float shakeThreshold = 2.0f; // Threshold to determine if the phone is shaking
    private Vector3 lastAcceleration;  // Previous accelerometer reading
    private Vector3 currentAcceleration; // Current accelerometer reading

    void Start()
    {
        // Initialize the initial accelerometer values
        lastAcceleration = Input.acceleration;
        currentAcceleration = Input.acceleration;
    }

    void Update()
    {
        // Update the current accelerometer reading
        currentAcceleration = Input.acceleration;

        // Calculate the change in acceleration
        Vector3 deltaAcceleration = currentAcceleration - lastAcceleration;

        // Check if the change exceeds the threshold or if the space key is pressed
        if (deltaAcceleration.sqrMagnitude > shakeThreshold * shakeThreshold || Input.GetKeyDown(KeyCode.Space))
        {
            ReactionManager.instance.StopReactions();
            MoleculeManager.instance.DeleteAllMolecules();
        }

        // Update the previous accelerometer reading
        lastAcceleration = currentAcceleration;
    }
}
