using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ValueListAttribute))]
public class TestDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ValueListAttribute valueList = attribute as ValueListAttribute;

        if (property.propertyType == SerializedPropertyType.Integer)
        {
            var values = valueList.ValueList;
            var display = new List<string>();
            foreach (var v in values)
            {
                display.Add(v.ToString());
            }
            property.intValue = EditorGUI.IntPopup(position, label.text, property.intValue, display.ToArray(), values);
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use ValueList with int.");
        }
    }
}