using UnityEngine;
using System;

public class InnerBalance : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float fire = 0.25f;
    [SerializeField, Range(0f, 1f)] private float water = 0.25f;
    [SerializeField, Range(0f, 1f)] private float earth = 0.25f;
    [SerializeField, Range(0f, 1f)] private float air = 0.25f;

    public event Action OnBalanceChanged;

    public float Fire { get => fire; }
    public float Water { get => water; }
    public float Earth { get => earth; }
    public float Air { get => air; }

    public void UpdateBalance(float fireChange, float waterChange, float earthChange, float airChange)
    {
        fire = Mathf.Clamp01(fire + fireChange);
        water = Mathf.Clamp01(water + waterChange);
        earth = Mathf.Clamp01(earth + earthChange);
        air = Mathf.Clamp01(air + airChange);

        NormalizeBalance();
        OnBalanceChanged?.Invoke();
        Debug.Log($"Balance updated: Fire={fire}, Water={water}, Earth={earth}, Air={air}");
    }

    private void NormalizeBalance()
    {
        float total = fire + water + earth + air;
        fire /= total;
        water /= total;
        earth /= total;
        air /= total;
    }
}