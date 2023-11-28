using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Debug = UnityEngine.Debug;
using System.Security.Cryptography;

public class NPC : MonoBehaviour
{
    public bool playerInRange;

    public bool isTalkingWithPlayer;

    public string ItemName;


    TextMeshProUGUI npcdialogText;

    public Button optionButton1;
    TextMeshProUGUI optionButton1Text;

    public Button optionButton2;
    TextMeshProUGUI optionButton2Text;

    public List<Quest> quests;
    public Quest currentActiveQuest = null;
    public int activeQuestIndex = 0;
    public bool firstTimeInteraction = true;
    public int currentDialog;


    private void Start()
    {
        npcdialogText = DialogSystem.instance.dialogText;

        optionButton1 = DialogSystem.instance.option1BTN;
        optionButton1Text = DialogSystem.instance.option1BTN.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

        optionButton2 = DialogSystem.instance.option2BTN;
        optionButton2Text = DialogSystem.instance.option2BTN.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
    }


    public string GetItemName()
    {
        return ItemName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void StartConservation()
    {
        isTalkingWithPlayer = true;

        LookAtPlayer();

        if (firstTimeInteraction)
        {
            firstTimeInteraction = false;
            currentActiveQuest = quests[activeQuestIndex];
            StartQuestInitialDialog();
            currentDialog = 0;
        }
        else // Interacting with the NPC after the first time
        {

            // If we return after declining the quest
            if (currentActiveQuest.declined)
            {

                DialogSystem.instance.OpenDialogUI();

                npcdialogText.text = currentActiveQuest.info.comebackAfterDecline;

                SetAcceptAndDeclineOptions();
            }


            // If we return while the quest is still in progress
            if (currentActiveQuest.accepted && currentActiveQuest.isCompleted == false)
            {
                if (AreQuestRequirmentsCompleted())
                {

                    SubmitRequiredItems();

                    DialogSystem.instance.OpenDialogUI();

                    npcdialogText.text = currentActiveQuest.info.comebackCompleted;

                    optionButton1Text.text = "[Take Reward]";
                    optionButton1.onClick.RemoveAllListeners();
                    optionButton1.onClick.AddListener(() => {
                        ReceiveRewardAndCompleteQuest();
                    });
                }
                else
                {
                    DialogSystem.instance.OpenDialogUI();

                    npcdialogText.text = currentActiveQuest.info.comebackInProgress;

                    optionButton1Text.text = "[Close]";
                    optionButton1.onClick.RemoveAllListeners();
                    optionButton1.onClick.AddListener(() => {
                        DialogSystem.instance.CloseDialogUI();
                        isTalkingWithPlayer = false;
                    });
                }
            }

            if (currentActiveQuest.isCompleted == true)
            {
                DialogSystem.instance.OpenDialogUI();

                npcdialogText.text = currentActiveQuest.info.finalWords;

                optionButton1Text.text = "[Close]";
                optionButton1.onClick.RemoveAllListeners();
                optionButton1.onClick.AddListener(() => {
                    DialogSystem.instance.CloseDialogUI();
                    isTalkingWithPlayer = false;
                });
            }

            // If there is another quest available
            if (currentActiveQuest.initialDialogCompleted == false)
            {
                StartQuestInitialDialog();
            }

        }
    }

    private void SetAcceptAndDeclineOptions()
    {
        optionButton1Text.text = currentActiveQuest.info.acceptOption;
        optionButton1.onClick.RemoveAllListeners();
        optionButton1.onClick.AddListener(() => {
            AcceptedQuest();
        });

        optionButton2.gameObject.SetActive(true);
        optionButton2Text.text = currentActiveQuest.info.declineOption;
        optionButton2.onClick.RemoveAllListeners();
        optionButton2.onClick.AddListener(() => {
            DeclinedQuest();
        });
    }

    private void SubmitRequiredItems()
    {
        string firstRequiredItem = currentActiveQuest.info.firstRequirementItem;
        int firstRequiredAmount = currentActiveQuest.info.firstRequirementAmount;

        if (firstRequiredItem != "")
        {
            InventorySystem.instance.RemoveItem(firstRequiredItem, firstRequiredAmount);
        }


        string secondtRequiredItem = currentActiveQuest.info.secondRequirementItem;
        int secondRequiredAmount = currentActiveQuest.info.secondRequirementAmount;

        if (firstRequiredItem != "")
        {
            InventorySystem.instance.RemoveItem(secondtRequiredItem, secondRequiredAmount);
        }

    }

    private bool AreQuestRequirmentsCompleted()
    {
        print("Checking Requirments");

        // First Item Requirment

        string firstRequiredItem = currentActiveQuest.info.firstRequirementItem;
        int firstRequiredAmount = currentActiveQuest.info.firstRequirementAmount;

        var firstItemCounter = 0;

        foreach (string item in InventorySystem.instance.itemList)
        {
            if (item == firstRequiredItem)
            {
                firstItemCounter++;
            }
        }

        // Second Item Requirment -- If we dont have a second item, just set it to 0

        string secondRequiredItem = currentActiveQuest.info.secondRequirementItem;
        int secondRequiredAmount = currentActiveQuest.info.secondRequirementAmount;

        var secondItemCounter = 0;

        foreach (string item in InventorySystem.instance.itemList)
        {
            if (item == secondRequiredItem)
            {
                secondItemCounter++;
            }
        }

        if (firstItemCounter >= firstRequiredAmount && secondItemCounter >= secondRequiredAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void StartQuestInitialDialog()
    {
        DialogSystem.instance.OpenDialogUI();
        npcdialogText.text = currentActiveQuest.info.initialDialog[currentDialog];
        optionButton1Text.text = "Next";
        optionButton1.onClick.RemoveAllListeners();
        optionButton1.onClick.AddListener(() =>
        {
            currentDialog++;
            checkIfDialogDone();
        });

        optionButton2.gameObject.SetActive(false);
    }

    private void checkIfDialogDone()
    {
        if (currentDialog == currentActiveQuest.info.initialDialog.Count - 1)
        {
            npcdialogText.text = currentActiveQuest.info.initialDialog[currentDialog];

            currentActiveQuest.initialDialogCompleted = true;

            optionButton1Text.text = currentActiveQuest.info.acceptOption;
            optionButton1.onClick.RemoveAllListeners();
            optionButton1.onClick.AddListener(() =>
            {
                AcceptedQuest();
            });

            optionButton2.gameObject.SetActive(true);
            optionButton2Text.text = currentActiveQuest.info.declineOption;
            optionButton2.onClick.RemoveAllListeners();
            optionButton2.onClick.AddListener(() =>
            {
                DeclinedQuest();
            });

        }
        else
        {
            npcdialogText.text = currentActiveQuest.info.initialDialog[currentDialog];

            optionButton1Text.text = "Next";
            optionButton1.onClick.RemoveAllListeners();
            optionButton1.onClick.AddListener(() =>
            {
                currentDialog++;
                checkIfDialogDone();
            });
        }
    }

    private void AcceptedQuest()
    {
        QuestManager.instance.AddActiveQuest(currentActiveQuest);

        currentActiveQuest.accepted = true;
        currentActiveQuest.declined = false;

        if (currentActiveQuest.hasNoRequirements)
        {
            npcdialogText.text = currentActiveQuest.info.comebackCompleted;
            optionButton1Text.text = "[Take Reward]";
            optionButton1.onClick.RemoveAllListeners();
            optionButton1.onClick.AddListener(() => {
                ReceiveRewardAndCompleteQuest();
            });
            optionButton2.gameObject.SetActive(false);
        }
        else
        {
            npcdialogText.text = currentActiveQuest.info.acceptAnswer;
            CloseDialogUI();
        }
    }

    private void ReceiveRewardAndCompleteQuest()
    {
        QuestManager.instance.MarkQuestCompleted(currentActiveQuest);

        currentActiveQuest.isCompleted = true;

        var coinsRecieved = currentActiveQuest.info.coinReward;
        print("You recieved " + coinsRecieved + " gold coins");

        if (currentActiveQuest.info.rewardItem1 != "")
        {
            InventorySystem.instance.AddToInventory(currentActiveQuest.info.rewardItem1);
        }

        if (currentActiveQuest.info.rewardItem2 != "")
        {
            InventorySystem.instance.AddToInventory(currentActiveQuest.info.rewardItem2);
        }

        activeQuestIndex++;

        // Start Next Quest 
        if (activeQuestIndex < quests.Count)
        {
            currentActiveQuest = quests[activeQuestIndex];
            currentDialog = 0;
            DialogSystem.instance.CloseDialogUI();
            isTalkingWithPlayer = false;
        }
        else
        {
            DialogSystem.instance.CloseDialogUI();
            isTalkingWithPlayer = false;
            print("No more quests");
        }

    }

    private void CloseDialogUI()
    {
        optionButton1Text.text = "[Close]";
        optionButton1.onClick.RemoveAllListeners();
        optionButton1.onClick.AddListener(() => {
            DialogSystem.instance.CloseDialogUI();
            isTalkingWithPlayer = false;
        });
        optionButton2.gameObject.SetActive(false);
    }


    private void DeclinedQuest()
    {
        currentActiveQuest.declined = true;

        npcdialogText.text = currentActiveQuest.info.declineAnswer;
        CloseDialogUI();
    }


    public void LookAtPlayer()
    {
        var player = PlayerState.Instance.playerBody.transform;
        Vector3 direction = player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

}
