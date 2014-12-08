using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ImageManager : MonoBehaviour 
{
    const float TIME = 1.5f;
    
    public List<Sprite> textures;
    public SpriteRenderer sprite;
    public List<SpriteRenderer> sprites;
    public Image countdown;
    public Text Phase;

    Color a1 = new Color (1, 1, 1, 1);
    Color a0 = new Color (0, 0, 0, 0);

    float timer = 0;

    public void SetPhase(string text)
    {
        if (Phase.text != text)
        {
            Phase.text = text;
            showPhase();
        }
    }

    public void reduceBar(float value, float max)
    {
        Vector2 size = countdown.rectTransform.sizeDelta;
        size.x = 200 * (1 - (value / max));
        countdown.rectTransform.sizeDelta = size;
        //countdown.flexibleWidth = 200 * (1 - (value / max));
    }

    public void showBar(bool value)
    {
        countdown.enabled = value;
    }

    public void showSymbol(int index, bool blend = true)
    {
        if (sprites[index] == null)
        {
            sprite.color = Color.white;
            sprite.sprite = textures[index];
            if (blend)
                StartCoroutine(Black(TIME));
        }
        else
        {
            GameObject.Instantiate(sprites[index]);
        }
    }

    IEnumerator Black(float time)
    {
        float t = 0;
        while (t != time)
        {
            t += Time.fixedDeltaTime;
            sprite.color = Color.Lerp(Color.white, Color.black, t / time);
            yield return new WaitForFixedUpdate();
        }
        sprite.sprite = null;
    }

    public void BlackScreen()
    {
        sprite.color = Color.black;
    }

    void showPhase()
    {
        Phase.color = a1;
        StartCoroutine(FadeText(TIME));
    }

    IEnumerator FadeText(float time)
    {
        float t = 0;
        while (t != time)
        {
            t += Time.fixedDeltaTime;
            Phase.color = Color.Lerp(a1, a0, t / time);
            yield return new WaitForFixedUpdate();
        }
    }
}
