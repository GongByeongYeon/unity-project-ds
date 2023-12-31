using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public interface IManager
{
    void InitManager();
}
public class ManagerFactory : MonoBehaviour
{
    public static GameObject ManagerObject;
    public static IManager[] CreateManager(Type[] _typeArray, bool _canDestroy = true)
    {
        IManager[] managers = new IManager[_typeArray.Length];
        
        for (int i = 0; i < _typeArray.Length; i++)
        {
            // 매니저 생성
            ManagerObject = new GameObject(_typeArray[i].Name);
            var iManager = ManagerObject.AddComponent(_typeArray[i]) as IManager;
            if(iManager is null) // IManager 인터페이스 구현 여부 확인
                Debug.Log(_typeArray[i].Name + " is not IManager");
            else
                managers[i] = iManager;

            // 매니저 설정
            if(!_canDestroy)
                DontDestroyOnLoad(ManagerObject);
        }

        return managers;
    }
    
    public static IManager[] CreateManager(GameObject[] _prefabArray, bool _canDestroy = true)
    {
        IManager[] managers = new IManager[_prefabArray.Length];

        for (int i = 0; i < _prefabArray.Length; i++)
        {
            // 매니저 생성
            ManagerObject = Instantiate(_prefabArray[i]);
            ManagerObject.name = _prefabArray[i].name;
            var iManager = ManagerObject.GetComponent<IManager>();
            if(iManager is null)
                Debug.Log(_prefabArray[i].name + " is not IManager");
            else
                managers[i] = iManager;

            // 매니저 설정
            if(!_canDestroy)
                DontDestroyOnLoad(ManagerObject);
        }

        return managers;
    }
}
