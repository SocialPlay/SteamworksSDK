using UnityEngine;
using System.Collections;
using Steamworks;
using UnityEngine.UI;

public class CloudGoodsSteamLogin : MonoBehaviour {

    public Text LoginText;

    protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;

    void OnEnable()
    {
        CloudGoods.OnUserAuthorized += CloudGoods_OnUserAuthorized;

        if (SteamManager.Initialized)
        {
            m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
        }
    }

    void OnDisable()
    {
        CloudGoods.OnUserAuthorized -= CloudGoods_OnUserAuthorized;
    }

    void Start()
    {
        if (SteamManager.Initialized)
        {
            string name = SteamFriends.GetPersonaName();

            CloudGoods.LoginWithPlatformUser(CloudGoodsPlatform.Steam, SteamUser.GetSteamID().ToString(), SteamFriends.GetPersonaName());

            Debug.Log("Steam ID: " + SteamUser.GetSteamID().ToString());
        }
    }

    void CloudGoods_OnUserAuthorized(CloudGoodsUser obj)
    {
        LoginText.text = "Welcome " + obj.userName;
    }

    void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
    {
        LoginText.text = "GameOverlay: " + pCallback.m_bActive.ToString();
    }
}
