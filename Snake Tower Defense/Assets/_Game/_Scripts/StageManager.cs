using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    float _timer = 0f;
    public TextMeshProUGUI TimeCounterText;

    private void Update()
    {
        _timer += Time.deltaTime;
        float seconds = (_timer % 60);

        TimeCounterText.text = seconds.ToString("F2"); //F1, F2, F3 etc can be used to restrict floats to decimals
    }
}
