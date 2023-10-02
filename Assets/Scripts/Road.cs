using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    private int currentTrafficLevel;
    public int edgeID;

    public Road()
    {
        edgeID = -1;
        currentTrafficLevel = 0;
    }

    private void Update()
    {
        int trafficLevel = Astar.trafficLevelList[edgeID];
        if (currentTrafficLevel == trafficLevel)
            return;
        switch (trafficLevel)
        {
            case 1: renderer.material.SetColor("_Color", Color.green); break;
            case 2: renderer.material.SetColor("_Color", Color.yellow); break;
            case 3: renderer.material.SetColor("_Color", new Color(1.0f, 0.64f, 0.0f)); break;
            case 4: renderer.material.SetColor("_Color", Color.red); break;
        }
    }
}
