using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script controls the animation of the rocks during the loading screen. 
/// It creates a diagonal wave effect by changing the color of the rock images 
/// in a pattern based on their row and column positions.
/// The animation runs in a loop, continuously updating the colors of the rocks 
/// to create a dynamic and visually appealing loading screen. 
/// The speed of the animation can be adjusted using the speed variable, allowing 
/// for customization of the loading experience. This script adds an engaging visual 
/// element to the loading screen, enhancing the overall user experience while waiting 
/// for the game to load.
/// </summary>
public class RockLoading : MonoBehaviour
{
    public int rows = 6;
    public int cols = 6;
    public float speed = 0.2f;

    public Color normalColor = new Color(0.15f, 0.2f, 0.2f);
    public Color glowColor = new Color(0.3f, 0.9f, 1f);

    private Image[] rocks;
    private int step = 0;

    void Start()
    {
        rocks = GetComponentsInChildren<Image>();
        StartCoroutine(Animate());
    }
    
    // Creates a diagonal wave effect by changing the color of the rock images in a pattern based on their row and column positions.
    IEnumerator Animate()
    {
        while (true)
        {
            for (int i = 0; i < rocks.Length; i++)
            {
                int row = i / cols;
                int col = i % cols;

                // DIAGONAL WAVE LOGIC
                if ((row + col + step) % cols < 3)
                {
                    rocks[i].color = glowColor;
                }
                else
                {
                    rocks[i].color = normalColor;
                }
            }

            step++;
            yield return new WaitForSeconds(speed);
        }
    }
}