using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class UIGamble : MonoBehaviour
{
    public Button StartButton;

    public Button RefreshButton;

    public Button ExitButton;

    public Transform ResultTrans;

    public Transform RuleTrans;

    public Transform RewardTrans;

    public Button ScrollButton;

    public InputField FromInput;

    public InputField ToInput;

    public InputField PeopleNum;

    public Button RuleConfirm;

    public InputField RewardName;

    public InputField RewardPrice;

    public Button RewardConfirm;

    public InputField CandidateName;

    public Button CandidateConfirm;

    public Transform ResultContent;

    public Transform RuleContent;

    public Transform RewardContent;

    public Transform CandidateContent;

    private List<Button> ResultButtons;

    private List<Button> RuleButtons;

    private List<Button> RewardButtons;

    private List<Button> CandidateButtons;

    private List<RuleStruct> RuleStructs;
    private List<RewardStruct> RewardStructs;
    private List<String> Candidates;
    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(OnStartClick);
        RefreshButton.onClick.AddListener(OnRefreshClick);
        ExitButton.onClick.AddListener(OnExitClick);
        RuleConfirm.onClick.AddListener(OnRuleConfirm);
        RewardConfirm.onClick.AddListener(OnRewardConfirm);
        CandidateConfirm.onClick.AddListener(OnCandidateConfirm);
        
        ResultButtons = new List<Button>();
        RuleButtons = new List<Button>();
        RewardButtons = new List<Button>();
        CandidateButtons = new List<Button>();
        RuleStructs = new List<RuleStruct>();
        RewardStructs = new List<RewardStruct>();
        Candidates = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnStartClick()
    {
        foreach (var v in ResultButtons)
        {
            GameObject.Destroy(v.gameObject);
        }
        ResultButtons.Clear();
        
        List<Int32> candidateIndex = new List<int>();
        Int32 expectCandidateNum = 0;
        foreach (RuleStruct r in RuleStructs)
        {
            expectCandidateNum += r.num;
        }

        Random rd = new Random();
        while (candidateIndex.Count < expectCandidateNum)
        {
            Int32 curIndex = rd.Next(0, Candidates.Count);
            if (expectCandidateNum <= Candidates.Count)
            {
                if (!candidateIndex.Contains(curIndex))
                {
                    candidateIndex.Add(curIndex);
                }
            }
            else
            {
                candidateIndex.Add(curIndex);
            }
        }

        foreach (Int32 index in candidateIndex)
        {
            Button resultButton = GameObject.Instantiate(ScrollButton, ResultContent);
            resultButton.transform.Find("Text").GetComponent<Text>().text = Candidates[index];
            ResultButtons.Add(resultButton);
        }
    }

    void OnRefreshClick()
    {
        FromInput.text = "";
        FromInput.transform.Find("Placeholder").GetComponent<Text>().text = "Enter text...";
        ToInput.text = "";
        ToInput.transform.Find("Placeholder").GetComponent<Text>().text = "Enter text...";
        PeopleNum.text = "";
        PeopleNum.transform.Find("Placeholder").GetComponent<Text>().text = "Enter text...";
        RewardName.text = "";
        RewardName.transform.Find("Placeholder").GetComponent<Text>().text = "Enter text...";
        RewardPrice.text = "";
        RewardPrice.transform.Find("Placeholder").GetComponent<Text>().text = "Enter text...";
        CandidateName.text = "";
        CandidateName.transform.Find("Placeholder").GetComponent<Text>().text = "Enter text...";
        foreach (var v in ResultButtons)
        {
            GameObject.Destroy(v.gameObject);
        }
        ResultButtons.Clear();
        
        foreach (var v in RuleButtons)
        {
            GameObject.Destroy(v.gameObject);
        }
        RuleButtons.Clear();
        RuleStructs.Clear();
        
        foreach (var v in RewardButtons)
        {
            GameObject.Destroy(v.gameObject);
        }
        RewardButtons.Clear();
        RewardStructs.Clear();
        
        foreach (var v in CandidateButtons)
        {
            GameObject.Destroy(v.gameObject);
        }
        CandidateButtons.Clear();
        Candidates.Clear();
    }

    void OnExitClick()
    {
        Application.Quit();
    }

    void OnScrollButtonClick(Button clickedButton)
    {
        do
        {
            if (RuleButtons.Contains(clickedButton))
            {
                RuleButtons.Remove(clickedButton);
                break;
            }
            if (RewardButtons.Contains(clickedButton))
            {
                RewardButtons.Remove(clickedButton);
                break;
            }
            if (CandidateButtons.Contains(clickedButton))
            {
                CandidateButtons.Remove(clickedButton);
                break;
            }
        } while (false);

        GameObject.Destroy(clickedButton.gameObject);
    }

    void OnRuleConfirm()
    {
        if (FromInput.text == "")
        {
            FromInput.transform.Find("Placeholder").GetComponent<Text>().text = "Empty?";
            return;
        }
        else
        {
            FromInput.transform.Find("Placeholder").GetComponent<Text>().text = "Enter text...";
        }
        if (ToInput.text == "")
        {
            ToInput.transform.Find("Placeholder").GetComponent<Text>().text = "Empty?";
            return;
        }
        else
        {
            ToInput.transform.Find("Placeholder").GetComponent<Text>().text = "Enter text...";
        }
        if (PeopleNum.text == "")
        {
            PeopleNum.transform.Find("Placeholder").GetComponent<Text>().text = "Empty?";
            return;
        }
        else
        {
            PeopleNum.transform.Find("Placeholder").GetComponent<Text>().text = "Enter text...";
        }
        
        RuleStruct newRule;
        newRule.from = Int32.Parse(FromInput.text);
        FromInput.text = "";
        newRule.to = Int32.Parse(ToInput.text);
        ToInput.text = "";
        newRule.num = Int32.Parse(PeopleNum.text);
        PeopleNum.text = "";
        RuleStructs.Add(newRule);

        Button newRuleButton = GameObject.Instantiate(ScrollButton, RuleContent);
        RuleButtons.Add(newRuleButton);
        newRuleButton.onClick.AddListener(delegate { 
            OnScrollButtonClick(newRuleButton);
            RuleStructs.Remove(newRule);
        });
        newRuleButton.transform.Find("Text").GetComponent<Text>().text =
            "From: " + newRule.from + " To: " + newRule.to + " Num: " + newRule.num;
    }

    void OnRewardConfirm()
    {
        if (RewardName.text == "")
        {
            RewardName.transform.Find("Placeholder").GetComponent<Text>().text = "Empty?";
            return;
        }
        else
        {
            RewardName.transform.Find("Placeholder").GetComponent<Text>().text = "Enter text...";
        }
        if (RewardPrice.text == "")
        {
            RewardPrice.transform.Find("Placeholder").GetComponent<Text>().text = "Empty?";
            return;
        }
        else
        {
            RewardPrice.transform.Find("Placeholder").GetComponent<Text>().text = "Enter text...";
        }
        RewardStruct newReward;
        newReward.name = RewardName.text;
        RewardName.text = "";
        newReward.price = Int32.Parse(RewardPrice.text);
        RewardPrice.text = "";
        RewardStructs.Add(newReward);
        
        Button newRewardButton = GameObject.Instantiate(ScrollButton, RewardContent);
        RewardButtons.Add(newRewardButton);
        newRewardButton.onClick.AddListener(delegate
        {
            OnScrollButtonClick(newRewardButton);
            RewardStructs.Remove(newReward);
        });
        newRewardButton.transform.Find("Text").GetComponent<Text>().text =
            "Reward: " + newReward.name + " Price: " + newReward.price;
    }

    void OnCandidateConfirm()
    {
        if (CandidateName.text == "")
        {
            CandidateName.transform.Find("Placeholder").GetComponent<Text>().text = "Empty?";
            return;
        }
        else
        {
            CandidateName.transform.Find("Placeholder").GetComponent<Text>().text = "Enter text...";
        }
        string newCandidate = CandidateName.text;
        CandidateName.text = "";
        Candidates.Add(newCandidate);
        
        Button newCandidateButton = GameObject.Instantiate(ScrollButton, CandidateContent);
        CandidateButtons.Add(newCandidateButton);
        newCandidateButton.onClick.AddListener(delegate
        {
            OnScrollButtonClick(newCandidateButton);
            Candidates.Remove(newCandidate);
        });
        newCandidateButton.transform.Find("Text").GetComponent<Text>().text = newCandidate;
    }
    
    struct RewardStruct
    {
        public string name;
        public int price;
    }

    struct RuleStruct
    {
        public int from;
        public int to;
        public int num;
    }
}
