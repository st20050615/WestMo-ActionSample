using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public NejikoController nejiko;
    public Text scoreLabel;
    public LifePanel lifePanel;

    // Update is called once per frame
    void Update()
    {
        // スコアラベルを更新
        int score = CalcScore();
        scoreLabel.text = "Score : " + score + "m";

        // ライフパネルを更新
        lifePanel.UpdateLife(nejiko.Life());
    }

    int CalcScore()
    {
        // ねじ子の走行距離をスコアとする
        return (int)nejiko.transform.position.z;
    }
}
