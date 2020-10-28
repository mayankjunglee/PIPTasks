using UnityEngine;
public abstract class AbsPopUp:MonoBehaviour
{
    public abstract void OpenPopUp(PopUpMaster master);
    public abstract void ClosePopUP();
    public abstract void FillPopUpData();
    private void OnEnable() {
        FillPopUpData();
    }
}