
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(InteractionObjScripts))]
public class InteractionObjScriptsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        InteractionObjScripts interactionObj = (InteractionObjScripts)target;

        // Enum 값을 선택하는 드롭다운을 표시
        interactionObj.objType = (Type)EditorGUILayout.EnumPopup("Object Type", interactionObj.objType);
        
        // objType 값에 따라 다른 설정을 추가할 수 있음
        switch (interactionObj.objType)
        {
            case Type.SLICEOBJ:
                interactionObj.movement = EditorGUILayout.Toggle("Movement", interactionObj.movement);
                interactionObj.intValue1 = EditorGUILayout.IntField ("Lever Int Value", interactionObj.intValue1);
                interactionObj.strValue = EditorGUILayout.TextField("Lever String Value", interactionObj.strValue);
                break;

            case Type.Move:
                interactionObj.movement = EditorGUILayout.Toggle("Movement", interactionObj.movement);
                interactionObj.intValue1 = EditorGUILayout.IntField("X", interactionObj.intValue1);
                interactionObj.intValue2 = EditorGUILayout.IntField("Y", interactionObj.intValue2);
                interactionObj.intValue3 = EditorGUILayout.IntField("Z", interactionObj.intValue3); 
                interactionObj.floatValue1 = EditorGUILayout.FloatField("Move Speed", interactionObj.floatValue1);
                break;

            case Type.SPAWNOBJ:
                interactionObj.movement = EditorGUILayout.Toggle("Movement", interactionObj.movement);
                interactionObj.intValue1 = EditorGUILayout.IntField("SpObj Int Value", interactionObj.intValue1);
            
                break;
            case Type.SPINOBJ:
                interactionObj.movement = EditorGUILayout.Toggle("Movement", interactionObj.movement); 
                interactionObj.isHitEventEnabled = EditorGUILayout.Toggle("HitEvent", interactionObj.isHitEventEnabled);
                if(interactionObj.isHitEventEnabled)
                {
                    interactionObj.layerMask = EditorGUILayout.LayerField("layerMask", interactionObj.layerMask);
                }
                interactionObj.onMoving = EditorGUILayout.Toggle("Moving", interactionObj.onMoving);
                if (interactionObj.onMoving)
                {
                    interactionObj.floatValue1 = EditorGUILayout.FloatField("AddForce", interactionObj.floatValue1);
                }
                interactionObj.intValue1 = EditorGUILayout.IntField("X", interactionObj.intValue1);
                interactionObj.intValue2 = EditorGUILayout.IntField("Y", interactionObj.intValue2);
                interactionObj.intValue3 = EditorGUILayout.IntField("Z", interactionObj.intValue3);             
                break;
            case Type.ROTATELOOPS:
                interactionObj.movement = EditorGUILayout.Toggle("Movement", interactionObj.movement);
                interactionObj.floatValue1 = EditorGUILayout.FloatField("X", interactionObj.floatValue1);
                interactionObj.floatValue2 = EditorGUILayout.FloatField("Y", interactionObj.floatValue2);
                interactionObj.floatValue3 = EditorGUILayout.FloatField("Z", interactionObj.floatValue3);
                break;
            case Type.LOOK:
                interactionObj.layerMask = EditorGUILayout.LayerField("layerMask", interactionObj.layerMask);
                break;
            default:
            
                break;
        }

        // 기본 인스펙터 GUI 표시
        DrawDefaultInspector();
    }
}
