﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectionsIDRestriction : MonoBehaviour, IContainerRestriction {

    public List<int> CollectionsIDList= new List<int>();
    public bool IsExcluded = false;

    ItemContainer restrictedContainer;

    void Awake()
    {
        restrictedContainer = GetComponent<ItemContainer>();
        restrictedContainer.containerAddRestrictions.Add(this);
    }

    public bool IsRestricted(ContainerAction action, ItemData itemData)
    {
        if (IsExcluded)
        {
            if (CollectionsIDList.Exists(x => x == itemData.CollectionID))
            {
                Debug.LogWarning("Item Resticted for being added to container because it has a Collection ID Restriction");
                return true;

            }

            return false;
        }
        else
        {
            if (CollectionsIDList.Exists(x => x == itemData.CollectionID))
                return false;

            Debug.LogWarning("Item Resticted for being added to container because it has a Collection ID Restriction");
            return true;
        }
    }
}
