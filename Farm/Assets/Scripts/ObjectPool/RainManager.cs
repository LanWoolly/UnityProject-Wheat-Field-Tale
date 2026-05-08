using System;
using UnityEngine.Pool;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RainManager : Singleton<RainManager>
{
    // 下雨时长配置（游戏分钟）
    [Header("下雨配置")]
    public float minRainDuration = 45;
    public float maxRainDuration = 520;
    // 每游戏小时触发下雨的概率（0-1）
    public float rainTriggerProb = 0.9f;
    public AudioSource ambientMusic;

    private bool isSummer = false; // 是否为夏季
    private bool isRaining = false; // 当前是否在下雨
    private bool isIndoorScene = false; // 当前是否为室内场景
    private Coroutine rainCoroutine; // 下雨协程
    private GameObject rainEffectObj; // 当前激活的下雨特效对象
    private ObjectPool<GameObject> rainPool; // 下雨特效对象池

    private void OnEnable()
    {
        // 监听游戏时间/季节更新
        EventHandler.GameDateEvent += OnGameDateEvent;
        // 监听场景加载完成
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
    }

    private void Update()
    {
        ambientMusic.gameObject.SetActive(isRaining);
    }

    private void OnDisable()
    {
        EventHandler.GameDateEvent -= OnGameDateEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        StopRain();
    }

    /// <summary>
    /// 初始化下雨特效对象池
    /// </summary>
    private void InitRainPool()
    {
        PoolManager poolManager = PoolManager.Instance;
        if (poolManager != null && poolManager.poolEffectList.Count >= 5)
        {
            rainPool = poolManager.poolEffectList[5]; // 对应Rain的索引5
        }
        else
        {
            Debug.LogError("下雨特效对象池未找到！请检查PoolManager的poolPrefabs列表");
        }
    }

    /// <summary>
    /// 监听场景加载，检测是否为室内场景
    /// </summary>
    private void OnAfterSceneLoadedEvent()
    {
        // 检测当前激活场景是否为室内（约定：室内场景名称包含"Indoor"）
        Scene currentScene = SceneManager.GetActiveScene();
        isIndoorScene = currentScene.name.Contains("Home");

        // 室内直接关闭下雨
        if (isIndoorScene)
        {
            StopRain();
        }
    }

    /// <summary>
    /// 监听游戏时间/季节更新
    /// </summary>
    private void OnGameDateEvent(int hour, int day, int month, int year, Season season)
    {
        // 更新季节状态
        isSummer = season == Season.夏天;

        // 非夏季：关闭下雨
        if (!isSummer)
        {
            StopRain();
            return;
        }

        // 室内：跳过触发逻辑
        if (isIndoorScene) return;

        // 仅在每小时整点时判断是否触发下雨（避免频繁判断）
        if (hour % 1 == 0 && !isRaining)
        {
            // 初始化对象池
            InitRainPool();

            TryTriggerRain();
        }
    }

    /// <summary>
    /// 随机触发下雨
    /// </summary>
    private void TryTriggerRain()
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        if (randomValue <= rainTriggerProb)
        {
            StartRain();
        }
    }

    /// <summary>
    /// 开始下雨
    /// </summary>
    private void StartRain()
    {
        if (isRaining || rainPool == null || isIndoorScene) return;

        isRaining = true;
        // 从对象池取出下雨特效
        rainEffectObj = rainPool.Get();
        rainEffectObj.transform.position = new Vector3(3, 21, 0); // 调整特效位置（根据你的场景修改）
        // 启动协程控制下雨时长
        rainCoroutine = StartCoroutine(RainDurationCoroutine());
    }

    /// <summary>
    /// 控制下雨时长的协程
    /// </summary>
    private IEnumerator RainDurationCoroutine()
    {
        // 随机下雨时长（转换为游戏时间：1游戏分钟 = 实际秒数（根据Settings.secondThreshold））
        float rainDuration = UnityEngine.Random.Range(minRainDuration, maxRainDuration);
        // 转换为实际等待时间：假设Settings.secondThreshold是1秒对应1游戏秒，1游戏分钟=60实际秒
        float realDuration = rainDuration * 60 * Settings.secondThreshold;

        yield return new WaitForSeconds(realDuration);

        // 时长结束后停止下雨
        StopRain();
    }

    /// <summary>
    /// 停止下雨并释放对象池
    /// </summary>
    public void StopRain()
    {
        if (!isRaining) return;

        isRaining = false;
        // 停止协程
        if (rainCoroutine != null)
        {
            StopCoroutine(rainCoroutine);
            rainCoroutine = null;
        }
        // 释放下雨特效到对象池
        if (rainEffectObj != null && rainPool != null)
        {
            rainPool.Release(rainEffectObj);
            rainEffectObj = null;
        }
    }
}