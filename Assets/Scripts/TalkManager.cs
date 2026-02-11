using System;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateTalkData();
    }

    void GenerateTalkData()
    {
        // Talk
        talkData.Add(10, new string[] { "안녕:1", "좋은 아침:2" });
        talkData.Add(20, new string[] { "하하:2", "물고기 진짜 많다!:1" });
        talkData.Add(100, new string[] { "믿기 어렵겠지만 상자다" });

        // Quest talk
        talkData.Add(10 + 10, new string[] { "파랑이가 할 말이 있대:1", "중요한 일이라는데?:0" }); // 퀘스트번호, NPC번호
        talkData.Add(10 + 20, new string[] { "오늘도 낚시할거야?:1", "재미있겠다:2" });


        for (int i = 0; i < 3; i++)
        {
            portraitData.Add(10 + i, portraitArr[i]);
        }
        for (int i = 0; i < 3; i++)
        {
            portraitData.Add(20 + i, portraitArr[3+i]);
        }
    }

    public string GetTalkData(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length) return null;
        return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex) 
    {
        return portraitData[id + portraitIndex]; 
    }
}