using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

[CustomPropertyDrawer(typeof(AxisKeys))]
public class AxisKeysDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // não indentar
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        

        //inicio
        EditorGUI.BeginProperty(position, label, property);

        //label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        //posicoes
        Rect posLabel = new Rect(position.x, position.y, 15, position.height);
        Rect posField = new Rect(position.x + 20, position.y, 50, position.height);
        Rect negLabel = new Rect(position.x + 75, position.y, 15, position.height);
        Rect negField = new Rect(position.x + 95, position.y, 50, position.height);

        //setar as labels
        GUIContent posGUI = new GUIContent("+");
        GUIContent negGUI = new GUIContent("-");

        //desenhar espaços
        EditorGUI.LabelField(posLabel, posGUI);
        EditorGUI.PropertyField(posField, property.FindPropertyRelative("positive"), GUIContent.none);
        EditorGUI.LabelField(negLabel, negGUI);
        EditorGUI.PropertyField(negField, property.FindPropertyRelative("negative"), GUIContent.none);

        //resetar indentação
        EditorGUI.indentLevel = indent;

        //fim
        EditorGUI.EndProperty();
    }




}
