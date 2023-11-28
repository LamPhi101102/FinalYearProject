using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance { get; set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public List<Quest> allActiveQuests;
    public List<Quest> allCompletedQuests;

    [Header("QuestMenu")]
    public GameObject questMenu;
    public bool isQuestMenuOpen;

    public GameObject activeQuestPrefab;
    public GameObject completedQuestPrefab;

    public GameObject questMenucontent;

    [Header("QuestTracker")]
    public GameObject questTrackerContent;
    public GameObject trackerRowPrefab;
    public List<Quest> allTrackerQuests;

    public void TrackQuest(Quest quest)
    {
        allTrackerQuests.Add(quest);
        RefreshTrackerList();
    }

    public void UnTrackQuest(Quest quest)
    {
        allTrackerQuests.Remove(quest);
        RefreshTrackerList();
    }

    public void RefreshTrackerList()
    {
        // Destroying the previous list
        foreach (Transform child in questTrackerContent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Quest trackedQuest in allTrackerQuests)
        {
            GameObject trackerPrefab = Instantiate(trackerRowPrefab, Vector3.zero, Quaternion.identity);
            trackerPrefab.transform.SetParent(questTrackerContent.transform, false);

            TrackerRow tRow = trackerPrefab.GetComponent<TrackerRow>();

            tRow.questName.text = trackedQuest.questName;
            tRow.description.text = trackedQuest.questDescription;





            if (trackedQuest.info.secondRequirementItem != "") // if we have 2 requirements
            {
                tRow.requirements.text = $"{trackedQuest.info.firstRequirementItem}" + InventorySystem.instance.CheckItemAmount(trackedQuest.info.firstRequirementItem) + "/" + $"{trackedQuest.info.firstRequirementAmount}\n" +
               $"{trackedQuest.info.secondRequirementItem}" + InventorySystem.instance.CheckItemAmount(trackedQuest.info.secondRequirementItem) + "/" + $"{trackedQuest.info.secondRequirementAmount}\n";
            }
            else // if we have only one
            {
                tRow.requirements.text = $"{trackedQuest.info.firstRequirementItem}" + InventorySystem.instance.CheckItemAmount(trackedQuest.info.firstRequirementItem) + "/" + $"{trackedQuest.info.firstRequirementAmount}\n";
            }
        }

    }



    public void AddActiveQuest(Quest quest)
    {
        allActiveQuests.Add(quest);
        TrackQuest(quest);
        RefreshQuestList();
    }

    public void MarkQuestCompleted(Quest quest)
    {
        allActiveQuests.Remove(quest);
        allCompletedQuests.Add(quest);
        UnTrackQuest(quest);
        RefreshQuestList();
    }



    public void RefreshQuestList()
    {
        foreach(Transform child in questMenucontent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach(Quest activeQuest in allActiveQuests)
        {
            GameObject questPrefab = Instantiate(activeQuestPrefab, Vector3.zero, Quaternion.identity);
            questPrefab.transform.SetParent(questMenucontent.transform, false);

            QuestRow qRow = questPrefab.GetComponent<QuestRow>();
            
            qRow.thisQuest = activeQuest;

            qRow.questName.text = activeQuest.questName;
            qRow.questGiver.text = activeQuest.questGiver;

            qRow.isActive = true;
            qRow.isTracking = true;

            qRow.coinAmount.text = $"{activeQuest.info.coinReward}";

            qRow.firstRewardAmount.text = "";
            qRow.secondRewardAmount.text = "";

        }

        foreach (Quest completedQuest in allCompletedQuests)
        {
            GameObject questPrefab = Instantiate(completedQuestPrefab, Vector3.zero, Quaternion.identity);
            questPrefab.transform.SetParent(questMenucontent.transform, false);

            QuestRow qRow = questPrefab.GetComponent<QuestRow>();

            qRow.questName.text = completedQuest.questName;
            qRow.questGiver.text = completedQuest.questGiver;

            qRow.isActive = false;
            qRow.isTracking = false;

            qRow.coinAmount.text = $"{completedQuest.info.coinReward}";

            qRow.firstRewardAmount.text = "";
            qRow.secondRewardAmount.text = "";

        }
    }

}
