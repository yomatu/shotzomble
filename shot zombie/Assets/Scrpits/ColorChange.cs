using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{
    public Text text;
    public Color[] colors; 
    public int index; 

    void Start()
    {
        text = GetComponent<Text>(); 
        colors = new Color[] { Color.red, Color.green, Color.blue };
        index = 0; 
    }

    void Update()
    {
        //Time.unscaledTime Time.unscaledDeltaTime is can stop
        //about Time.timeScale
        float t = Mathf.PingPong(Time.unscaledTime, colors.Length);
  
        index = Mathf.FloorToInt(t);
      
        int nextIndex = (index + 1) % colors.Length;
 
        t = t - index;
     
        Color color = Color.Lerp(colors[index], colors[nextIndex], t);
 
        text.color = color;
    }
}
