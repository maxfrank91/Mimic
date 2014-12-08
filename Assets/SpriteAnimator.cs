using UnityEngine;
using System.Collections;

public class SpriteAnimator : MonoBehaviour 
{
    public bool onAwake = false;
    public bool destroyOnFinish = false;

    private string spriteString = "";
    private int count = 0;
    private int maxCount = 0;

	// Use this for initialization
	void Awake ()
    {
        string n = GetComponent<SpriteRenderer>().sprite.name;
        spriteString = n.Substring(0, n.IndexOf('_'));
        maxCount = Resources.LoadAll<Sprite>(spriteString).Length - 1;

        if (onAwake)
            StartCoroutine(PlayAnimation());
	}

    public IEnumerator PlayAnimation()
    {
        while (count < maxCount)
        {
            count++;
            GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(spriteString)[count];
            yield return new WaitForFixedUpdate();
        }
        if (destroyOnFinish)
            Destroy(gameObject);
    }
}
