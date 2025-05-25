using UnityEngine;

public class GpuMiner : MonoBehaviour
{
    [Header("Монет в секунду")]
    public float miningRate = 3f;

    private float accumulator = 0f;

    void Update()
    {
        accumulator += miningRate * Time.deltaTime;
        if (accumulator >= 1f)
        {
            int toAdd = Mathf.FloorToInt(accumulator);
            accumulator -= toAdd;
            CurrencyManager.Instance.AddCoins(toAdd);
            Debug.Log($"{name} добыл {toAdd} монет");
        }
    }
}
