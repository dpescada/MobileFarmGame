using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] bool inFarm = true;
    [SerializeField] bool inShop = false;
    [SerializeField] bool inCave = false;

    [SerializeField] int totalFarms;
    [SerializeField] int currentFarm;
    [SerializeField] Vector3 targetPos;
    [SerializeField] Vector3 horizontalStep;
    [SerializeField] Vector3 verticalStep;
    [SerializeField] RectTransform scrollViewRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    float dragThreshould;
    LTDescr tween;

    [SerializeField] RectTransform farmShopCaveRect, farm1Rect, farm2Rect, farm3Rect, farm4Rect, farm5Rect, shopRect, caveRect;

    void Awake()
    {
        currentFarm = 1;
        targetPos = scrollViewRect.localPosition;
        dragThreshould = Screen.width / 3;
        horizontalStep = new Vector3(-Screen.width, 0, 0);
        verticalStep = new Vector3(0, -Screen.height, 0);

        farmShopCaveRect.sizeDelta = new Vector2(Screen.width, Screen.height);
        farm1Rect.sizeDelta = new Vector2(Screen.width, Screen.height);
        farm2Rect.sizeDelta = new Vector2(Screen.width, Screen.height);
        farm3Rect.sizeDelta = new Vector2(Screen.width, Screen.height);
        farm4Rect.sizeDelta = new Vector2(Screen.width, Screen.height);
        farm5Rect.sizeDelta = new Vector2(Screen.width, Screen.height);
        shopRect.sizeDelta = new Vector2(Screen.width, Screen.height);
        caveRect.sizeDelta = new Vector2(Screen.width, Screen.height);
        shopRect.position = new Vector3(farm1Rect.position.x, farm1Rect.position.y - Screen.height, 0);
        caveRect.position = new Vector3(farm1Rect.position.x, farm1Rect.position.y - Screen.height * 2, 0);
    }

    public void SwipeLeft()
    {
        if(currentFarm < totalFarms && inFarm)
        {
            currentFarm++;
            targetPos += horizontalStep;
            shopRect.position -= new Vector3(-Screen.width, 0, 0);
            caveRect.position -= new Vector3(-Screen.width, 0, 0);
            MovePage();
        }
    }

    public void SwipeRight()
    {
        if(currentFarm > 1 && inFarm)
        {
            currentFarm--;
            targetPos -= horizontalStep;
            shopRect.position -= new Vector3(Screen.width, 0, 0);
            caveRect.position -= new Vector3(Screen.width, 0, 0);
            MovePage();
        }
    }

    public void SwipeDown()
    {
        if(!inFarm)
        {
            if(inCave)
            {
                inCave = false;
                inShop = true;
            }
            else if(inShop)
            {
                inShop = false;
                inFarm = true;
            }

            targetPos += verticalStep;
            MovePage();
        }
    }

    public void SwipeUp()
    {
        if(!inCave)
        {
            if(inFarm)
            {
                inFarm = false;
                inShop = true;
            }
            else if(inShop)
            {
                inShop = false;
                inCave = true;
            }

            targetPos -= verticalStep;
            MovePage();
        }
    }

    void MovePage()
    {
        if(tween != null)
            tween.reset();
        tween = scrollViewRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if(eventData.position.x > eventData.pressPosition.x) SwipeRight();
            else SwipeLeft();
        }
        else
        {
            MovePage();    
        }
        
        if(Mathf.Abs(eventData.position.y - eventData.pressPosition.y) > dragThreshould)
        {
            if(eventData.position.y > eventData.pressPosition.y) SwipeUp();
            else SwipeDown();
        }
        else
        {
            MovePage();    
        }
    }
}
