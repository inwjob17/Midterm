using UnityEngine;
using TMPro;

public class TlmerScipt : MonoBehaviour
{
   public float time;
   public TextMeshProUGUI timerText;

    void Update()
    {
        time += Time.deltaTime;
        timerText.text = Mathf.Floor(time).ToString();

    }
}
