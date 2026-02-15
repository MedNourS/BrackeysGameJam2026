using UnityEngine;
using System.Linq;

public class BaseMenu : MonoBehaviour
{
    [SerializeField] private GameObject rootMenu;
    [SerializeField] private GameObject[] optionMenus;

    // Methods are public so they can be accessed with UnityEvents attached to buttons
    public void BackToMain()
    {
        rootMenu.SetActive(true);
        foreach (var menu in optionMenus) { menu.SetActive(false); }
    }

    // Navigate from main menu to different menus
    public void OpenOptionMenu(GameObject menu)
    {
        rootMenu.SetActive(false);
        if (optionMenus.Contains(menu)) { menu.SetActive(true); }
    }
}