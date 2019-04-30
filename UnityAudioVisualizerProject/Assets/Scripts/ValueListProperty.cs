using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueListAttribute : PropertyAttribute
{
    public int[] ValueList { get; private set; }

    public ValueListAttribute(params int[] valueList)
    {
        this.ValueList = valueList;
    }

}

