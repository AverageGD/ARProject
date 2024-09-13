
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{


    [SerializeField] private Vector3 _rotationSpeed = new Vector3(0f, 100f, 0f);

    void Update()
    {
        transform.Rotate(_rotationSpeed * Time.deltaTime);
    }
}
