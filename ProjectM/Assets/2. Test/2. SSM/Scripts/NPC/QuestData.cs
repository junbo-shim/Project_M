using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{

    private int _id;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    private string _questType;

    public string QuestType
    {
        get { return _questType; }
        set { _questType = value; }
    }

    private string _questNameKey;

    public string QuestNameKey
    {
        get { return _questNameKey; }
        set { _questNameKey = value; }
    }

    private string _questGoalKey;

    public string QuestGoalKey
    {
        get { return _questGoalKey; }
        set { _questGoalKey = value; }
    }

    private string _questExplainKey;

    public string QuestExplainKey
    {
        get { return _questExplainKey; }
        set { _questExplainKey = value; }
    }

    private string _questBackgroundType;

    public string QuestBackgroundType
    {
        get { return _questBackgroundType; }
        set { _questBackgroundType = value; }
    }

    private string _questAreaType;

    public string QuestAreaType
    {
        get { return _questAreaType; }
        set { _questAreaType = value; }
    }

    private int _completionConditionID;

    public int CompletionConditionID
    {
        get { return _completionConditionID; }
        set { _completionConditionID = value; }
    }

    private int _questNPCSuccessID;

    public int QuestNPCSuccessID
    {
        get { return _questNPCSuccessID; }
        set { _questNPCSuccessID = value; }
    }

    private int _precedeQuestID;

    public int PrecedeQuestID
    {
        get { return _precedeQuestID; }
        set { _precedeQuestID = value; }
    }

    private int _linkQuestID;

    public int LinkQuestID
    {
        get { return _linkQuestID; }
        set { _linkQuestID = value; }
    }

    private string _questProgressDialogue;

    public string QuestProgressDialogue
    {
        get { return _questProgressDialogue; }
        set { _questProgressDialogue = value; }
    }
    private int _Reward_ID;

    public int Reward_ID
    {
        get { return _Reward_ID; }
        set { _Reward_ID = value; }
    }

    private string _Situation;

    public string Situation
    {
        get { return _Situation; }
        set { _Situation = value; }
    }

}
