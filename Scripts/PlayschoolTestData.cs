using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public struct PlaySchoolTestStars
{
    public PlaySchoolTestStars(int attemptNumber, int maxStars)
    {
        this.attemptNumber = attemptNumber;
        this.maxStars = maxStars;
    }
    public int attemptNumber;

    public int maxStars;


}
public class PlayschoolTestData
{
    private int m_MaxTestQuestions;

    public int MaxTestQuestions => m_MaxTestQuestions;

    private List<PlaySchoolTestStars> m_PlaySchoolTestStars;


    public PlayschoolTestData()
    {

    }

    public PlayschoolTestData(int maxTextQuestions)
    {
        m_MaxTestQuestions = maxTextQuestions;
    }

    ~PlayschoolTestData()
    {

    }

    #region Stars
    public int GetStarsBasedOnAttempt(int attemptNumber, int questionRight)
    {
        //HEELO TESTING
        float percentage = ((float)questionRight / m_MaxTestQuestions) * 100;

        switch (attemptNumber)
        {
            case 1:
                if (percentage >= 100)
                    return 5; // 100% correct answers
                else if (percentage >= 80)
                    return 4; // 80% to 99% correct answers
                else if (percentage >= 70)
                    return 3; // 70% to 79% correct answers
                else if (percentage >= 60)
                    return 0; // 60% to 69% correct answers
                else
                    return 0; // Less than 60% correct answers

            case 2:
                if (percentage >= 100)
                    return 4; // 100% correct answers
                else if (percentage >= 80)
                    return 3; // 80% to 99% correct answers
                else if (percentage >= 70)
                    return 2; // 70% to 79% correct answers
                else if (percentage >= 60)
                    return 0; // 60% to 69% correct answers
                else
                    return 0; // Less than 60% correct answers

            default:
                if (percentage >= 100)
                    return 3; // 100% correct answers
                else if (percentage >= 80)
                    return 2; // 80% to 99% correct answers
                else if (percentage >= 70)
                    return 1; // 70% to 79% correct answers
                else if (percentage >= 60)
                    return 0; // 60% to 69% correct answers
                else
                    return 0; // Less than 60% correct answers

        }
    }


    #endregion
}

public static class PlaySchoolTestDataConstants
{
    public const int MAX_STARS_ATTEMPT = 3;

    public const int FIRST_ATTEMPT_MAX_STARS = 5;
}
