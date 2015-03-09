using UnityEngine;
using System.Collections;

public class DisplayLoginPanelOnStart : MonoBehaviour {

    public UnityUICloudGoodsLogin login;

	// Use this for initialization
	void Start () {
        login.DisplayLoginPanel();
	}

}
