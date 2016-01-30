#if ENABLE_UNET

using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NetworkManager))]
public class Networking : MonoBehaviour {

    private static Networking s_instance;
    public static Networking Manager {
        get {
            if (s_instance == null)
                s_instance = FindObjectOfType(typeof(Networking)) as Networking;

            if (s_instance == null) {
                GameObject obj = new GameObject("NetworkManager");
                s_instance = obj.AddComponent(typeof(Networking)) as Networking;
            }
            return s_instance;
        }
    }

    NetworkManager manager;

    void Start() {
        manager = GetComponent<NetworkManager>();
        manager.dontDestroyOnLoad = true;

        manager.StartMatchMaker();
        Debug.Log(GetType().BaseType);
    }

    [ContextMenu("ClearRegisteredPrefabs")]
    public void ClearRegisteredPrefabs() {
        if (!manager)
            manager = GetComponent<NetworkManager>();
        manager.spawnPrefabs.Clear();
    }

    public virtual void DebugPlay() {
        StartCoroutine(LoadMatchMaker());
    }

    public IEnumerator LoadMatchMaker() {
        int trys = 1;
        while (trys < 4) {
            manager.matchMaker.ListMatches(0, 2000, "", manager.OnMatchList);
            yield return new WaitForSeconds(1f);
            trys++;
            if (manager.matches != null) {
                if (manager.matches.Count > 0) {
                    foreach (var match in manager.matches) {
                        manager.matchName = match.name;
                        manager.matchSize = (uint)match.currentSize;
                        manager.matchMaker.JoinMatch(match.networkId, "", manager.OnMatchJoined);
                        trys = 99;
                        yield break;
                    }
                }
            }
        }
        manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, true, "", manager.OnMatchCreate);

        yield return null;
    }
}

#endif //ENABLE_UNET
