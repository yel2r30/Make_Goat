using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class FruitDropper : MonoBehaviour
{
    [SerializeField] private GameObject m_prefab_FruitItem;

    [SerializeField] private TMP_Text m_text_Score;
    [SerializeField] private TMP_Text m_Goat_Text;
    private int m_Goat_Count = 0;
    private int m_score = 0;

    private FruitItem m_curHoldFruitItem = null;
    private bool m_bisFruitThrowEnd = true;

    private bool m_bisGameOver = false;


    public void Update()
    {
        //// 게임 오버 상태에서는 과일을 잡거나 던지지 않음
        if (m_bisGameOver)
        {
            return;
        }

        //아무것도 잡은 것이 없을떄
        if (m_curHoldFruitItem == null && m_bisFruitThrowEnd)
        {
            //과일 생성 위치 + fruititem이라는 컴포넌트가 있는 프리팹 생성
            m_curHoldFruitItem = Instantiate(m_prefab_FruitItem, new Vector3(0.0f, transform.position.y, 0.0f), Quaternion.identity).GetComponent<FruitItem>();
            // 과일 중에 랜덤으로 하나 나옴
            m_curHoldFruitItem.Init(this, (FruitItem.FruitType)(Mathf.Floor(Mathf.Pow(20, Random.Range(0.0f, 1.0f) - 0.75f) + 0.5f)));
        }
        // 과일을 잡은 상태인데 던지지는 않음
        else if (m_curHoldFruitItem != null && m_bisFruitThrowEnd)
        {
            // 메인으로 있는 카메라를 기준으로 스크린 좌표를 3차원 좌표로 변환
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 마우스 좌표를 2D 평면에 맞춰서 과일 아이템의 위치를 업데이트 (y, z 좌표는 고정)
            m_curHoldFruitItem.transform.position = new Vector3()
            {
                x = mouseWorldPos.x,
                y = transform.position.y,
                z = 0.0f
            };

            // 과일 아이템이 화면 밖으로 나가지 않도록 제한
            if (m_curHoldFruitItem.transform.position.x < -4.5f)
            {
                m_curHoldFruitItem.transform.position = new Vector3(-4.5f, m_curHoldFruitItem.transform.position.y, 0.0f);
            }
            else if (m_curHoldFruitItem.transform.position.x > 4.5f)
            {
                m_curHoldFruitItem.transform.position = new Vector3(4.5f, m_curHoldFruitItem.transform.position.y, 0.0f);
            }

            if (Input.GetMouseButtonDown(0))
            {
                // 마우스 클릭 시 과일을 던지는 상태로 변경
                m_bisFruitThrowEnd = false;

                m_curHoldFruitItem.ActiveCollider();
                m_curHoldFruitItem.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                m_curHoldFruitItem = null;
                // 1초 후에 과일 던지기 함수 호출
                Invoke("ThrowFruit", 1.0f);
            }
        }
    }

    public void GameOver()
    {
        m_bisGameOver = true;
    }

    internal void UpgradeFruit(FruitItem fruitItem_0, FruitItem fruitItem_1)
    {
        FruitItem.FruitType curFruitType = fruitItem_0.GetFruitType();
        if (curFruitType == FruitItem.FruitType.Goat)
        {
            Destroy(fruitItem_0.gameObject);
            Destroy(fruitItem_1.gameObject);
        }
        else
        {
            Vector3 itemPos_0 = fruitItem_0.transform.position;
            Vector3 ItemPos_1 = fruitItem_1.transform.position;

            Destroy(fruitItem_0.gameObject);
            Destroy(fruitItem_1.gameObject);

            FruitItem newFruitItem = Instantiate(m_prefab_FruitItem).GetComponent<FruitItem>();
            newFruitItem.transform.position = (itemPos_0 + ItemPos_1) / 2.0f;
            newFruitItem.Init(this, (FruitItem.FruitType)((int)curFruitType + 1));
            newFruitItem.ActiveCollider();
        }

        // 점수 계산
        switch (curFruitType)
        {
            case FruitItem.FruitType.Penguin:
                m_score += 1;
                break;

            case FruitItem.FruitType.Cat:

                m_score += 3;
                break;

            case FruitItem.FruitType.Dog:
                m_score += 6;
                break;

            case FruitItem.FruitType.Hamster:

                m_score += 10;
                break;

            case FruitItem.FruitType.Aegithalos:
                m_score += 15;
                break;

            case FruitItem.FruitType.Fox:
                m_score += 21;
                break;

            case FruitItem.FruitType.Snake:
                m_score += 28;
                break;

            case FruitItem.FruitType.Weasel:
                m_score += 36;
                break;

            case FruitItem.FruitType.Squirrel:
                m_score += 45;
                break;

            case FruitItem.FruitType.Otter:
                m_score += 55;
                break;

            case FruitItem.FruitType.Goat:
                m_score += 66;
                // 염소를 만들었을때 카운트 증가
                m_Goat_Count++;
                m_Goat_Text.text = $"Goat : {m_Goat_Count}";
                break;
        }
        m_text_Score.text = $"Score : {m_score}";
    }

    private void ThrowFruit()
    {
        m_bisFruitThrowEnd = true;
    }
    
}
