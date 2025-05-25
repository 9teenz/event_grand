using UnityEngine;

public class AutoDelete : MonoBehaviour
{
    [SerializeField] private float lifetime = 3f;

    private void OnEnable()
    {
        Destroy(gameObject, lifetime);
    }
}
