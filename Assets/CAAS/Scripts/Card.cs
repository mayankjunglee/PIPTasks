using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Card : MonoBehaviour
{
    public CardStatus pCardStatus;
    private Vector2 mOffset;
    public int pGroupNo{get;set;}
    private CardManager mCardManager;
    private Camera mMain;

    public int pZOrder{get;set;}
    private void Awake() {
        pCardStatus = CardStatus.DOWN;
        mMain = Camera.main;
    }
    public void SetData(int groupNo,CardManager cm,int renderOrder)
    {
        pGroupNo = groupNo;
        mCardManager = cm;
        SetZOrder(renderOrder);
    }
    public void SetZOrder(int renderOrder)
    {
        pZOrder = renderOrder;
    }
    private void OnMouseDown() {
        Input.multiTouchEnabled = false;
        ToggleCardStatus();
        Vector2 point = mMain.ScreenToWorldPoint(Input.mousePosition);
        
        mOffset = new Vector2(transform.position.x - point.x,transform.position.y-point.y);
        mCardManager.OnCardMouseDown(this);
    }
    private void OnMouseUp() {
        Vector2 point = mMain.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(point.x+mOffset.x,point.y+mOffset.y,pZOrder);
        mCardManager.OnCardMouseUp(this);
        Input.multiTouchEnabled = true;
    }
    private void OnMouseDrag() {
        Vector2 point = mMain.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(point.x+mOffset.x,point.y+mOffset.y,pZOrder);
        mCardManager.OnCardMouseDrag(this);
    }

    private void ToggleCardStatus()
    {
        pCardStatus = pCardStatus==CardStatus.DOWN?CardStatus.UP:CardStatus.DOWN;
    }
}
