# Space Air Hockey
コンピューターと対戦するエアホッケーのゲームです。
様々なアイテムがあるため、単純なパックの打ち合いだけでなくアイテムを利用した戦い方が求められます。（設定でオフにできます）

# Image＆Application
https://suzusho130.github.io/WebGLForUnity/.

# Features
基本はパックの打ち合いなどの基本動作は物理エンジンで実現しています。

後述のイベント[流星群]ではオブジェクトが大量に出現するため、負荷を下げるためにオブジェクトプールを導入しています。

# Requirement
+ Unity 2019.3.6f1

# Play
遊び方
1. 上記URLをクリック
2. タイトル画面[Setting]をクリックして、好みに設定し[OK]をクリック
3. タイトル画面[Start]をクリックしてゲームを開始
4. 
    + マウス　　：ストライカーの操作
    + スクロール：ストライカーの移動速度を変更
    + 右クリック：ストライカーの位置を初期化
---
アイテム

+ ミニパック：ゴールに入るか、一定時間経過で消滅するパックです。
+ 得点星　　：パックを当てることでポイントが得られます。
+ 衛星　　　：一定時間自動迎撃する衛星です。複数取ることで時間を延長します。
---
イベント

+ 流れ星:一定間隔で空からアイテムが降ってきます。何が出現するかはランダムです。
+ 流星群：稀に空から大量に流れ星が降ってきます。

---
得点
|名前|点数|
|----|----|
|パック|3点|
|ミニパック|1点|
|得点星（赤）|10点|
|得点星（黄）|3点|
|得点星（青）|1点|

---
ゲーム画面見方

+ 左側のスコアボード：プレイヤーの獲得スコア
+ 右側のスコアボード：CPU側の獲得スコア
+ 上部バー　　　　　：残り時間
