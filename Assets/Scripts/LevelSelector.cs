using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public GameObject Worlds;
    public Sprite StarWon;
    public Sprite StarEmpty;
    public GameObject TextStarsNumber;
    public GameObject LevelPrefab;

    private int worldIndex = 0;
    private int maxWorldIndex = 2;

    private DataController dataController;

    private void Start()
    {
        dataController = GameObject.FindObjectOfType<DataController>();

        dataController.LoadData();
        FillWorldsLevels();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void NextWorld()
    {
        worldIndex++;
        if (worldIndex > maxWorldIndex)
        {
            worldIndex = maxWorldIndex;
            return;
        }
        Worlds.GetComponent<RectTransform>().anchoredPosition = new Vector2(Worlds.GetComponent<RectTransform>().anchoredPosition.x - 550f, Worlds.GetComponent<RectTransform>().anchoredPosition.y);

    }
    public void PreviewWorld()
    {
        worldIndex--;
        if (worldIndex < 0)
        {
            worldIndex = 0;
            return;
        }
        Worlds.GetComponent<RectTransform>().anchoredPosition = new Vector2(Worlds.GetComponent<RectTransform>().anchoredPosition.x + 550f, Worlds.GetComponent<RectTransform>().anchoredPosition.y);
    }

    private void FillWorldsLevels()
    {
        Player playerData = dataController.GetPlayer();

        TextStarsNumber.GetComponent<Text>().text = "" + playerData.numberOfStars;

        int levelIndex = 0;
        Level[] DataControllerLevels = dataController.GetLevels();
        foreach (Transform world in Worlds.transform)
        {
            GameObject LevelsContainer = world.Find("LevelsContainer").gameObject;

            List<Level> worldLevels = new List<Level>();
            for (int i = levelIndex; i < levelIndex + 9; i++)
            {
                if (i >= DataControllerLevels.Length)
                    break;
                worldLevels.Add(DataControllerLevels[i]);
            }

            int row = 0;
            int col = 0;
            int startX = -20;
            int marginX = 135;
            foreach (var level in worldLevels)
            {
                GameObject levelP = Instantiate(LevelPrefab) as GameObject;
                levelP.transform.SetParent(LevelsContainer.transform);

                int posY = (row == 0) ? 20 : (row == 1) ? -120 : -260;
                levelP.GetComponent<RectTransform>().localPosition = new Vector2(startX+(marginX*col),posY);
                levelP.GetComponent<RectTransform>().localScale = new Vector2(1f,1f);

                GameObject LockedContainer = levelP.transform.Find("Locked").gameObject;
                GameObject UnlockedContainer = levelP.transform.Find("Unlocked").gameObject;
                UnlockedContainer.GetComponent<Button>().onClick.AddListener(delegate { GameObject.FindObjectOfType<LevelManager>().LoadGame(level.index); });

                if (level.locked)
                {
                    LockedContainer.SetActive(true);
                    UnlockedContainer.SetActive(false);

                    int starsLeftToUnlockLevel = level.starsToUnlock;
                    LockedContainer.transform.Find("Text").GetComponent<Text>().text = "" + starsLeftToUnlockLevel;
                }
                else
                {
                    LockedContainer.SetActive(false);
                    UnlockedContainer.SetActive(true);

                    GameObject textLevelNumber = UnlockedContainer.transform.Find("Number").gameObject;
                    textLevelNumber.GetComponent<Text>().text = "" + (levelIndex + 1);

                    GameObject starsContainer = UnlockedContainer.transform.Find("StarsContainer").gameObject;

                    int starIndex = 0;
                    int numberOfStars = level.stars;
                    foreach (Transform Star in starsContainer.transform)
                    {
                        if (starIndex > numberOfStars - 1)
                            Star.gameObject.GetComponent<Image>().sprite = StarEmpty;
                        else
                            Star.gameObject.GetComponent<Image>().sprite = StarWon;
                        starIndex++;
                    }
                }

                levelIndex++;
                col++;
                if(col > 2)
                {
                    col = 0;
                    row++;
                }
            }
        }
    }
}
