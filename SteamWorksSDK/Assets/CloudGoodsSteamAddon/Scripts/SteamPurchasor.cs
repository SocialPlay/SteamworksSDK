using UnityEngine;
using System.Collections;
using System;

public class SteamPurchasor : MonoBehaviour {

    public event Action<string> ReceivedPurchaseResponse;
    public event Action<string> OnPurchaseErrorEvent;

    public void Purchase(PremiumBundle id, int amount, string userID)
    {

    }

    public void OnReceivedPurchaseResponse(string data)
    {

    }
}
