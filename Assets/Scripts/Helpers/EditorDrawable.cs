using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
#if UNITY_EDITOR
    /// <summary>
    /// Draws the property field for any field marked with ExpandableAttribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(ExpandableAttribute), true)]
    public class ExpandableAttributeDrawer : PropertyDrawer
    {
        // Use the following area to change the style of the expandable ScriptableObject drawers;
        #region Style Setup
        private enum BackgroundStyles
        {
            None,
            HelpBox,
            Darken,
            Lighten
        }

        /// <summary>
        /// Whether the default editor Script field should be shown.
        /// </summary>
        private static bool _Show_Script_Field = false;

        /// <summary>
        /// The spacing on the inside of the background rect.
        /// </summary>
        private static float _Inner_Spacing = 6.0f;

        /// <summary>
        /// The spacing on the outside of the background rect.
        /// </summary>
        private static float _Outer_Spacing = 4.0f;

        /// <summary>
        /// The style the background uses.
        /// </summary>
        private static readonly BackgroundStyles BACKGROUND_STYLE = BackgroundStyles.HelpBox;

        /// <summary>
        /// The colour that is used to darken the background.
        /// </summary>
        private static readonly Color _Darken_Colour = new Color(0.0f, 0.0f, 0.0f, 0.2f);

        /// <summary>
        /// The colour that is used to lighten the background.
        /// </summary>
        private static readonly Color _Lighten_Colour = new Color(1.0f, 1.0f, 1.0f, 0.2f);
        #endregion

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float totalHeight = 0.0f;

            totalHeight += EditorGUIUtility.singleLineHeight;

            if (property.objectReferenceValue == null)
                return totalHeight;

            if (!property.isExpanded)
                return totalHeight;

            SerializedObject targetObject = new SerializedObject(property.objectReferenceValue);

            SerializedProperty field = targetObject.GetIterator();

            field.NextVisible(true);

            if (_Show_Script_Field)
            {
                totalHeight += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            while (field.NextVisible(false))
            {
                totalHeight += EditorGUI.GetPropertyHeight(field, true) + EditorGUIUtility.standardVerticalSpacing;
            }

            totalHeight += _Inner_Spacing * 2;
            totalHeight += _Outer_Spacing * 2;

            return totalHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect fieldRect = new Rect(position);
            fieldRect.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(fieldRect, property, label, true);

            if (property.objectReferenceValue == null)
                return;

            property.isExpanded = EditorGUI.Foldout(fieldRect, property.isExpanded, GUIContent.none, true);

            if (!property.isExpanded)
                return;

            SerializedObject targetObject = new SerializedObject(property.objectReferenceValue);

            #region Format Field Rects
            List<Rect> propertyRects = new List<Rect>();
            Rect marchingRect = new Rect(fieldRect);

            Rect bodyRect = new Rect(fieldRect);
            bodyRect.xMin += EditorGUI.indentLevel * 14;
            bodyRect.yMin += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
                                                               + _Outer_Spacing;

            SerializedProperty field = targetObject.GetIterator();
            field.NextVisible(true);

            marchingRect.y += _Inner_Spacing + _Outer_Spacing;

            if (_Show_Script_Field)
            {
                propertyRects.Add(marchingRect);
                marchingRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            while (field.NextVisible(false))
            {
                marchingRect.y += marchingRect.height + EditorGUIUtility.standardVerticalSpacing;
                marchingRect.height = EditorGUI.GetPropertyHeight(field, true);
                propertyRects.Add(marchingRect);
            }

            marchingRect.y += _Inner_Spacing;

            bodyRect.yMax = marchingRect.yMax;
            #endregion

            DrawBackground(bodyRect);

            #region Draw Fields
            EditorGUI.indentLevel++;

            int index = 0;
            field = targetObject.GetIterator();
            field.NextVisible(true);

            if (_Show_Script_Field)
            {
                //Show the disabled script field
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.PropertyField(propertyRects[index], field, true);
                EditorGUI.EndDisabledGroup();
                index++;
            }

            //Replacement for "editor.OnInspectorGUI ();" so we have more control on how we draw the editor
            while (field.NextVisible(false))
            {
                try
                {
                    EditorGUI.PropertyField(propertyRects[index], field, true);
                }
                catch (StackOverflowException)
                {
                    field.objectReferenceValue = null;
                    Debug.LogError("Detected self-nesting causing a StackOverflowException, avoid using the same " +
                                   "object inside a nested structure.");
                }

                index++;
            }

            targetObject.ApplyModifiedProperties();

            EditorGUI.indentLevel--;
            #endregion
        }

        /// <summary>
        /// Draws the Background
        /// </summary>
        /// <param name="rect">The Rect where the background is drawn.</param>
        private static void DrawBackground(Rect rect)
        {
            switch (BACKGROUND_STYLE)
            {

                case BackgroundStyles.HelpBox:
                    EditorGUI.HelpBox(rect, "", MessageType.None);
                    break;

                case BackgroundStyles.Darken:
                    EditorGUI.DrawRect(rect, _Darken_Colour);
                    break;

                case BackgroundStyles.Lighten:
                    EditorGUI.DrawRect(rect, _Lighten_Colour);
                    break;
            }
        }
    }
#endif

    /// <summary>
    /// Use this property on a ScriptableObject type to allow the editors drawing the field to draw an expandable
    /// area that allows for changing the values on the object without having to change editor.
    /// </summary>
    public class ExpandableAttribute : PropertyAttribute
    {
        public ExpandableAttribute()
        {

        }
    }
}