using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class Samples : MonoBehaviour
{
    public TMP_Text m_text_Sample;

    public GameObject m_prefab_Item;
    private GameObject m_curItem;

    private void Start()
    {
        m_text_Sample.text = "수박 게임 따라하기";
    }

    // 버튼 클릭하면 호출되는 함수
    public void Test()
    {
        m_text_Sample.text = "버튼 클릭";

        if(m_curItem == null)
        {
            m_curItem = Instantiate(m_prefab_Item, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Destroy(m_curItem);
        }
        
    }

}
