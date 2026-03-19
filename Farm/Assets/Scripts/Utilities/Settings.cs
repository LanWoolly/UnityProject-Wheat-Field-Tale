using System.Collections;
using System.Collections.Generic;
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
}
