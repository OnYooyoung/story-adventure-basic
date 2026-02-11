using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questOrderIndex;

    Dictionary<int, QuestData> questList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        questList.Add(10, new QuestData("친구들과 대화", new int[] {10, 20}));
    }

    public int GetQuestIndex(int id)
    {
        return questId + questOrderIndex;
    }

    public void QuestIndexInc()
    {
        questOrderIndex++;
    }
}
