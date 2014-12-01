using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ImageManager : MonoBehaviour 
{
    public List<Sprite> textures;
    public SpriteRenderer sprite;
    public Image countdown;

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

    public void showSymbol(int index)
    {
        sprite.sprite = textures[index];
    }
}
