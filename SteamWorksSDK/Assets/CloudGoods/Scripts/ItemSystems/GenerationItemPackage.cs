using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GenerationItemPackage : MonoBehaviour {

    public int GenerationID;
    public int Location;
    public string UserType;

    public ItemContainer targetContainer;

    List<SelectedGenerationItem> selectedItems = new List<SelectedGenerationItem>();

    bool isGenerateTimerStarted = false;
    bool hasSentPackage = false;

    float timer = 0.0f;
    float maxSendPackageTimer = 3.0f;

    public bool HasPackageBeenSent()
    {
        return hasSentPackage;
    }

    public void InitializeItemIDs(SelectedGenerationItem selectedItem)
    {
        selectedItems.Add(selectedItem);

        if (!isGenerateTimerStarted)
            isGenerateTimerStarted = true;
    }

    public void AddItemID(SelectedGenerationItem selectedItem)
    {
        SelectedGenerationItem generationItem = selectedItems.Find(x => x.ItemId == selectedItem.ItemId);

        if(generationItem != null)
            generationItem.Amount += selectedItem.Amount;
        else
            selectedItems.Add(selectedItem);
    }

    void Update()
    {
        if (isGenerateTimerStarted && !hasSentPackage)
        {
            timer += Time.deltaTime;

            if (timer >= maxSendPackageTimer)
            {
                CloudGoods.GiveGeneratedItemToOwner(UserType, selectedItems, GenerationID, Location, OnReceivedGiveItemGenerationItemResult);
                hasSentPackage = true;
                timer = 0.0f;
            }
        }
    }


    void OnReceivedGiveItemGenerationItemResult(List<GiveGeneratedItemResult> itemResults)
    {
        targetContainer.UpdateContainerWithItems(itemResults);
        CloudGoods.GenerationPackages.Remove(this);
        Destroy(gameObject);
    }

}
