using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class QuestState
{
    public enum QuestStatus
    {
        NotStarted,
        InProgress,
        Completed
    }

    protected QuestStatus _state;

    public QuestState()
    {
        _state = QuestStatus.NotStarted;
    }

    public QuestStatus State
    {
        get { return _state; }
        set { _state = value; }
    }

    public abstract void Start();

    public abstract void Complete();

}