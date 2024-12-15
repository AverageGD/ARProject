using UnityEngine;

public class ReactionPrefab : MonoBehaviour
{
    public short id;

    public short electronCount;

    [SerializeField] private GameObject _torus;

    private void Awake()
    {
        float radius = 3 * _torus.GetComponent<ParticleSystem>().shape.radius;

        for (int i = 1; i <= electronCount; i++)
        {
            GameObject gb = Instantiate(_torus, gameObject.transform);
            ParticleSystem particleSystem = gb.GetComponent<ParticleSystem>();
            var shape = particleSystem.shape;
            shape.radius = radius * i;
        }
    }
}
