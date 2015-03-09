using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PublicAppDataExample : MonoBehaviour {

    public InputField SaveKey;
    public InputField SaveValue;
    public Text saveResponse;

    public InputField RetrieveKey;
    public Text loadResponse;

    public InputField DeleteKey;
    public Text DeleteResponse;

    public Text RetrieveAllAppResponse;

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


    public void SaveAppData()
    {
        CloudGoods.SaveAppData(SaveKey.text, SaveValue.text, (r) =>
        {
            Debug.Log(r);
            saveResponse.text = r.ToString();
        });


    }


    public void RetrieveAppDataValue()
    {
        CloudGoods.RetrieveAppDataValue(RetrieveKey.text, (r) =>
        {
            if (r.isExisting)
                loadResponse.text = r.userValue;
            else
            {
                loadResponse.text = "App Does not have a value for that key".ToRichColor(Color.yellow);
            }
        });
    }


    public void DeleteAppDateValue()
    {
        CloudGoods.DeleteAppDataValue(DeleteKey.text, (r) => { DeleteResponse.text = r; });
    }


    public void RetrieveAllAppDatas()
    {
     
        CloudGoods.RetrieveAllAppDataValues((r) =>
        {
       
            RetrieveAllAppResponse.text = "";
            foreach (KeyValuePair<string, string> data in r)
            {
                RetrieveAllAppResponse.text += data.Key.ToRichColor(Color.white) + ":" + (data.Value != null ? data.Value : "Null") + "\n";
            }

        });
    }




}
