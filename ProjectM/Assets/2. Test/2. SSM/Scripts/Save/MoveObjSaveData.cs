using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjSaveData : MonoBehaviour
{
    public string pos;

    public string actState;

    public string rot;

    public void Start()
    {
        SetVector(pos, rot, actState);
    }

    public void SaveObj()
    {
        pos = ReplaceStr(transform.position.ToString()); 

        rot = ReplaceStr(transform.rotation.ToString());

        actState = gameObject.activeSelf.ToString();
    }

    public string ReplaceStr(string str)
    {
        str = str.Replace("(", "");
        str = str.Replace(")", "");
        return str;
    }

    public void SetVector(string pos_ ,string rot_ , string actState_)
    {
        Quaternion newQuaternion;
        string[] pos_Array = pos_.Split(',');
        string[] rot_Array = rot_.Split(',');

        transform.position = new Vector3(int.Parse(pos_Array[0]), int.Parse(pos_Array[1]), int.Parse(pos_Array[2]));
        if (rot_Array.Length == 3 &&
            float.TryParse(rot_Array[0], out float x) &&
            float.TryParse(rot_Array[1], out float y) &&
            float.TryParse(rot_Array[2], out float z))
        {
            newQuaternion = new Quaternion(x, y, z,0f);
        }else
        {
            newQuaternion = Quaternion.identity;
        }
       
        transform.rotation = newQuaternion;

        if(actState_.Equals("true"))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


}
