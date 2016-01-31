using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Diary : MonoBehaviour {
    public Text dayText;
    public Text entryText;

    public List<DiaryEntry> diaryEntries = new List<DiaryEntry>();
    public int currentEntry = 0;

    // Use this for initialization
    void Start () {
        DisplayEntry();
    }

    void OnEnable() {
        DisplayEntry();
    }

    // Update is called once per frame
    void Update () {
    }

    void DisplayEntry() {
        dayText.text = "Day " + diaryEntries[currentEntry].day.ToString();
        entryText.text = diaryEntries[currentEntry].entryText;
    }

    public void NextIndex() {
        do {
            currentEntry = (currentEntry + 1) % diaryEntries.Count;
        } while (diaryEntries[currentEntry].day == 0);
        DisplayEntry();
    }

    public void PrevIndex() {
        do {
            currentEntry = (currentEntry + diaryEntries.Count - 1) % diaryEntries.Count;
        } while (diaryEntries[currentEntry].day == 0);
        DisplayEntry();
    }


    public static bool UnlockDiaryEntry() {
        Diary diary = GameState.gameState.diaryScreen.GetComponent<Diary>();

        if (GameState.day == 1 && diary.diaryEntries[0].day == 0) {
            // Entry 0: Day 1
            diary.diaryEntries[0].day = GameState.day;
            return true;
        } else if (Upgrades.aesthetics >= 1 && diary.diaryEntries[1].day == 0) {
            // Entry 1: Birds
            diary.diaryEntries[1].day = GameState.day;
            return true;
        } else if (Upgrades.aesthetics >= 2 && diary.diaryEntries[2].day == 0) {
            // Entry 2: Colors
            diary.diaryEntries[2].day = GameState.day;
            return true;
        } else if (Upgrades.aesthetics >= 3 && diary.diaryEntries[3].day == 0) {
            // Entry 3: Petals
            diary.diaryEntries[3].day = GameState.day;
            return true;
        } else if (Upgrades.aesthetics >= 4 && diary.diaryEntries[4].day == 0) {
            // Entry 4: Particles
            diary.diaryEntries[4].day = GameState.day;
            return true;
        } else if (Upgrades.aesthetics >= 5 && diary.diaryEntries[5].day == 0) {
            // Entry 5: Greenery
            diary.diaryEntries[5].day = GameState.day;
            return true;
        } else if (Upgrades.aesthetics >= 6 && diary.diaryEntries[6].day == 0) {
            // Entry 6: Stars
            diary.diaryEntries[6].day = GameState.day;
            return true;
        } else if (Upgrades.aesthetics >= 6 && GameState.bestDistance > GameState.todaysBest + 10f && diary.diaryEntries[7].day == 0) {
            // Entry 7: Farther than ever
            diary.diaryEntries[7].day = GameState.day;
            return true;
        }
        return false;
    }
}
