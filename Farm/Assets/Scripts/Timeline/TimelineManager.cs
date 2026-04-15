using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineManager : Singleton<TimelineManager>
{
    public PlayableDirector startDirector;
    private PlayableDirector currentDirector;
    private bool isPause;
    private bool isDone;
    public bool IsDone { set => isDone = value; }

    //标记开场动画是否已播放（静态变量跨场景保留，PlayerPrefs跨游戏会话保留）1是true,0是false
    public static bool IsOpeningTimelinePlayed
    {
        get => PlayerPrefs.GetInt("IsOpeningPlayed", 0) == 1;
        private set => PlayerPrefs.SetInt("IsOpeningPlayed", value ? 1 : 0);
    }

    protected override void Awake()
    {
        base.Awake();
        currentDirector = startDirector;
    }

    private void OnEnable()
    {
        // currentDirector.played += TimelinePlayed;
        // currentDirector.stopped += TimelineStopped;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
    }

    private void Update()
    {
        if (isPause && Input.GetKeyDown(KeyCode.Space))
        {
            isPause = false;
            currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(1d);
        }
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }

    private void OnAfterSceneLoadedEvent()
    {
        currentDirector = FindFirstObjectByType<PlayableDirector>();
        if (currentDirector != null && currentDirector == startDirector && !IsOpeningTimelinePlayed)
        {
            currentDirector.Play();
            IsOpeningTimelinePlayed = true;
        }
    }

    public void PauseTimeline(PlayableDirector director)
    {
        currentDirector = director;
        currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
        isPause = true;
    }

    public static void ResetOpeningTimelineState()
    {
        IsOpeningTimelinePlayed = false;
    }

    // private void TimelinePlayed(PlayableDirector director)
    // {
    //     if (director != null)
    //         EventHandler.CallUpdateGameStateEvent(GameState.Pause);
    // }


    // private void TimelineStopped(PlayableDirector director)
    // {
    //     if (director != null)
    //     {
    //         EventHandler.CallUpdateGameStateEvent(GameState.GamePlay);
    //         director.gameObject.SetActive(false);
    //     }
    // }

}
