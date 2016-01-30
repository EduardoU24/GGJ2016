using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GGJ : MonoBehaviour {

    private static GGJ s_instance;
    public static GGJ Manager {
        get {
            if (s_instance == null)
                s_instance = FindObjectOfType(typeof(GGJ)) as GGJ;

            if (s_instance == null) {
                GameObject obj = new GameObject("GGJManager");
                s_instance = obj.AddComponent(typeof(GGJ)) as GGJ;
            }
            return s_instance;
        }
    }


    public Camera mainCam;
    public string menuScene;
    public GameObject bulletSimple;
    public GameObject bulletFire;
    public Text debug;

    void Awake() {
        DontDestroyOnLoad(this);
    }

    void Start() {
        SceneManager.LoadScene(menuScene, LoadSceneMode.Single);
    }

    void OnLevelWasLoaded() {
        mainCam = Camera.main;
    }
}
