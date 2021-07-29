# EventLogListView

## Package Overview

The character string is displayed while being animated in a list format.

You can use it in various ways, such as displaying an event that occurred in the game as a message or displaying a debugging message.

---

## How to use

It's simple to use, first add using directives.

```
using EventLogListView;
```

And just call it like this from anywhere.

```
EventLog.Add("Test message");
```

To use the loading indicator, do as follows.

```
var eventLog = EventLog.AddLoading("Loading...");
await Task.Delay(1000);
eventLog.Done("Success");
```

---

## Settings

You can make detailed settings here.

```
Assets/EventLogListView/Resources/EventLogListView/EventLogData.asset
```

### ```EventLogData```

|name|initial value|description|
|:---|:---:|:---|
|enableDebugLog|true|If set to true, output debug log to console.|
|updateMode|UnscaledTime|This allows you to select when the Animator updates, and which timescale it should use.|
|itemLimit|32|Maximum number of objects to display.|
|defaultKey|"Default"|```Viewtype``` key to use by default.|
|doneKey|"Done"|```Viewtype``` key used in done.|
|errorKey|"Error"|```Viewtype``` key used in error.|
|loadingKey|"Loading"|```Viewtype``` key used for loading.|
|list||List of ```Viewtype```.|

### ```EventLogData.ViewType```

|name|description|
|:---|:---|
|key|Key to access.|
|color|Text color.|
|sprite|The image to display at the top of the view. Hide if null.|

---

## Change the width of the view

Adjust ```Anchored Position.x```.

```
Assets/EventLogListView/Animations/EventLogAppear.anim
Assets/EventLogListView/Animations/EventLogDisappear.anim
```

---

## Change view display time

Adjust key frame.

```
Assets/EventLogListView/Animations/EventLogDisappear.anim
```

---
https://ebsoft.jp  
(c) 2021 eB Soft Inc.
