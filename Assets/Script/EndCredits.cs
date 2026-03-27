using UnityEngine;
using UnityEngine.UI;

public class EndCredits : MonoBehaviour
{
    public float scrollSpeed =30f;

    private RectTransform RectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
        
    }
}
