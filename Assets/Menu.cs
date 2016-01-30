using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Menu : MonoBehaviour {

    private static Menu s_instance;
    public static Menu Manager {
        get {
            if (s_instance == null)
                s_instance = FindObjectOfType(typeof(Menu)) as Menu;

            if (s_instance == null) {
                GameObject obj = new GameObject("Menu");
                s_instance = obj.AddComponent(typeof(Menu)) as Menu;
            }
            return s_instance;
        }
    }

    public void MainMenuStart() {
        NetworkManager.singleton.StartHost();
    }
    public void MainMenuJoin() {
        NetworkManager.singleton.StartClient();
    }

    public void MainMenuPlay() {
        Networking.Manager.DebugPlay();
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void SetFullscreen() {
        Screen.SetResolution(Screen.resolutions[Screen.resolutions.Length - 1].width, Screen.resolutions[Screen.resolutions.Length - 1].height, true);
    }

    public void SetResolution(int _resid) {
        Debug.Log(_resid + ": " + Screen.resolutions[_resid].ToString());
    }

    public void IncreaseQuality() {
        QualitySettings.IncreaseLevel();
    }

    public void DecreaseQuality() {
        QualitySettings.DecreaseLevel();
    }

    public void SetVSyncCount(int _count) {
        if (!(_count >= 1 && _count <= 3))
            return;
        QualitySettings.vSyncCount = _count;
    }

}
