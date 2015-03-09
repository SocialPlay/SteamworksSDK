using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;

public class ItemDrop : MonoBehaviour
{
    static public GameObject dropParentObject { get { if (mDrop == null) mDrop = new GameObject("DroppedItems"); return mDrop; } }
    static GameObject mDrop;

    public IGameObjectAction postDropObjectAction;

    public void DropItemIntoWorld(ItemData item, Vector3 dropPosition, GameObject dropModelDefault, bool isDropSingleAmount)
    {
        if (item != null)
        {
            Debug.Log("Loading asset :" + item.assetURL.ToRichColor(Color.white));
            item.AssetBundle((UnityEngine.Object bundleObj) =>
                {
                    if (isDropSingleAmount)
                    {
                        for (int i = 0; i < item.stackSize; i++)
                            dropPosition = DropItem(item, dropPosition, dropModelDefault, bundleObj, 1);
                    }

                    else
                        dropPosition = DropItem(item, dropPosition, dropModelDefault, bundleObj, item.stackSize);
                }
            );
        }
    }

    private Vector3 DropItem(ItemData item, Vector3 dropPosition, GameObject dropModelDefault, UnityEngine.Object bundleObj, int dropAmount)
    {
        GameObject dropObject = GameObject.Instantiate(bundleObj != null ? bundleObj : dropModelDefault) as GameObject;

        ItemData itemData = dropObject.AddComponent<ItemDataComponent>().itemData;
        itemData.SetItemData(item);
        itemData.stackSize = dropAmount;

        dropObject.name = item.itemName + " (ID: " + item.CollectionID + ")";

        ItemComponentInitalizer.InitializeItemWithComponents(dropObject.GetComponent<ItemDataComponent>().itemData, AddComponetTo.prefab);

        dropObject.transform.position = dropPosition;
        dropObject.transform.parent = dropParentObject.transform;

        if (postDropObjectAction != null)
            postDropObjectAction.DoGameObjectAction(dropObject);
        return dropPosition;
    }
}
