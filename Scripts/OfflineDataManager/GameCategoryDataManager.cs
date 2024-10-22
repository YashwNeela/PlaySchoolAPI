
using System.IO;
using UnityEngine;
using System;
using SimpleJSON;
using Unity.VisualScripting;

[Serializable]
public class GameCategoryDataSaver
{
    public int gameId;
    public int completedLevels;
    public int star;
    public int score;
}

public  class GameCategoryDataManager
{
    private  string gameDataPath;
    private  GameCategoryDataSaver _newGameData;
    private  GameCategoryDataSaver _GameDataload;
    public  int GetCompletedLevel => _GameDataload.completedLevels;
    public  int Getstar => _GameDataload.star;

    public int GameId;
    public GameCategoryDataManager(int gameId)
    {
        if (!PlayerPrefs.HasKey(TMKOCPlaySchoolConstants.currentStudentPlaying))
        {
            PlayerPrefs.SetString(TMKOCPlaySchoolConstants.currentStudentPlaying, "TestData");
        }
        string fileName = $"{PlayerPrefs.GetString(TMKOCPlaySchoolConstants.currentStudentPlaying)} gamedata.json";
        gameDataPath = Path.Combine(Application.persistentDataPath, fileName);
        GameId = gameId;
        LoadSpecificGameData();
     
    }


    public  string GetFileName(string StudentName)
    {
        return Path.Combine(Application.persistentDataPath, $"{StudentName} gamedata.json");
    }


        public  void SaveLevel(int levelNum , int totalLevel)
        {
            if (_newGameData == null)
            {
                _newGameData = new GameCategoryDataSaver();
            }
     
             int _star = CalculateStars(levelNum , totalLevel);
            if(Getstar == 5 ||  _newGameData.star == 5)
            {
                _newGameData.star = 5;
                   
            }else{
    
            _newGameData.star = _star;
            }
            _newGameData.completedLevels = levelNum;
            
            _newGameData.gameId = GameId;

            SaveSpecificGameData(_newGameData);
        }

          public int CalculateStars(int completedLevel, int totalLevel)
            {
                float percentage = (float)completedLevel / totalLevel * 100;
                if (percentage >= 20 && percentage < 40) return 1;
                else if (percentage >= 40 && percentage <60) return 2;
                else if (percentage >= 60 && percentage < 80) return 3;
                else if (percentage >= 80 && percentage < 100) return 4;
                else if (percentage == 100) return 5;

                return 0;
            }
    public  void LoadSpecificGameData()
    {
        string getFileNameWithStudent = GetFileName(PlayerPrefs.GetString(TMKOCPlaySchoolConstants.currentStudentPlaying));
        if (File.Exists(getFileNameWithStudent))
        {
            string jsonData = File.ReadAllText(getFileNameWithStudent);
            JSONNode loadedJson = JSON.Parse(jsonData);

            if (loadedJson != null)
            {
                foreach (JSONNode gameData in loadedJson.AsArray)
                {
                    if (gameData["gameId"].AsInt == GameId)
                    {
                        _GameDataload = new GameCategoryDataSaver
                        {
                            gameId = gameData["gameId"].AsInt,
                            completedLevels = gameData["completedLevels"].AsInt,
                           star = gameData["star"].AsInt,
                            score = gameData["score"].AsInt
                        };
                       // GetCompletedLevel = _GameDataload.completedLevels;
                        Debug.Log($"Loaded Data for {GameId}");
                        return;
                    }
                }
            }
              _GameDataload = new GameCategoryDataSaver();
            Debug.Log($"No data found for {GameId} ID {GameId}");
        }else{
            
                _GameDataload = new GameCategoryDataSaver();
             Debug.Log($"No File Found in Path a");
        }
    }

     private  void SaveSpecificGameData(GameCategoryDataSaver newGameData)
    {
        JSONNode gameDataJson;

        if (File.Exists(gameDataPath))
        {
            string jsonData = File.ReadAllText(gameDataPath);
            gameDataJson = JSON.Parse(jsonData);
        }
        else
        {
            gameDataJson = new JSONArray();
        }

        bool gameExists = false;

        for (int i = 0; i < gameDataJson.Count; i++)
        {
            if (gameDataJson[i]["gameId"].AsInt == newGameData.gameId)
            {
                gameDataJson[i]["completedLevels"] = newGameData.completedLevels;
                gameDataJson[i]["star"] = newGameData.star;
                gameDataJson[i]["score"] = newGameData.score;
                gameExists = true;
                       Debug.Log("Game Update Successfully");
                break;
            }
        }

        if (!gameExists)
        {
            JSONObject newGameEntry = new JSONObject
            {
                ["gameId"] = newGameData.gameId,
                ["completedLevels"] = newGameData.completedLevels,
                 ["star"] = newGameData.star,
                ["score"] = newGameData.score
            };
                   Debug.Log("Game Added Successfully");
            gameDataJson.Add(newGameEntry);
        }

        File.WriteAllText(gameDataPath, gameDataJson.ToString());
        Debug.Log("Game data saved or updated!");
    }
}
