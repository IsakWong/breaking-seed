using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanObtainItem
{
    GameObject GetGO();

    void BeginObtainItem();
    void ObtainItem(ItemBehaviour Item);
    void BeginDropItem();
    void DropItem(ItemBehaviour Item);
    Vector3 GetObtainLocation();
}