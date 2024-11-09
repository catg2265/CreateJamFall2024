using UnityEngine;
using UnityEngine.UI;

public class Moodbar : MonoBehaviour
{
    public Slider moodSlider;
    public Image Gfx;

    public int medThreshold = 70;
    public int lowThreshold = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moodSlider.maxValue = 100; moodSlider.minValue = 0;
        moodSlider.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
        print(moodSlider.value);
        if (moodSlider.value > 0)
        {
            if (moodSlider.value <= lowThreshold)
            {
                Gfx.color = Color.red;
            }
            else if (moodSlider.value <= medThreshold)
            {
                Gfx.color = Color.yellow;
            }
            else
            {
                Gfx.color = Color.green;
            }
        }
        else
        {
            //End the game
        }
        
    }
    public void UpdateMoodbar(int percent)
    {
        moodSlider.value -= percent;
    }
}
