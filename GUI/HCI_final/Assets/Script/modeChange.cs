using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class modeChange : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    void Start()
    {
        GameObject btnObj = GameObject.Find("Button_mode1");
        Button btn = btnObj.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            this.GoGameScene(btnObj);
        });
    }

    Image icon;
    Sprite sp;
    void showPic(string buttonname)
    {
        icon = GameObject.Find("Image_mode").GetComponent<Image>();
        switch (buttonname)
        {
            case "Button_mode1":
                sp= Resources.Load("image/mode1_city", typeof(Sprite)) as Sprite;
                icon.overrideSprite = sp;
                break;
            case "Button_mode2":
                sp = Resources.Load("image/mode2_forest", typeof(Sprite)) as Sprite;
                icon.overrideSprite = sp;
                break;
            case "Button_mode3":
                sp = Resources.Load("image/mode3_univers", typeof(Sprite)) as Sprite;
                icon.overrideSprite = sp;
                break;
        }
    }
    
    void hidePic(string buttonname)
    {
        icon = GameObject.Find("Image_mode").GetComponent<Image>();
        sp = Resources.Load("image/mode_cover", typeof(Sprite)) as Sprite;
        icon.overrideSprite = sp;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //当鼠标进入button范围 name即为button名称
        showPic(name);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        //当鼠标移出button范围 name即为button名称
        hidePic(name);
    }

    public void GoGameScene(GameObject NScene)
    {
        SceneManager.LoadScene("GameScene");
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
