using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuCanvas : MonoBehaviour
{
    public GameObject CollectionGalleryObj;
    public TransitionPoint TransitionPoint;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        TransitionPoint.transitionType = TransitionPoint.TransitionType.DifferentZone;
        TransitionPoint.newSceneName = "stage_1";
        TransitionPoint.transitionDestinationTag = SceneTransitionDestination.DestinationTag.A;
        SceneController.TransitionToScene(TransitionPoint);
        Services.Get<DataManager>().CreateNewPlayerData();
        Services.Get<DataManager>().SetSavePoint("stage_1", SceneTransitionDestination.DestinationTag.A);
    }

    public void ContinueGame()
    {
        TransitionPoint.transitionType = TransitionPoint.TransitionType.DifferentZone;
        TransitionPoint.newSceneName = Services.Get<DataManager>().PlayerData.saveSceneName;
        TransitionPoint.transitionDestinationTag = Services.Get<DataManager>().PlayerData.saveTransitionDestinationTag;
        SceneController.TransitionToScene(TransitionPoint);
    }

    public void OpenCollectionGallery()
    {
        CollectionGalleryObj.SetActive(true);
    }
}
