using System;
using UnityEngine;

public class Settings
{
    public const float itemFadeDuration = 0.35f;
    public const float targetAlpha = 0.45f;

    //时间相关
    public const float secondThreshold = 0.012f; //数值越小时间越快
    public const int secondHold = 59;
    public const int minuteHold = 59;
    public const int hourHold = 23;
    public const int dayHold = 15; //15天就是一个月
    public const int seasonHold = 3; //每过3个季节回到第一个季节

    //场景过渡
    public const float fadeDuration = 1.5f;

    //割草数量限制
    public const int reapAmount = 2;

    public const float gridCellSize = 1;
    public const float gridCellDiagonalSize = 1.41f;
    public const float pixelSize = 0.05f; //20*20占1unit
    public const float animationBreakTime = 5f; //动画间隔时间
    public const int maxGridSize = 999;

    //灯光
    public const float lightChangeDuration = 25f;
    public static TimeSpan morningTime = new TimeSpan(5, 0, 0);
    public static TimeSpan nightTime = new TimeSpan(19, 0, 0);

    public static Vector3 playerStartPos = new Vector3(0, -7f, 0);
    public const int playerStartMoney = 100;
}
