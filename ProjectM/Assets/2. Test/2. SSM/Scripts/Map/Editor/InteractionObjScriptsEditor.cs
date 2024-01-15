using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractionObjScripts))]
public class InteractionObjScriptsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        InteractionObjScripts interactionObj = (InteractionObjScripts)target;

        // Enum 값을 선택하는 드롭다운을 표시
        interactionObj.objType = (Type)EditorGUILayout.EnumPopup("Object Type", interactionObj.objType);
        interactionObj.startEvent = EditorGUILayout.Toggle("StartEvent", interactionObj.startEvent);
        interactionObj.isInteraction = EditorGUILayout.Toggle("isInteraction", interactionObj.isInteraction);
        if (interactionObj.isInteraction)
        {

            interactionObj.isEnable = EditorGUILayout.Toggle("isEnable", interactionObj.isEnable);
            if (interactionObj.isEnable)
            {
                interactionObj.intValue5 = EditorGUILayout.IntField("EnableTime", interactionObj.intValue5);
            }
            interactionObj.isDisable = EditorGUILayout.Toggle("isDisable", interactionObj.isDisable);
            if (interactionObj.isDisable)
            {
                interactionObj.intValue4 = EditorGUILayout.IntField("DisableTime", interactionObj.intValue4);
            }
            interactionObj.isHitEventEnabled = EditorGUILayout.Toggle("HitEvent", interactionObj.isHitEventEnabled);
            if (interactionObj.isHitEventEnabled)
            {
                interactionObj.layerMask = EditorGUILayout.LayerField("LayerMask1", interactionObj.layerMask);
                if (interactionObj.layerMask != 0)
                {
                    interactionObj.layerMask2 = EditorGUILayout.LayerField("LayerMask2", interactionObj.layerMask2);
                    interactionObj.floatValue2 = EditorGUILayout.FloatField("AddForce2", interactionObj.floatValue2);

                }
            }
            interactionObj.intValue6 = EditorGUILayout.IntField("ID", interactionObj.intValue6);
        }


        // objType 값에 따라 다른 설정을 추가할 수 있음
        switch (interactionObj.objType)
        {
            case Type.SLICEOBJ:
                interactionObj.movement = EditorGUILayout.Toggle("Movement", interactionObj.movement);
                interactionObj.intValue1 = EditorGUILayout.IntField("Lever Int Value", interactionObj.intValue1);
                interactionObj.strValue = EditorGUILayout.TextField("Lever String Value", interactionObj.strValue);
                break;

            case Type.Move:
                interactionObj.movement = EditorGUILayout.Toggle("Movement", interactionObj.movement);
                interactionObj.intValue1 = EditorGUILayout.IntField("X", interactionObj.intValue1);
                interactionObj.intValue2 = EditorGUILayout.IntField("Y", interactionObj.intValue2);
                interactionObj.intValue3 = EditorGUILayout.IntField("Z", interactionObj.intValue3);
                interactionObj.floatValue1 = EditorGUILayout.FloatField("Move Speed", interactionObj.floatValue1);
                interactionObj.floatValue2 = EditorGUILayout.FloatField("Time", interactionObj.floatValue2);
                break;

            case Type.SPAWNOBJ:
                interactionObj.movement = EditorGUILayout.Toggle("Movement", interactionObj.movement);
                interactionObj.intValue1 = EditorGUILayout.IntField("SpObj Int Value", interactionObj.intValue1);

                break;
            case Type.SPINOBJ:
                interactionObj.movement = EditorGUILayout.Toggle("Movement", interactionObj.movement);
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

                
            case Type.OBJACT:
                interactionObj.movement = EditorGUILayout.Toggle("movement", interactionObj.movement);

                interactionObj.gameObject1 = EditorGUILayout.ObjectField("GameObj", interactionObj.gameObject1, typeof(GameObject), true) as GameObject;

                break;

            case Type.TELEPORT:
                interactionObj.gameObject1 = EditorGUILayout.ObjectField("GameObj", interactionObj.gameObject1, typeof(GameObject), true) as GameObject;

                break;
            default:



                break;
        }

        // 기본 인스펙터 GUI 표시
        DrawDefaultInspector();
    }
}
