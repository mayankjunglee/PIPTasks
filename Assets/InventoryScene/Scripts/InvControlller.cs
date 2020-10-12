using UnityEngine.SceneManagement;
using UnityEngine;

public class InvControlller : MonoBehaviour
{
    [SerializeField] StoreUI mStoreUI;
    [SerializeField] Inventory mInventory;
    private void Awake() {
        OnInventoryOpenClicked();
    }
    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void OnInventoryOpenClicked()
    {
        mStoreUI.gameObject.SetActive(false);
        mInventory.gameObject.SetActive(true);
    }
    
    public void OnStoreOpenClicked()
    {
        mInventory.gameObject.SetActive(false);
        mStoreUI.gameObject.SetActive(true);
    }
}
