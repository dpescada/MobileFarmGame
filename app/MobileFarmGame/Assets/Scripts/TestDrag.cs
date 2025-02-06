using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TestDrag : MonoBehaviour
{
    public ScrollRect scrollRect;
    public int num = 0;
    public Image cropImage;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
    }

    public void OnPress()
    {
        scrollRect.enabled = false;
        print(num + " pressed");
        PlayerControls.touchingScreen = true;
        cropImage.enabled = false;
    }

    public void OnRelease()
    {
        scrollRect.enabled = true;
        print(num + " pressed");
        PlayerControls.touchingScreen = false;
        cropImage.enabled = false;
    }

    public void OnDrag()
    {
        if(PlayerControls.touchingScreen)
        {
            print(num + " pressed");
            cropImage.enabled = false;
        }
    }
}
