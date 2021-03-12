# alice-bot-cs
A simple QQ bot running with MiraiHttp and MiraiCSharp & 一个基于MiraiHttp以及MiraiCSharp构建的基于C#编写的QQ机器人

## 概述
- `AliceBot`是继`Urara`后由很菜很菜的`MashiroSA`发起的一个自研QQ机器人项目，使用`c#`编写，基于`Mirai(Mirai Core、Mirai Console、Mirai Http、Mirai CShape)`。目前正在由`MeowCatZ`团队开发中
- `AliceBot`是运行在非盈利的基础上，一切行为仅供交流学习
> AliceBot需要借助Mirai套件下运行，且其为一个独立的机器人

## 工程文件
- `Core`：核心功能类
- `Entity`：实体类，用于实例化参数
- `Extensions`：与Modules耦合的外部功能
- `Modules`：机器人插件
- `Tools`：机器人可能会使用到的非核心的外部功能
- `Program.cs`：程序入口点
> 请注意设置.gitignore来排除.vs与.git参数文件以及/obj与/bin等输出文件夹

## 核心功能
| 功能名 | 命令 | 作用 |
| ---- | ---- | ---- |
| 早晚安 | 早安、晚安 | 给你一句早晚安的问候 |
| 随机色图 | .setu lolicon或.setu elbot或随机色图 | 发送一张色图（API来自elbot示例及lolicon） |
| 随机NB话 | .nb | 发送一句NB话（API来自elbot） |
| 事件提醒 | | 提醒群内发生的事件 |
| 随机猫猫 | .cat或随机猫猫 | 发送一张随机猫猫图 |

## 关联项目
- [Mirai](https://github.com/mamoe/mirai)
- [Mirai Console](https://github.com/mamoe/mirai-console)
- [mirai-api-http](https://github.com/project-mirai/mirai-api-http)
- [Mirai CSharp](https://github.com/Executor-Cheng/Mirai-CSharp)
- [Lolicon API](https://api.lolicon.app/)
- [Elbot](https://github.com/YunYouJun/el-bot)

## 注意事项
- 一切仅供交流学习
- 请使用c# >= 8.0建构项目
