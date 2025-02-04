using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TestGrow : MonoBehaviour
{
    public float growTimer = 0;
    public float timeToGrow = 7;
    public RectTransform maskRectTrans;
    bool grown = false;
    public LeanTweenType tweenType;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!grown)
        {
            if(maskRectTrans.sizeDelta.y < 100)
            {
                growTimer += Time.deltaTime;
                maskRectTrans.sizeDelta = new Vector2(maskRectTrans.sizeDelta.x, (growTimer / timeToGrow * 50) + 50);
            }
            else
            {
                grown = true;
                StartCoroutine(Tween());
            }
        }
    }

    IEnumerator Tween()
    {
        while(grown)
        {
            maskRectTrans.LeanMoveLocal(new Vector2(0, 10), 0.5f).setEase(tweenType);
            yield return new WaitForSeconds(0.5f);
            maskRectTrans.LeanMoveLocal(new Vector2(0, -10), 0.5f).setEase(tweenType);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
