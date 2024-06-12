using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FadeIn : MonoBehaviour
{

    public UnityEngine.UI.Image image_this;
    public UnityEngine.UI.Text text;
    public float velocity;
    public float start_time;

    public static Color color_a = new Color(0f, 0f, 0f);
    public static Color color_b = new Color(255f, 255f, 255f);

    // Start is called before the first frame update
    void Start()
    {
        start_time += Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        image_this.color = FadeIn.color_a;
        text.color = color_b;

        if (FadeIn.color_a.a <= 0)
        {
            FadeIn.color_a.a = 0;

            if (FadeIn.color_b.a <= 0)
            {
                FadeIn.color_b.a = 0;
                image_this.raycastTarget = false;
                text.raycastTarget = false;
            }
        }

        if (start_time < Time.time && FadeIn.color_a.a != 0 && FadeIn.color_b.a != 0)
        {

            FadeIn.color_a = new Color(image_this.color.r, image_this.color.g, image_this.color.b, (image_this.color.a - (velocity * Time.deltaTime)));


            if (image_this.color.a < 35)
            {
                FadeIn.color_b = new Color(text.color.r, text.color.g, text.color.b, (text.color.a - (velocity * Time.deltaTime)));
            }
        }
    }
}
