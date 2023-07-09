using UnityEngine;
using UnityEngine.UI;

public class PowerOnBatteryLevel : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxEnergy(float energyLevel)
    {
        slider.maxValue = energyLevel;
        slider.value = energyLevel;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetEnergyOnDisplay(float energyLevel)
    {
        slider.value = energyLevel;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
