using System;
using UnityEngine;
using UnityEngine.UI;
public class JsonPopUP :AbsPopUp
{
    public Action OnOkButtonClickAction;
    public Action OnCancelButtonClickAction;
    [SerializeField] private Text m_Header;
    [SerializeField] private Text m_Body;
    [SerializeField] private TextAsset m_jsonFile;
    PopUpMaster master1;
    public override void OpenPopUp(PopUpMaster master)
    {
        //Can trigger some animation here
        Debug.Log("open "+gameObject.name);
        this.master1 = master;
        gameObject.SetActive(true);
    }
    public override void ClosePopUP()
    {
        //Destroy(gameObject);
        Debug.Log("close "+gameObject.name);
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
        var data = JsonUtility.FromJson<PopUpJsonClass>(m_jsonFile.text);
        m_Header.text = data.header;
        m_Body.text = data.body;
    }

    [Serializable]
    public class PopUpJsonClass
    {
        public string header;
        public string body;
    }
}
