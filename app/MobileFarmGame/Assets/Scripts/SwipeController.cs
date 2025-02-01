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

    [SerializeField] RectTransform shopRect;
    [SerializeField] RectTransform caveRect;

    void Awake()
    {
        currentFarm = 1;
        targetPos = scrollViewRect.localPosition;
        dragThreshould = Screen.width / 3;
    }

    public void SwipeLeft()
    {
        if(currentFarm < totalFarms && inFarm)
        {
            currentFarm++;
            targetPos += horizontalStep;
            shopRect.position -= new Vector3(-1180, 0, 0);
            caveRect.position -= new Vector3(-1180, 0, 0);
            MovePage();
        }
    }

    public void SwipeRight()
    {
        if(currentFarm > 1 && inFarm)
        {
            currentFarm--;
            targetPos -= horizontalStep;
            shopRect.position -= new Vector3(1180, 0, 0);
            caveRect.position -= new Vector3(1180, 0, 0);
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
