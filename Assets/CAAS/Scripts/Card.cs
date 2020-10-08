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
    #if UNITY_EDITOR
    private void OnMouseDown() {
        Debug.Log("OnMouseDown");
        Input.multiTouchEnabled = false;
        Vector2 point = mMain.ScreenToWorldPoint(Input.mousePosition);
        OnCustomMouseDown(point);
    }
    private void OnMouseUp() {
        Vector2 point = mMain.ScreenToWorldPoint(Input.mousePosition);
        OnCustomMouseUp(point);
        Input.multiTouchEnabled = true;
    }
    private void OnMouseDrag() {
        Vector2 point = mMain.ScreenToWorldPoint(Input.mousePosition);
        OnCustomMouseDrag(point);
    }
    #endif
    private void ToggleCardStatus()
    {
        pCardStatus = pCardStatus==CardStatus.DOWN?CardStatus.UP:CardStatus.DOWN;
    }

    public void OnCustomMouseDown(Vector2 point)
    {
        ToggleCardStatus();
        mOffset = new Vector2(transform.position.x - point.x,transform.position.y-point.y);
        mCardManager.OnCardMouseDown(this);
    }
    public void OnCustomMouseDrag(Vector2 point)
    {
        transform.position = new Vector3(point.x+mOffset.x,point.y+mOffset.y,pZOrder);
        mCardManager.OnCardMouseDrag(this);
    }
    public void OnCustomMouseUp(Vector2 point)
    {
        transform.position = new Vector3(point.x+mOffset.x,point.y+mOffset.y,pZOrder);
        mCardManager.OnCardMouseUp(this);
    }
}
