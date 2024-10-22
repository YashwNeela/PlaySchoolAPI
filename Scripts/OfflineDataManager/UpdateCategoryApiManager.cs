using System.IO;
using UnityEngine;
using SimpleJSON;
using System.Collections;
using UnityEngine.Networking;
using System;


[Serializable]
public class RecentGameData
{
    public int gameId;
    public int score;
    public int totalLevel;
    public int completedLevel;
    public float timeSpentInSeconds;
    public int star;
    public int attempts;
}



public class UpdateCategoryApiManager 
{

   
    private string updatedPlayDataPath;
      public string fileName;

    public RecentGameData _newGamesData;

    public int gameId;

    public UpdateCategoryApiManager(int _gameId)
    {
       

    

          if(!PlayerPrefs.HasKey(TMKOCPlaySchoolConstants.currentStudentPlaying)){
        PlayerPrefs.SetString(TMKOCPlaySchoolConstants.currentStudentPlaying , "TestData");
        }
        
        fileName = $"{PlayerPrefs.GetString(TMKOCPlaySchoolConstants.currentStudentPlaying)} recentplaygame.json";
        updatedPlayDataPath = Path.Combine(Application.persistentDataPath, fileName);
        gameId = _gameId;
     
    }

      public void SetGameDataMore(int levelNum  ,int  totalLevel , int attempts, int star)
        {

            if(_newGamesData == null)
            {
                _newGamesData = new RecentGameData();
            }
       
      
         
      
                    _newGamesData.completedLevel = levelNum;
                _newGamesData.totalLevel = totalLevel;
                _newGamesData.star = star;

        
          
                  _newGamesData.timeSpentInSeconds = Time.time;
   
               _newGamesData.attempts = attempts;
              _newGamesData.gameId = gameId;

              SaveSpecificGameData(_newGamesData);
        }
 
   public void SaveSpecificGameData(RecentGameData newGameData)
    {
        JSONNode gameDataJson;

        if (File.Exists(updatedPlayDataPath))
        {
            string jsonData = File.ReadAllText(updatedPlayDataPath);
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
           
                gameDataJson[i]["totalLevel"] = newGameData.totalLevel;
                gameDataJson[i]["completedLevel"] = newGameData.completedLevel;
                gameDataJson[i]["timeSpentInSeconds"] += newGameData.timeSpentInSeconds;
                gameDataJson[i]["star"] = newGameData.star;
                gameDataJson[i]["attempts"] += newGameData.attempts;
                gameDataJson[i]["score"] = newGameData.score;
                Debug.Log("Update The  data");
                gameExists = true;
                break;
            }
        }

        // If the gameId does not exist, add new data
        if (!gameExists)
        {
            JSONObject newGameEntry = new JSONObject
            {
                ["gameId"] = newGameData.gameId,
                ["totalLevel"] = newGameData.totalLevel,
                ["completedLevel"] = newGameData.completedLevel,
                ["timeSpentInSeconds"] = newGameData.timeSpentInSeconds,
                ["star"] = newGameData.star,
                ["attempts"] = newGameData.attempts,
                ["score"] = newGameData.score
            };
            //gameDataJson.(0, newGameEntry); // Insert at first index
            gameDataJson.Add(newGameEntry);
        }

          
        File.WriteAllText(updatedPlayDataPath, gameDataJson.ToString());
    }

    public void CallTheApiIfOnline()
    {
          if (Application.internetReachability == NetworkReachability.NotReachable)return;
         if (File.Exists(updatedPlayDataPath))
        {
            string jsonData = File.ReadAllText(updatedPlayDataPath);
            //StartCoroutine(AddStudentGameByIdApi(jsonData));
        }
    }

//     IEnumerator AddStudentGameByIdApi(string jsonData)
//     {
    
//         JSONNode responseJson = JSON.Parse(jsonData);
//         JSONNode dataArray = responseJson;

//  for (int i = 0; i < dataArray.Count; i++)
//      {
//                 JSONNode data = dataArray[i];
//          WWWForm form = new WWWForm();
//             form.AddField("StudentName", fileName.Split()[0]);
//             form.AddField("Stars", data["star"].ToString() );
//             form.AddField("CompletedLevel",data["completedLevel"].ToString() );
//             form.AddField("TotalLevel",  data["totalLevel"].ToString() );
//             form.AddField("Attempts", data["attempts"].ToString() );
//             form.AddField("TimeSpentInSeconds",data["timeSpentInSeconds"].ToString() );
//             form.AddField("Score",data["score"].ToString() );
//             form.AddField("GameId", data["gameId"].ToString());

//               using (UnityWebRequest request = UnityWebRequest.Post(Constants.AddDataStudentGameById, form))
//             {
//                 request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString(TMKOCPlaySchoolConstants.AuthorizationToken));
//                 yield return request.SendWebRequest();

//                 if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
//                 {
//                     Debug.LogError(request.error);
//                 }
//                 else
//                 {
//                     var jsonResponse = request.downloadHandler.text;
//                     Debug.Log(jsonResponse);
//                 }
//             }
//            //Debug.Log(" a " +  data["star"] + " sv s " + data["totalLevel"] + " s " + data["gameId"]   + " ds " +fileName.Split()[0]);
//         //
//      }
 

//             File.Delete(updatedPlayDataPath);

//     }


}
