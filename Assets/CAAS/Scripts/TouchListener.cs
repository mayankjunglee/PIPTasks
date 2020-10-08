using UnityEngine;

public class TouchListener : MonoBehaviour
{
    Camera mMain;
    Card mCard;
    private void Awake() {
        mMain = Camera.main;
    }
    #if UNITY_ANDROID && !UNITY_EDITOR
    private void Update() 
    {
        if(Input.touchCount>0)
        {
            Touch firstTouch = Input.touches[0];
            switch(firstTouch.phase)
            {
                case TouchPhase.Began:
                mCard = null;
                Vector2 touchPos2D = GetTouchPos(firstTouch);
                RaycastHit2D hit2D = Physics2D.Raycast(touchPos2D,mMain.transform.forward);
                if(hit2D.collider!=null)
                {
                    mCard = hit2D.collider.GetComponent<Card>();
                    if(mCard!=null)
                    {
                       mCard.OnCustomMouseDown(touchPos2D); 
                    }
                }
                break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                if(mCard!=null)
                {
                    mCard.OnCustomMouseDrag(GetTouchPos(firstTouch));
                }
                break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                if(mCard!=null)
                {
                    mCard.OnCustomMouseUp(GetTouchPos(firstTouch));
                }

                mCard = null;
                break;
                
            }
        }    
    }
    private Vector2 GetTouchPos(Touch touch)
    {
        Vector3 touchPos = mMain.ScreenToWorldPoint(touch.position);
        Vector2 touchPos2D = new Vector2(touchPos.x,touchPos.y);
        return touchPos2D;
    }
    #endif
}
