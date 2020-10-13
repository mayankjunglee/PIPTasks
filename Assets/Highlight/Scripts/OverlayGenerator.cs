using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OverlayGenerator : MonoBehaviour
{
    [SerializeField] private GameObject mImgObj;
    [SerializeField] private List<RectTransform> mObjToHighlight;
    [SerializeField] private RectTransform mCanvas;
    [SerializeField] private Transform mParent;


    private void Start() {
        List<Rectangle> rects = RectangleCalculator.GetCoverRects(mCanvas,mObjToHighlight);

        for(int i =0;i<rects.Count;i++)
        {
            Rectangle rect = rects[i];
            var obj = Instantiate<GameObject>(mImgObj);
            obj.transform.SetParent(mParent.transform,false);
            obj.transform.localScale = Vector3.one;
            RectTransform rTrans = obj.GetComponent<RectTransform>();
            rTrans.localPosition = GetPositionFromRectangle(rect);
            rTrans.sizeDelta = GetSizeOfARecangle(rect);

        }
    }
    private Vector2 GetSizeOfARecangle(Rectangle rect)
    {
        return new Vector2(rect.v11.x-rect.v00.x,rect.v11.y-rect.v00.y);
    }
    private Vector3 GetPositionFromRectangle(Rectangle rect)
    {
        Vector2 size = GetSizeOfARecangle(rect);
        return new Vector3(rect.v00.x+size.x/2,rect.v00.y+size.y/2,0);
    }
    public void ClickOnBackButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
