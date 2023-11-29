using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSystem : MonoBehaviour
{
    //IllustratedGuide illustratedGuide;
    //AllBook allBook;
    //Skill skill;


    //public LayerMask targetLayer;


    //private void Start()
    //{
    //    illustratedGuide = GetComponent<IllustratedGuide>();
    //    allBook = GetComponent<AllBook>();
    //    skill = GetComponent<Skill>();
    //}

    //private void Update()
    //{
    //    AllClick();
    //}


    //private void AllClick()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    if (Input.GetMouseButtonDown(0))// 마우스 왼쪽 버튼을 클릭했을 때
    //    {
    //        if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
    //        {
    //            Debug.LogFormat("충돌한 오브젝트 이름: " + hit.collider.gameObject.name);
    //            //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    //            //GuideClick
    //            if (hit.collider.gameObject.name == "FireBall")
    //            {
    //                illustratedGuide.FIreBallOnOff();
    //            }
    //            else if (hit.collider.gameObject.name == "Razer")
    //            {
    //                illustratedGuide.RazerOnOff();
    //            }
    //            else if (hit.collider.gameObject.name == "IceBullet")
    //            {
    //                illustratedGuide.IceBulletOnOff();
    //            }
    //            else if (hit.collider.gameObject.name == "Poison")
    //            {
    //                illustratedGuide.PoisonOnOff();
    //            }

    //            //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    //            //SideClick

    //            else if (hit.collider.gameObject.name == "InventoryButton")
    //            {
    //                allBook.InventoryUIOnOff();

    //            }
    //            else if (hit.collider.gameObject.name == "SkillButton")
    //            {
    //                allBook.SkillUIOnOff();
    //            }
    //            else if (hit.collider.gameObject.name == "CreaftingButton")
    //            {
    //                allBook.CreaftingUIOnOff();
    //            }

    //            //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    //            //CreaftingClick

    //            else if (hit.collider.gameObject.name == "CreaftingButton_F")
    //            {
    //                skill.FireBallSkill();
    //            }
    //            else if (hit.collider.gameObject.name == "CreaftingButton_EF")
    //            {
    //                skill.EnhanceFireBallSkill();
    //            }
    //            else if (hit.collider.gameObject.name == "CreaftingButton_R")
    //            {
    //                skill.RazerSkill();
    //            }
    //            else if (hit.collider.gameObject.name == "CreaftingButton_ER")
    //            {
    //                skill.EnhanceRazerSkill();
    //            }
    //            else if (hit.collider.gameObject.name == "CreaftingButton_I")
    //            {
    //                skill.IceBulletSkill();
    //            }
    //            else if (hit.collider.gameObject.name == "CreaftingButton_EI")
    //            {
    //                skill.EnhanceIceBulletSkill();
    //            }
    //            else if (hit.collider.gameObject.name == "CreaftingButton_P")
    //            {
    //                skill.PosionSkill();
    //            }
    //            else if (hit.collider.gameObject.name == "CreaftingButton_EP")
    //            {
    //                skill.EnhancePosionSkill();
    //            }
    //        }
    //    }
    //}


}
