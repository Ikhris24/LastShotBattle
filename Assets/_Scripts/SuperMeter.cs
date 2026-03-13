using UnityEngine;
using UnityEngine.UI;

public class SuperMeter : MonoBehaviour
{
    [Header("Meter Settings")]
    public float maxMeter = 100f;
    public float currentMeter = 0f;

    [Header("UI")]
    public Image meterFill;

    [Header("Debug")]
    public KeyCode debugFillKey = KeyCode.M;

    void Start()
    {
        UpdateMeter();
    }

    void Update()
    {
        // Debug key to fill the meter
        if (Input.GetKeyDown(debugFillKey))
        {
            SetMeterToMax();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            AddMeter(10f);
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
        Debug.Log("Super meter filled!");
    }

    void UpdateMeter()
    {
        if (meterFill != null)
        {
            meterFill.fillAmount = currentMeter / maxMeter;
        }
    }
}