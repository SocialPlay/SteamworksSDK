using UnityEngine;
using System.Collections;
using Steamworks;

public class SteamScript : MonoBehaviour {

    protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;

	// Use this for initialization
	void OnEnable() {
        if (SteamManager.Initialized)
        {
            m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
        }
	}

    private void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
    {
        if (pCallback.m_bActive == 1)
            Debug.Log("Steam Overlay has been actviated");
        else
            Debug.Log("Steam Overlay has been closed");
    }

}
