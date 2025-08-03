using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitItem : MonoBehaviour
{
    public enum FruitType
    {
        Penguin,
        Cat,
        Dog,
        Hamster,
        Aegithalos,
        Fox,
        Snake,
        Weasel,
        Squirrel,
        Otter,
        Goat
    }

    private FruitDropper m_fruitDropper;

    private SpriteRenderer m_spriteRenderer = null;
    private CircleCollider2D m_circleCollider2D = null;

    [SerializeField] private Sprite[] m_sprites;

    private FruitType m_curFruiitType;
    private bool m_bisCanCallUpgrade = true;

    public void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        FruitItem fruitItem = collision.transform.GetComponent<FruitItem>();
        if (fruitItem != null
            && fruitItem.m_curFruiitType == m_curFruiitType
            && m_bisCanCallUpgrade)
        {
            fruitItem.m_bisCanCallUpgrade = false;
            m_fruitDropper.UpgradeFruit(this, fruitItem);
        }
    }

    internal void Init(FruitDropper fruitDropper, FruitType fruitType)
    {
        m_fruitDropper = fruitDropper;
        m_curFruiitType = fruitType;

        m_spriteRenderer.sprite = m_sprites[(int)m_curFruiitType];
        //과일이 커질때마다 0.5씩 커짐
        transform.localScale = new Vector3(0.5f * (int)m_curFruiitType + 1.2f, 0.5f * (int)m_curFruiitType + 1.2f, 1.2f);

        m_spriteRenderer.enabled = true;
    }

    internal void ActiveCollider()
    {
        m_circleCollider2D.enabled = true;
    }

    internal FruitType GetFruitType()
    {
        return m_curFruiitType;
    }


}
