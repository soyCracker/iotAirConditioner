# iotAirConditioner
## 智慧空調
利用手機app遠端操控家裡的冷氣。採用.net core 2.0 + asp.net core signalr來達到跨平台即時通訊，藉此達成遠端操控的目標。
* iot client選擇raspberry pi3為實作平台，使用lirc紅外線遙控套件來操控冷氣。
* asp.net core signalr 1.0目前並不能與asp.net signalr互通，因此不論是server端與client端都必須採用asp.net core signalr。
* server需安裝nuget：Microsoft.AspNetCore.SignalR
* client需安裝nuget：Microsoft.AspNetCore.SignalR.Client
