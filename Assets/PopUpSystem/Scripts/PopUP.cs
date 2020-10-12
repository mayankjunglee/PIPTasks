using System;
using UnityEngine;
using UnityEngine.UI;
public class PopUP : MonoBehaviour
{
    public Action OnOkButtonClickAction;
    public Action OnCancelButtonClickAction;
    [SerializeField] private Text m_Header;
    [SerializeField] private Text m_Body;
    
    public void OpenPopUp()
    {
        //Can trigger some animation here
    }
    public void ClosePopUP()
    {
        Destroy(gameObject);
    }
    public void OKButtonClicked()
    {
        OnOkButtonClickAction?.Invoke();
        ClosePopUP();
    }
    public void CancelButtonClicked()
    {
        OnCancelButtonClickAction?.Invoke();
        ClosePopUP();
    }
    public void BackButtonClicked()
    {
        CancelButtonClicked();
    }

    public void SetData(PopUPData data,Action OkButtonClickAction,Action CancelButtonClickAction)
    {
        m_Header.text = data.HeaderText;
        m_Body.text = data.BodyText;
        OnOkButtonClickAction += OkButtonClickAction;
        OnCancelButtonClickAction += CancelButtonClickAction;
    }
}
