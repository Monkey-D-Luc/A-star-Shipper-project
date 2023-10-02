using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PlayeSceneUIHandler playeSceneUIHandler;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI goalText;
    private int goal;
    private int money;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        goal = 200;
        money = 1;
        moneyText.SetText("Money: " + money.ToString() + ".000 VND");
        goalText.SetText("Goal: " + goal.ToString() + ".000 VND");
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (money >= goal)
        {
            playeSceneUIHandler.DisplayGameoverScreen(true);
            Time.timeScale = 0;
        }
        if (money <= 0)
        {
            playeSceneUIHandler.DisplayGameoverScreen(false);
            Time.timeScale = 0;
        }
    }

    public void GainMoney(Food foodType)
    {
        switch (foodType)
        {
            case Food.Pizza: money += 100; break;
            case Food.BanhMi: money += 50; break;
        }
        moneyText.SetText("Money: " + money.ToString() + ".000 VND");
    }

    public void LoseMoney(Food foodType)
    {
        switch (foodType)
        {
            case Food.Pizza: money -= 100; break;
            case Food.BanhMi: money -= 50; break;
        }
        moneyText.SetText("Money: " + money.ToString() + ".000 VND");
    }
}
