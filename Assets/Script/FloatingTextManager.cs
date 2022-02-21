using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FlaotingText> floatingTexts = new List<FlaotingText>();

    private void Update()
    {
        foreach (FlaotingText txt in floatingTexts)
        {
            txt.UpdateFloatingText();
        }
    }

    public void Show(string message , int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FlaotingText Floattxt = GetFloatingText();

        Floattxt.text.text = message;
        Floattxt.text.fontSize = fontSize;
        Floattxt.text.color = color;
        Floattxt.go.transform.position = Camera.main.WorldToScreenPoint(position);
        Floattxt.motion = motion;
        Floattxt.duration = duration;
        Floattxt.Show();
    }



    private FlaotingText GetFloatingText()
    {
        FlaotingText txt = floatingTexts.Find(t => !t.active);

        if (txt == null)
        {
            txt = new FlaotingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.text = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }
        return txt;
    
    }


}
