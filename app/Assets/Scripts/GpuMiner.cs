using UnityEngine;

public class GpuMiner : MonoBehaviour
{
    [Header("����� � �������")]
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
            Debug.Log($"{name} ����� {toAdd} �����");
        }
    }
}
