using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineManager : MonoBehaviour
{
    public static Dictionary<int, VineMono> Vines;
    private Transform m_content;
    private void AddVineFromID(int id)
    {
        Vines.Add(id,m_content.Find("vine" + id).GetComponent<VineMono>());
    }
    void Start()
    {
        m_content = transform.Find("Viewport/Content");
        AddVineFromID(101);
        AddVineFromID(100);
        AddVineFromID(601);
        AddVineFromID(502);
        AddVineFromID(500);
        AddVineFromID(404);
        AddVineFromID(401);
        AddVineFromID(301);
        AddVineFromID(201);
        AddVineFromID(200);
        AddVineFromID(300);
        AddVineFromID(400);
        AddVineFromID(402);
        AddVineFromID(403);
        AddVineFromID(700);
        AddVineFromID(501);
        AddVineFromID(600);
        AddVineFromID(701);
        AddVineFromID(702);
        foreach (var item in Vines)
        {
            item.Value.id = item.Key;
        }
    }

    public static VineMono GetVineFromID(int id)
    {
        if (!Vines.TryGetValue(id, out var vineMono))
        {
            return null;
        }
        
        return vineMono;
    }

    public static bool CheckVine(int id)
    {
        if (!Vines.TryGetValue(id, out var vineMono))
        {
            return false;
        }

        return vineMono.Active;
    }

}
