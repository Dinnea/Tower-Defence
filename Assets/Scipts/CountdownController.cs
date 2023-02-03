using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Contains countdown to wave.
/// </summary>
public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    [SerializeField] private TextMeshProUGUI _countdownDisplay;

    IEnumerator CountdownToWave()
    {
        while (countdownTime > 0)
        {
            _countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
    }
}
