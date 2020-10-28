using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMaster : MonoBehaviour
{


    [SerializeField] Transform mPopUPHolder;

    [SerializeField] GameObject background;
    [SerializeField] GameObject clearAllButton;

    Stack<AbsPopUp> popUps;
    private void Awake() {
        popUps = new Stack<AbsPopUp>();
    }
    void ActivateBG(bool activate)
    {
        background.SetActive(activate);
        clearAllButton.SetActive(activate);
    }

    public void ClearAllPopUP()
    {
        if(popUps.Count==0) return;
        AbsPopUp popUp = popUps.Pop();
        popUp.ClosePopUP();
        popUps.Clear();
    }
    public void OpenPopUp(AbsPopUp popup)
    {
        if(popUps.Count==0)
        {
            popup.OpenPopUp(this);
            popUps.Push(popup);
        }
        else
        {
            if(popUps.Peek().gameObject.Equals(popup.gameObject))
            {
                return;
            }
            popUps.Peek().ClosePopUP();
            popup.OpenPopUp(this);
            popUps.Push(popup);

        }
    }
    public void CloseCurrentPopUp()
    {
        if(popUps.Count==0) return;
        AbsPopUp popUp = popUps.Pop();
        popUp.ClosePopUP();
        OpenCurrentPopUp();
    }
    public void OpenCurrentPopUp()
    {
        if(popUps.Count==0) return;
        AbsPopUp openPopUp = popUps.Peek();
        openPopUp.OpenPopUp(this);
    }
    public void BackButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }
}
