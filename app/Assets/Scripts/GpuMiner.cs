using UnityEngine;

public class GpuMiner : MonoBehaviour
{
    public float miningRate = 3f; 
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            Mine();
            timer = 0f;
        }
    }

    void Mine()
    {
        Debug.Log($"{name} добыл {miningRate} монет");
    }
}
