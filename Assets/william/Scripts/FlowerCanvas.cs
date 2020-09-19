using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlowerCanvas : MonoBehaviour
{
    public static FlowerCanvas Instance { get; protected set; }

    public GameObject QuickKeyPrefab;
    public Transform QuickKeyTransform;
    public string[] itemNames;
    protected const float k_KeyIconAnchorWidth = 0.11f;

    public Sprite[] sprites;

    private List<QuickKey> QuickKeys = new List<QuickKey>();
    private int setQuickKeyIndex = 0;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetInitialQuickKeyCount();
    }


    public void SetInitialQuickKeyCount()
    {

        for (int i = 0; i < 3; i++)
        {
            GameObject quickKeyPrefab = Instantiate(QuickKeyPrefab);
            quickKeyPrefab.transform.SetParent(QuickKeyTransform);
            RectTransform healthIconRect = quickKeyPrefab.transform as RectTransform;
            QuickKeys.Add(quickKeyPrefab.GetComponent<QuickKey>());
            //healthIconRect.anchoredPosition = Vector2.zero;
            //healthIconRect.sizeDelta = Vector2.zero;
            //healthIconRect.anchorMin -= new Vector2(k_KeyIconAnchorWidth, 0f) * i;
            //healthIconRect.anchorMax -= new Vector2(k_KeyIconAnchorWidth, 0f) * i;
            //m_KeyIconAnimators[i] = healthIcon.GetComponent<Animator>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeKeyUI(string itemName)
    {
        //for (int i = 0; i < keyNames.Length; i++)
        //{
        //    m_KeyIconAnimators[i].SetBool(m_HashActivePara, controller.HasItem(keyNames[i]));
        //}

        //search sprite by item name.
        foreach(var go in sprites)
        {
            if(go.name == itemName)
            {
                if(setQuickKeyIndex < QuickKeys.Count)
                {
                    QuickKeys[setQuickKeyIndex].btnImage.sprite = go;
                    setQuickKeyIndex++;
                }
            }
        }
    }
}
