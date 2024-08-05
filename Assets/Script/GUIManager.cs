using UnityEngine;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager>
{
    [SerializeField] private GameObject homeGUI;
    [SerializeField] private GameObject gameGUI;

    [SerializeField] private Transform lifeGrid;
    [SerializeField] private GameObject lifeIconPrefab;

    [SerializeField] private ImageFilled levelProgressBar;
    [SerializeField] private ImageFilled hpProgressBar;

    [SerializeField] private Text hpCountingTxt;
    [SerializeField] private Text XpCountingTxt;
    [SerializeField] private Text levelCountingTxt;
    [SerializeField] private Text coinCountingTxt;
    [SerializeField] private Text reloadTxt;

    [SerializeField] private Dialog gunUpgradeDialog;
    [SerializeField] private Dialog gameOverDialog;

    private Dialog activeDialog;

    public Dialog ActiveDialog { get => activeDialog; private set => activeDialog = value; }

    protected override void Awake()
    {
        MakeSingleton(false);
    }
    public void ShowGameGUI(bool isShow)
    {
        if (gameGUI != null)
        {
            gameGUI.gameObject.SetActive(isShow);
        }
        if(homeGUI != null) 
        {
            homeGUI.gameObject.SetActive(!isShow);
        }
    }
    public void ShowDialog(Dialog dialog)
    {
        if (dialog == null) return;
        activeDialog = dialog;
        activeDialog.Show(true);
    }
    public void ShowGunUpgradeDialog()
    {
        ShowDialog(gunUpgradeDialog);
    }
    public void ShowGameOverDialog()
    {
        ShowDialog(gameOverDialog);
    }
    public void UpdateLifeInfo(int life)
    {
        if(lifeGrid == null) return;

        ClearLifeGrid();

        DrawLifeGrid(life);
    }

    private void DrawLifeGrid(int life)
    {
        if (lifeGrid == null || lifeIconPrefab == null) return;
        for(int i = 0; i < life; i++)
        {
            var lifeItem = Instantiate(lifeIconPrefab,Vector3.zero, Quaternion.identity);
            lifeItem.transform.SetParent(lifeGrid);
            lifeItem.transform.localScale = Vector3.one;
        }    
    }

    private void ClearLifeGrid()
    {
        if (lifeGrid == null) return;
        int lifeCounting = lifeGrid.childCount;
        for (int i = 0; i < lifeCounting ; i++)
        {
            var lifeItem = lifeGrid.GetChild(i);
            if (lifeItem != null)
            {
                Destroy(lifeItem.gameObject);
            }
        }
    }
    public void UpdateLevelInfo(int currentLevel, float currentXp, float levelUpXpRequire)
    {
        levelProgressBar?.UpdateValue(currentXp, levelUpXpRequire);

        if(levelCountingTxt != null)
        {
            levelCountingTxt.text = $"LEVEL  {currentLevel.ToString("00")}";
        }
        if(XpCountingTxt != null)
        {
            XpCountingTxt.text = $"{currentXp.ToString("00")} / {levelUpXpRequire.ToString("00")}";
        }
    }
    public void UpdateHpInfo(float currentHp, float maxHp)
    {
        hpProgressBar?.UpdateValue(currentHp, maxHp);

        if(hpCountingTxt != null)
        {
            hpCountingTxt.text = $"{currentHp.ToString("00")} / {maxHp.ToString("00")}";
        }
    }
    public void UpdateCoinCounting(int coin)
    {
        if(coinCountingTxt != null)
        {
            coinCountingTxt.text = coin.ToString("n0");
        }
    }
    public void ShowReloadTxt(bool isShow)
    {
        if(reloadTxt != null)
        {
            reloadTxt.gameObject.SetActive(isShow);
        }
    }
}
