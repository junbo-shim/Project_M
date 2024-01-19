using BNG;
using System.Collections.Generic;
using UnityEngine;


public class Skill : MonoBehaviour
{

    Inventory inventory;
    ItemDataBase itemData;
    IllustratedGuide illustratedGuide;

    public CreaftingUI creaftingUI;
    public GameObject[] creaftingButton;
    public GameObject[] enhanceButton;
    public List<string> creaftedItemNames;


    Dictionary<string, int> mySkill = new Dictionary<string, int>();

    private void Awake()
    {
        SkillManager.Instance.skillCheckDict = mySkill;
    }

    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
        itemData = FindAnyObjectByType<ItemDataBase>();
        illustratedGuide = FindAnyObjectByType<IllustratedGuide>();


        for (int i = 0; i < 5; i++)
        {
            enhanceButton[i].SetActive(false);
        }

        for (int i = 0; i < 6; i++)
        {
            creaftingButton[i].SetActive(false);
        }

        mySkill.Add("Invisibility", 1106);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Keypad0))
        {
            AddSkillDeveloperMode();
        }
    }

    public void AddSkillDeveloperMode()
    {
        Debug.Log("스킬 추가완료");
        FireBallSkill();
        PosionSkill();
        ProtectSkill();
        IceBallSkill();
        JumpSkill();
        HealSkill();
    }

    public void AddEhancedSkillDeveloperMode()
    {
        mySkill.Add("_FireBall", 1001);
        mySkill.Add("_Poison", 1003);
        mySkill.Add("_IceBall", 1101);
        mySkill.Add("_Protect", 1103);
    }


    #region FIreBall
    public void FireBallSkill()
    {
        creaftingButton[0].SetActive(false);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "FIreBallRecipe")
            {
                inventory.items[i].itemCount = inventory.items[i].itemCount - 3;
                //creaftedItemNames.Add("FIreBallRecipe");
                EnhanceButtonManager("FIreBallRecipe", 0);
                illustratedGuide.FireBallChangeColor();


            }
        }
        mySkill.Add("FireBall", 1000);
        Debug.Log(mySkill.Keys);
    }


    public void EnhanceFireBallSkill()
    {
        enhanceButton[0].SetActive(false);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "FIreBallRecipe")
            {
                inventory.items[i].itemCount = inventory.items[i].itemCount - 3;
            }
        }

        mySkill.Add("_FireBall", 1001);
    }
    #endregion

    #region Protect
    public void ProtectSkill()
    {
        creaftingButton[1].SetActive(false);


        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "ProtectRecipe")
            {
                inventory.items[i].itemCount = inventory.items[i].itemCount - 3;
                //creaftedItemNames.Add("ProtectRecipe");
                EnhanceButtonManager("ProtectRecipe", 1);
                illustratedGuide.ProtectChangeColor();
            }
        }

        mySkill.Add("Protect", 1102);
        Debug.Log(mySkill.Keys);

    }


    public void EnhanceProtectSkill()
    {
        enhanceButton[1].SetActive(false);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "ProtectRecipe")
            {
                inventory.items[i].itemCount = inventory.items[i].itemCount - 3;
            }
        }

        mySkill.Add("_Protect", 1103);

    }

    #endregion

    #region IceBall
    public void IceBallSkill()
    {
        creaftingButton[2].SetActive(false);


        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "IceBallRecipe")
            {
                inventory.items[i].itemCount = inventory.items[i].itemCount - 3;
                //creaftedItemNames.Add("IceBallRecipe");
                EnhanceButtonManager("IceBallRecipe", 2);
                illustratedGuide.IceBallChangeColor();
            }
        }
        mySkill.Add("IceBall", 1100);
        Debug.Log(mySkill.Keys);

    }


    public void EnhanceIceBallSkill()
    {
        enhanceButton[2].SetActive(false);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "IceBallRecipe")
            {
                inventory.items[i].itemCount = inventory.items[i].itemCount - 3;

            }
        }
        mySkill.Add("_IceBall", 1101);

    }
    #endregion

    #region Poison
    public void PosionSkill()
    {
        creaftingButton[3].SetActive(false);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "PoisonRecipe")
            {
                inventory.items[i].itemCount = inventory.items[i].itemCount - 3;
                //creaftedItemNames.Add("PoisonRecipe");
                EnhanceButtonManager("PoisonRecipe", 3);
                illustratedGuide.PoisonChangeColor();
            }
        }

        mySkill.Add("Poison", 1002);
        Debug.Log(mySkill.Keys);

    }



    public void EnhancePosionSkill()
    {
        enhanceButton[3].SetActive(false);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "PoisonRecipe")
            {
                inventory.items[i].itemCount = inventory.items[i].itemCount - 3;
            }
        }
        mySkill.Add("_Poison", 1003);

    }
    #endregion

    #region Jump
    public void JumpSkill()
    {
        creaftingButton[4].SetActive(false);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "JumpRecipe")
            {
                inventory.items[i].itemCount = inventory.items[i].itemCount - 2;
                //creaftedItemNames.Add("JumpRecipe");
                //EnhanceButtonManager("JumpRecipe", 4);
                illustratedGuide.JumpChangeColor();
            }
        }
        mySkill.Add("Jump", 1104);
        Debug.Log(mySkill.Keys);

    }

    #endregion

    #region Heal
    public void HealSkill()
    {
        creaftingButton[5].SetActive(false);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "HillRecipe")
            {
                inventory.items[i].itemCount = inventory.items[i].itemCount - 3;
                //creaftedItemNames.Add("HillRecipe");
                illustratedGuide.HillChangeColor();
            }
        }
        mySkill.Add("Heal", 1107);
        Debug.Log(mySkill.Keys);

    }
    #endregion


    private void EnhanceButtonManager(string itemName, int buttonIndex)
    {
        int itemIndex = inventory.items.FindIndex(item => item.itemName == itemName);

        if (inventory.items[itemIndex].itemCount >= 3)
        {
            enhanceButton[buttonIndex].SetActive(true);
        }
        else if (inventory.items[itemIndex].itemCount < 3)
        {
            enhanceButton[buttonIndex].SetActive(false);
        }
    }


    public void CreafringSkill(string itemName)
    {

        int itemIndex = inventory.items.FindIndex(item => item.itemName == itemName);
        int itemDBIndex = itemData.itemDB.FindIndex(item => item.itemName == itemName);

        if (creaftedItemNames.Contains(itemName))
        {
            EnhanceButtonManager("FIreBallRecipe", 0);
            EnhanceButtonManager("ProtectRecipe", 1);
            EnhanceButtonManager("IceBallRecipe", 2);
            EnhanceButtonManager("PoisonRecipe", 3);
            return;
        }

        if (itemDBIndex < 6)
        {
            if (inventory.items[itemIndex].itemName == "JumpRecipe")
            {
                if (inventory.items[itemIndex].itemCount >= 2)
                {
                    creaftingButton[itemDBIndex].SetActive(true);
                }
            }

            else if (inventory.items[itemIndex].itemCount >= 3 && inventory.items[itemIndex].itemName != "JumpRecipe")
            {
                creaftingButton[itemDBIndex].SetActive(true);
            }
        }
        else
        {
            return;
        }


    }



}