using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutines : MonoBehaviour
{

    private string _text;
    private List<char> _list = new List<char> {'a', 'b'};
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(StartCount(_list));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _list.Add('x');
        }
    }

    private IEnumerator StartCount(List<char> list)
    {
        for (var i = 0; i < 10; ++i)
        {
            Debug.Log(list.Count);
            yield return new WaitForSeconds(1);
        }
        list.Clear();
    }
}
