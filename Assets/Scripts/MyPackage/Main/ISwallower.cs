using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwallower
{
    public void SwallowItem(Transform item, int price = 0);
}
