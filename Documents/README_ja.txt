# EventLogListView 1.0.0

## Package Overview

文字列をリスト形式でアニメーションさせつつ表示します。

ゲーム中で起こったイベントをメッセージとして通知するための表示や、デバッグ用メッセージの表示等、様々な使い方ができます。

## How to use

使い方はシンプルで、どこからでもこのように呼び出すだけです。
使い方はシンプルで、まずは using directives を追加します。

そして、どこからでもこのように呼び出すだけです。

```
EventLog.Add("Test message");
```

ローディングインジケーターを使う場合は以下のようにします。

## Settings

細かい設定はこちらからできます。

Assets/EventLogListView/Resources/EventLogListView/EventLogData.asset

|a|a|
|--|--|
|enableDebugLog|true|
|updateMode|UnscaledTime|
|itemLimit|32|
|defaultKey|Default|
|doneKey|Done|
|errorKey|Error|
|loadingKey|Loading|
|list||

trueに設定した場合、コンソールへのデバッグログを出力する
This allows you to select when the Animator updates, and which timescale it should use.
表示するオブジェクト数の上限
デフォルトで使用するビュータイプのキー
Doneで使用するビュータイプのキー
エラーで使用するビュータイプのキー
ローディングで使用するビュータイプのキー
ビュータイプのリスト

アクセスするためのキー
テキストの色
ビューの先頭に表示する画像。nullの場合は非表示。

ビューの横幅の変更
pos.xを調整してください。

ビューの表示時間の変更
