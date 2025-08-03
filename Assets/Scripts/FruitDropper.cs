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
        //// ���� ���� ���¿����� ������ ��ų� ������ ����
        if (m_bisGameOver)
        {
            return;
        }

        //�ƹ��͵� ���� ���� ������
        if (m_curHoldFruitItem == null && m_bisFruitThrowEnd)
        {
            //���� ���� ��ġ + fruititem�̶�� ������Ʈ�� �ִ� ������ ����
            m_curHoldFruitItem = Instantiate(m_prefab_FruitItem, new Vector3(0.0f, transform.position.y, 0.0f), Quaternion.identity).GetComponent<FruitItem>();
            // ���� �߿� �������� �ϳ� ����
            m_curHoldFruitItem.Init(this, (FruitItem.FruitType)(Mathf.Floor(Mathf.Pow(20, Random.Range(0.0f, 1.0f) - 0.75f) + 0.5f)));
        }
        // ������ ���� �����ε� �������� ����
        else if (m_curHoldFruitItem != null && m_bisFruitThrowEnd)
        {
            // �������� �ִ� ī�޶� �������� ��ũ�� ��ǥ�� 3���� ��ǥ�� ��ȯ
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ���콺 ��ǥ�� 2D ��鿡 ���缭 ���� �������� ��ġ�� ������Ʈ (y, z ��ǥ�� ����)
            m_curHoldFruitItem.transform.position = new Vector3()
            {
                x = mouseWorldPos.x,
                y = transform.position.y,
                z = 0.0f
            };

            // ���� �������� ȭ�� ������ ������ �ʵ��� ����
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
                // ���콺 Ŭ�� �� ������ ������ ���·� ����
                m_bisFruitThrowEnd = false;

                m_curHoldFruitItem.ActiveCollider();
                m_curHoldFruitItem.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                m_curHoldFruitItem = null;
                // 1�� �Ŀ� ���� ������ �Լ� ȣ��
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

        // ���� ���
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
                // ���Ҹ� ��������� ī��Ʈ ����
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
