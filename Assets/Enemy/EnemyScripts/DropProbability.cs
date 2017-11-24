using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DropProbability {

    //===========================
    // 真偽を判定
    //===========================

    /// <summary>
    /// 入力した確率で判定を行う
    /// </summary>
    public static bool DetectFromPercent(int percent)
    {
        return DetectFromPercent((float)percent);
    }

    /// <summary>
    /// 入力した確率で判定を行う
    /// </summary>
    public static bool DetectFromPercent(float percent)
    {
        // 小数点以下の桁数
        int digitNum = 0;
        if(percent.ToString().IndexOf(".") > 0)
        {
            digitNum = percent.ToString().Split('.')[1].Length;
        }

        // 小数点以下をなくすように乱数の上限と判定の境界を下げる
        int rate = (int)Mathf.Pow(10, digitNum);

        // 乱数の上限と真と判定するボーダーを設定
        int randomValueLimit = 100 * rate;
        int border = (int)(rate * percent);

        return Random.Range(0, randomValueLimit) < border;
    }

    //===========================
    // 複数の中から一つを選択
    //===========================
    
    /// <summary>
    /// 入力したDictから一つを決定し、そのDictのKeyを返す
    /// </summary>
    public static T DetermineFromDict<T>(Dictionary<T, int> targetDict)
    {
        Dictionary<T, float> targetFloatDict = new Dictionary<T, float>();

        foreach(KeyValuePair<T, int> pair in targetDict)
        {
            targetFloatDict.Add(pair.Key, (float)pair.Value);
        }

        return DetermineFromDict(targetFloatDict);
    }


    /// <summary>
    /// 確率とその対象をまとめたDictを入力しその中から一つを決定、対象を返す
    /// </summary>
    public static T DetermineFromDict<T>(Dictionary<T, float> targetDict)
    {
        // 累計確率
        float totalPer = 0;
        foreach(float per in targetDict.Values)
        {
            totalPer += per;
        }

        // 0~累計確率の間で乱数を生成
        float rand = Random.Range(0, totalPer);

        // 乱数から各確率を引いていき、0未満になったら終了
        foreach(KeyValuePair<T, float> pair in targetDict)
        {
            rand -= pair.Value;

            if(rand <= 0)
            {
                return pair.Key;
            }
        }

        // エラー、ここに来たときはプログラムが間違っている
        Debug.Log("抽選ができませんでした");
        return new List<T>(targetDict.Keys)[0];
    }
}
