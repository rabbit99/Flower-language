using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MainSystem : Singleton<MainSystem>
{
    
    private const int CHECK_NETWORK_EVERY_X_FRAME = 30;
    private static bool m_servicesInited = false;
    private EventManager m_eventManager;
    private StateManager m_stateManager;
    public StateManager StateManager
    {
        get { return m_stateManager; }
    }
    private StoryManager m_storyManager;
    public StoryManager StoryManager
    {
        get { return m_storyManager; }
    }
    protected GameEventListener m_eventListener;

    private int m_frame = -1;
    private float m_deltaTime;

    public Canvas worldCanvas;
    public Canvas canvas;
    public PostProcessVolume ppVolume;
    public Cinemachine.CinemachineBrain cinemachineBrain;
    public GameObject audioController;
    public GameConfig gameConfig;

    private bool switchSettingUIController = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        Debug.Log("MainSystem start");
        if (!m_servicesInited)
        {
            m_eventManager = new EventManager();
            m_stateManager = new StateManager();

            m_eventListener = new GameEventListener();

            Services.Set<EventManager>(m_eventManager);
            Services.Set<UIManager>(new UIManager(worldCanvas, canvas));
            Services.Set<ResourcesManager>(new ResourcesManager());
            Services.Set<DataManager>(new DataManager());
            //Services.Set<ActionManager>(new ActionManager(gameConfig.gameDataFilePaths));

            //m_storyManager = new StoryManager(gameConfig.storyControllerPath);
            //Services.Set<StoryManager>(m_storyManager);
        }

        //m_eventListener.ListenForEvent(EGameEvents.GotTheClue, OnGotTheClue);
        //Services.Get<DataManager>().PlayerData.savePointName = "start";

        //Services.Get<DataManager>().SetSavePoint("Start 1", SceneTransitionDestination.DestinationTag.A);

        //m_stateManager.ChangeState(new InitLoadingState(m_stateManager, this));
    }

    //private EventResult OnGotTheClue(object eventData)
    //{
    //    EventResult eventresult = new EventResult(false);
    //    m_storyManager.ClueProcess(eventData);

    //    //TODO 在DataManager Set
    //    Services.Get<DataManager>().SetClueHaveGotten((string)eventData);

    //    return eventresult;
    //}

    void Update()
    {
        //TO DO
        //multiple language
        //LocalizationManager.Update();

        m_frame++;
        if (m_frame % CHECK_NETWORK_EVERY_X_FRAME == 0)
        {
            //TO DO
            //CkeckNetwork();
        }

        m_deltaTime = Time.deltaTime;
#if UNITY_EDITOR
#if UNITY_5_5_OR_NEWER
        UnityEngine.Profiling.Profiler.BeginSample("State Manager  Update");
#else
			Profiler.BeginSample("State Manager  Update");
#endif
#endif

        if (m_stateManager != null)
        {
            m_stateManager.Update(m_deltaTime);
        }

#if UNITY_EDITOR
#if UNITY_5_5_OR_NEWER
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.BeginSample("Input Manager  Update");
#else
			Profiler.EndSample();
			Profiler.BeginSample("Input Manager  Update");
#endif
#endif

        //TO DO
        //other input?
        //if (m_inputMgr != null)
        //{
        //    m_inputMgr.Update(m_deltaTime);
        //}

#if UNITY_EDITOR
#if UNITY_5_5_OR_NEWER
        UnityEngine.Profiling.Profiler.EndSample();
#else
			Profiler.EndSample();
#endif
#endif

        if (m_eventManager != null)
        {
            m_eventManager.SendEvent((int)EGameEvents.MainGame_Update, Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (m_eventManager != null)
        {
            m_eventManager.SendEvent((int)EGameEvents.MainGame_FixedUpdate, Time.deltaTime);
        }
    }

    void LateUpdate()
    {
        if (m_eventManager != null)
        {
            m_eventManager.SendEvent((int)EGameEvents.MainGame_LateUpdate, Time.deltaTime);
        }
    }
}
