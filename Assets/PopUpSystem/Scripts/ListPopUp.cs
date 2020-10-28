using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class ListPopUp :AbsPopUp
{
    public Action OnOkButtonClickAction;
    public Action OnCancelButtonClickAction;
    [SerializeField] private Text m_Header;
    [SerializeField] private Text m_Body;
    [SerializeField] List<string> m_SomeInfo;
    private PopUpMaster master1;
    public override void OpenPopUp(PopUpMaster master)
    {
        //Can trigger some animation here
        this.master1 = master;
        gameObject.SetActive(true);
    }
    public override void ClosePopUP()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
    public void OKButtonClicked()
    {
        // OnOkButtonClickAction?.Invoke();
        // ClosePopUP();
         master1.CloseCurrentPopUp();
    }
    public void CancelButtonClicked()
    {
        // OnCancelButtonClickAction?.Invoke();
        // ClosePopUP();
         master1.CloseCurrentPopUp();
    }
    public void BackButtonClicked()
    {
        // CancelButtonClicked();
         master1.CloseCurrentPopUp();
    }
    public override void FillPopUpData()
    {
        m_Header.text = m_SomeInfo[0];
        m_Body.text = m_SomeInfo[1];
    }


}
