using Tool.Tween;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Tween.Editor
{
    [CustomEditor(typeof(CustomButtonByInheritance))]
    internal class CustomButtonEditor : ButtonEditor
    {
        private SerializedProperty m_InteractableProperty;

        protected override void OnEnable()
        {
            m_InteractableProperty = serializedObject.FindProperty("m_Interactable");
        }

        // Новый способ редактирования представления инскпектора
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();

            PropertyField animationType = new(serializedObject.FindProperty(CustomButtonByInheritance.AnimationTypeName));
            PropertyField curveEase = new(serializedObject.FindProperty(CustomButtonByInheritance.CurveEaseName));
            PropertyField duration = new(serializedObject.FindProperty(CustomButtonByInheritance.DurationName));

            Label tweenLabel = new("Settings Tween");
            Label intractableLabel = new("Interactable");

            root.Add(tweenLabel);
            root.Add(animationType);
            root.Add(curveEase);
            root.Add(duration);

            root.Add(intractableLabel);
            root.Add(new IMGUIContainer(OnInspectorGUI));

            return root;
        }

        // Старый способ представления инскпектора
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_InteractableProperty);

            EditorGUI.BeginChangeCheck();
            EditorGUI.EndChangeCheck();

            serializedObject.ApplyModifiedProperties();
        }
    }
}