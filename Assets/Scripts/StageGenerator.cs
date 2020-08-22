﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    const int StageTipSize = 30;

    int currentTipIndex;

    public Transform character;
    public GameObject[] stageTips;
    public int startTipIndex;
    public int preInstantiate;
    public List<GameObject> generatedStageList = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        currentTipIndex = startTipIndex - 1;
        UpdateStage(preInstantiate);
    }

    // Update is called once per frame
    void Update()
    {
        // キャラクターの一から現在のステージチップのインデックスを計算
        int charaPositionIndex = (int)(character.position.z / StageTipSize);

        // 次のステージチップに入ったらステージの更新処理をおこなう
        if (charaPositionIndex + preInstantiate > currentTipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }
    }

    // 指定のIndexまでのステージチップを生成して、管理下に置く
    void UpdateStage(int toTipIndex)
    {
        if (toTipIndex <= currentTipIndex) return;

        // 指定のステージチップまでを作成
        for (int i = currentTipIndex + 1; i <= toTipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);

            // 生成したステージチップを管理リストに追加
            generatedStageList.Add(stageObject);
        }

        // ステージ保持上限内になるまで古いステージを削除
        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();

        currentTipIndex = toTipIndex;
    }

    // 指定のインデックス位置にStegeオブジェクトをランダムに生成
    GameObject GenerateStage(int tipIndex)
    {
        int nextStageTip = Random.Range(0, stageTips.Length);

        GameObject stageObject = (GameObject)Instantiate(
            stageTips[nextStageTip],
            new Vector3(0, 0, tipIndex * StageTipSize),
            Quaternion.identity
        );
        return stageObject;
    }

    // 一番古いステージを削除
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
}
