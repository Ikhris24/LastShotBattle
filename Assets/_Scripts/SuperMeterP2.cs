using UnityEngine;
using UnityEngine.UI;

public class SuperMeterP2 : MonoBehaviour
{
    [Header("Meter Settings")]
    public float maxMeter = 100f;
    public float currentMeter = 0f;

    [Header("UI")]
    public Image meterFill;

    [Header("Debug")]
    public KeyCode debugFillKey = KeyCode.K;
    public float debugAddAmount = 10f;

    void Start()
    {
        UpdateMeter();
    }

    void Update()
    {
        // Fill meter instantly
        if (Input.GetKeyDown(debugFillKey))
        {
            SetMeterToMax();
        }

        // Add meter slowly for testing
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddMeter(debugAddAmount);
        }
    }

    public void AddMeter(float amount)
    {
        currentMeter += amount;
        currentMeter = Mathf.Clamp(currentMeter, 0f, maxMeter);
        UpdateMeter();
    }

    public void SetMeterToMax()
    {
        currentMeter = maxMeter;
        UpdateMeter();
        Debug.Log(gameObject.name + " Super Meter FULL");
    }

    void UpdateMeter()
    {
        if (meterFill != null)
        {
            meterFill.fillAmount = currentMeter / maxMeter;
        }
    }
}