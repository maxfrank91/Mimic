using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ImageManager : MonoBehaviour 
{
    const float TIME = 1.5f;
    
    public List<Sprite> textures;
    public SpriteRenderer sprite;
    public Image countdown;

    float timer = 0;

    
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

    public void showSymbol(int index, bool video = false)
    {
        sprite.color = Color.white;
        sprite.sprite = textures[index];
        if (!video) 
            StartCoroutine(Black(TIME));
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
}
