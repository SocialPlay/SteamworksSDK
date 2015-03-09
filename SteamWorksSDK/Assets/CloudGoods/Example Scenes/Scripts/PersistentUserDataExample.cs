using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class PersistentUserDataExample : MonoBehaviour
{

    public InputField SaveKey;
    public InputField SaveValue;
    public Text saveResponse;

    public InputField RetrieveKey;
    public Text loadResponse;

    public InputField DeleteKey;
    public Text DeleteResponse;

    public Text RetrieveAllUserResponse;

    public InputField RetrieveAllValuesOfKey;
    public Text RetrieveAllValuesOfKeyResponse;

    public InputField AlternateUserID;
    public Toggle isUsingAlternate;

    Guid User
    {
        get
        {
            Guid userID = new Guid(CloudGoods.user.userGuid);
            if (isUsingAlternate.isOn)
            {
                try
                {
                    userID = new Guid(AlternateUserID.text);
                }
                catch
                {
                    userID = new Guid(CloudGoods.user.userGuid);
                }
            }
            return userID;
        }
    }


    void Awake()
    {
        CloudGoods.OnRegisteredUserToSession += (r) => { ShowChildren(true); };
        ShowChildren(false);
    }


    void ShowChildren(bool isShown)
    {
        for (int childIndex = 0; childIndex < this.transform.childCount; childIndex++)
        {
            this.transform.GetChild(childIndex).gameObject.SetActive(isShown);
        }
    }


    public void SaveUserData()
    {
        CloudGoods.SaveUserData(SaveKey.text, SaveValue.text, (r) =>
        {
            Debug.Log(r);
            saveResponse.text = r.ToString();
        }, User);


    }


    public void RetrieveUserDataValue()
    {
        CloudGoods.RetrieveUserDataValue(RetrieveKey.text, (r) =>
        {
            if (r.isExisting)
                loadResponse.text = r.userValue;
            else
            {
                loadResponse.text = "User Does not have a value for that key".ToRichColor(Color.yellow);
            }
        }, User);
    }


    public void DeleteUserDateValue()
    {
        CloudGoods.DeleteUserDataValue(DeleteKey.text, (r) => { DeleteResponse.text = r; }, User);
    }


    public void RetrieveAllUsersData()
    {
        CloudGoods.RetrieveAllUserDataValues((r) =>
        {
            RetrieveAllUserResponse.text = "";
            foreach (KeyValuePair<string, string> data in r)
            {
                RetrieveAllUserResponse.text += data.Key.ToRichColor(Color.white) + ":" + (data.Value != null ? data.Value : "Null") + "\n";
            }

        }, User);
    }


    public void RetrieveAllUserDataOfKey()
    {
        CloudGoods.RetrieveAllUserDataOfKey(RetrieveAllValuesOfKey.text, (r) =>
        {
            RetrieveAllValuesOfKeyResponse.text = "";
            for (int i = 0; i < r.Count; i++)
            {
                RetrieveAllValuesOfKeyResponse.text += r[i].user.userName.ToRichColor(Color.white) + " : " + r[i].value + "\n";
            }
        }, User);
    }


}
