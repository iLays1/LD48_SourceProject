using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerActionButton : MonoBehaviour
{
    public UnityEvent OnClick = new UnityEvent();
    public UnitAction action;
    public bool active = true;

    [Space]
    public TextMeshProUGUI text;
    public Image iconImage;
    public TextMeshProUGUI cdtext;
    public Image cdCover;

    SkillSelection selector;
    Collider2D col;
    int coolDown = 0;

    private void Awake()
    {
        selector = FindObjectOfType<SkillSelection>();
        col = GetComponent<Collider2D>();

        TickManager.OnTick.AddListener(TickEvent);
    }
    private void Start()
    {
        SetActive(true);
    }

    private void LateUpdate()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            
            if (touch.phase == TouchPhase.Began)
            {
                float extra = 0.1f;

                if (touchPos.x < transform.position.x + (col.bounds.size.x / 2) + extra && 
                   touchPos.x > transform.position.x - (col.bounds.size.x / 2) - extra &&
                   touchPos.y < transform.position.y + (col.bounds.size.y / 2) + extra &&
                   touchPos.y > transform.position.y - (col.bounds.size.y / 2) - extra)
                {
                    ClickButton();
                }
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickButton();
        }
    }

    public bool UseAction(FallingUnit user, int dir)
    {
        bool skillUsed = action.Do(user, dir);

        if (skillUsed && action.coolDown > 0)
        {
            SetActive(false);
            coolDown = action.coolDown + 1;
            cdtext.text = $"{action.coolDown}";
        }

        return skillUsed;
    }

    public void LoadButton(UnitAction action)
    {
        if(action == null)
        {
            gameObject.SetActive(false);
            return;
        }

        this.action = action;
        text.text = action.displayName;
        iconImage.sprite = action.icon;
    }

    public void ClickButton()
    {
        if (!active) return;

        //Debug.Log(name);
        MainMobileController.CallCancel.Invoke();
        OnClick.Invoke();
    }

    public void TickEvent()
    {
        if(coolDown > 0)
        {
            coolDown--;
            cdtext.text = coolDown.ToString();
            if (coolDown == 0)
                SetActive(true);
        }
    }

    public void SetActive(bool active)
    {
        this.active = active;
        cdCover.gameObject.SetActive(!active);
        cdtext.gameObject.SetActive(!active);

        if (!active)
        {
            if (SkillSelection.selectedAction == this)
                selector.SetSkill(0);
        }
    }
}
