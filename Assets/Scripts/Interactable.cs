using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private GrabbablePlaceable localItem = null;
    public virtual bool Place(GrabbablePlaceable item)
    {
        if (localItem == null)
        {
            localItem = item;
            localItem.transform.SetParent(transform);
            localItem.transform.position = 
                new Vector3(transform.position.x,
                    transform.position.y + transform.localScale.y / 2 + localItem.transform.localScale.y / 2,
                    transform.position.z);
            localItem.transform.rotation = Quaternion.identity;
            return true;
        }
        else
        {
            return false;
        }

    }

    public virtual GrabbablePlaceable Grab()
    {
        var item = localItem;
        localItem = null;
        return item;

    }
}
