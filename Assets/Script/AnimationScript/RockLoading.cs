using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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