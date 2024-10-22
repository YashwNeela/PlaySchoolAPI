using System.IO;
using UnityEngine;
using SimpleJSON;
using System.Collections;
using UnityEngine.Networking;
using System;



[SerializeField]
public class RecentGameDataTest
{
    public int TestId;
    public int scores;
    public int TotalQuestions;
    public float timeSpentInSeconds;
    public int star;
    public int attempts;
    public int Medal;
}


public class UpdateCategoryApiManagerTest 
{
    private string updatedPlayDataPathTest;        
      public string fileNameTest;
    public RecentGameDataTest _newGamesDataTest;

    public UpdateCategoryApiManagerTest()
    {
          if(!PlayerPrefs.HasKey(TMKOCPlaySchoolConstants.currentStudentPlaying)){
        PlayerPrefs.SetString(TMKOCPlaySchoolConstants.currentStudentPlaying , "TestData");
        }
        
        fileNameTest = $"{PlayerPrefs.GetString(TMKOCPlaySchoolConstants.currentStudentPlaying)} recentplaygameTest.json";
        updatedPlayDataPathTest = Path.Combine(Application.persistentDataPath, fileNameTest);
     
    }

      public void SetGameDataMore(int scores  ,int  totalQuestions , int attempts, int star, int _medals, int testId)
        {

            if(_newGamesDataTest == null)
            {
                _newGamesDataTest = new RecentGameDataTest();
            }
       
             _newGamesDataTest.TotalQuestions =totalQuestions;
                _newGamesDataTest.Medal = _medals ;
               _newGamesDataTest.star = star ;
                _newGamesDataTest.scores = scores ;
                  _newGamesDataTest.timeSpentInSeconds = Time.time;
   
               _newGamesDataTest.attempts = attempts;
              _newGamesDataTest.TestId = testId;

              SaveSpecificGameData(_newGamesDataTest);
        }
         public void SaveSpecificGameData(RecentGameDataTest newGameData)
    {
        JSONNode gameDataJson;

        if (File.Exists(updatedPlayDataPathTest))
        {
            string jsonData = File.ReadAllText(updatedPlayDataPathTest);
            gameDataJson = JSON.Parse(jsonData);
        }
        else
        {
            gameDataJson = new JSONArray();
        }

        bool gameExists = false;


        for (int i = 0; i < gameDataJson.Count; i++)
        {
            if (gameDataJson[i]["testId"].AsInt == newGameData.TestId)
            {
            
                gameDataJson[i]["score"] = newGameData.scores;
                gameDataJson[i]["totalQuestions"] = newGameData.TotalQuestions;
                gameDataJson[i]["timeSpentInSeconds"] += newGameData.timeSpentInSeconds;
                gameDataJson[i]["star"] = newGameData.star;
                gameDataJson[i]["attempts"] += newGameData.attempts;
                gameDataJson[i]["medal"] = newGameData.Medal;
                Debug.Log("Test Update Successfully");
                gameExists = true;
                break;
            }
        }

        // If the gameId does not exist, add new data
        if (!gameExists)
        {
            JSONObject newGameEntry = new JSONObject
            {
                ["testId"] = newGameData.TestId,
                ["score"] = newGameData.scores,
                ["totalQuestions"] = newGameData.TotalQuestions,
                ["timeSpentInSeconds"] = newGameData.timeSpentInSeconds,
                ["star"] = newGameData.star,
                ["attempts"] = newGameData.attempts,
                ["medal"] = newGameData.Medal
            };
      
                Debug.Log("Test Added Successfully");
            //gameDataJson.(0, newGameEntry); // Insert at first index
            gameDataJson.Add(newGameEntry);
        }

      
        File.WriteAllText(updatedPlayDataPathTest, gameDataJson.ToString());
    }

    public void CallTheApiIfOnline()
    {
          if (Application.internetReachability == NetworkReachability.NotReachable)return;
         if (File.Exists(updatedPlayDataPathTest))
        {
            string jsonData = File.ReadAllText(updatedPlayDataPathTest);
            //StartCoroutine(AddStudentGameByIdApi(jsonData));
        }
    }

 
}
    // form.AddField("StudentName", StudentName);
    //     form.AddField("Stars", stars.ToString());
    //     form.AddField("Medal", medal.ToString());
    //     form.AddField("Scores", scores.ToString());
    //     form.AddField("Attempts", attempts.ToString());
    //     form.AddField("TotalQuestions", totalQuestions.ToString());
    //     form.AddField("Streak", streak.ToString());
    //     form.AddField("TimeSpentInSeconds", timeSpentInSeconds.ToString());
    //     form.AddField("TestId", TestId.ToString());