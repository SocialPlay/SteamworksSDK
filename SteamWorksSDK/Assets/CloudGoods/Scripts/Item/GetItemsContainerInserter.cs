using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using SocialPlay.Data;

public class GetItemsContainerInserter : MonoBehaviour, IGetItems
{

    static public GetItemsContainerInserter instance;

    public ItemContainer container;

    public event Action<List<ItemData>> onReciveItems;

    void Awake()
    {
        instance = this;
    }

    public void GetGameItem(List<ItemData> items)
    {

        if (container == null)
            throw new Exception("You must provide a container to your GameItemContainerInserter");
        else
        {
            List<SelectedGenerationItem> giveItems = new List<SelectedGenerationItem>();

            int GenerationID = 0;

            foreach (ItemData item in items)
            {
                if (item.IsGenerated)
                    GenerationID = item.GenerationID;

                SelectedGenerationItem selectItem = new SelectedGenerationItem();

                selectItem.ItemId = item.ItemID;
                selectItem.Amount = item.stackSize;

                AddItemToGenerationPackage("User", item.GenerationID, container.GetComponentInChildren<PersistentItemContainer>().Location, selectItem);
            }
        }
    }

    void AddItemToGenerationPackage(string UserType, int GenerationID, int location, SelectedGenerationItem selectedItem)
    {
        GenerationItemPackage generationItemPackage = CloudGoods.GenerationPackages.Find(x => x.UserType == UserType && x.GenerationID == GenerationID && x.Location == location);

        if (generationItemPackage != null && !generationItemPackage.HasPackageBeenSent())
        {
            Debug.Log("Checking for generation packages: " + generationItemPackage + " has item been sent: " + generationItemPackage.HasPackageBeenSent());
            generationItemPackage.AddItemID(selectedItem);
        }
        else
        {
            CreateGenerationPackage(UserType, GenerationID, location, selectedItem);
        }
    }

    void CreateGenerationPackage(string UserType, int GenerationID, int location, SelectedGenerationItem selectedItems)
    {
        GameObject packageObj = new GameObject();
        packageObj.name = "Generation Package : " + GenerationID;
        GenerationItemPackage generationPackage = packageObj.AddComponent<GenerationItemPackage>();

        generationPackage.UserType = UserType;
        generationPackage.GenerationID = GenerationID;
        generationPackage.Location = location;
        generationPackage.InitializeItemIDs(selectedItems);
        generationPackage.targetContainer = container;

        CloudGoods.GenerationPackages.Add(generationPackage);
    }


}