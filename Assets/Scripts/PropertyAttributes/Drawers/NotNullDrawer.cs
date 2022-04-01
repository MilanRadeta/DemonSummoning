using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(NotNull))]
public class NotNullDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        if (property.objectReferenceValue == null)
        {
            GUI.color = Color.red;
            var targetObject = property.serializedObject.targetObject;
            var typeName = targetObject.GetType().Name;
            var propName = property.name;
            var objName = targetObject.name;
            Debug.LogError($"{typeName}.{propName} not assigned on {objName}");
        }

        
        EditorGUI.PropertyField(position, property, label);
        GUI.color = Color.white;

        EditorGUI.EndProperty();
    }

}