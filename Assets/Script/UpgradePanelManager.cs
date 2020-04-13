using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

using FFTAICommunicationLib;

public class UpgradePanelManager : MonoBehaviour
{
	public MainSysPanel MainPanel;
	public UpgradePanelManager upgradePanelManager;

	public Button ReturnButton;

	public Button SelectButton;

	public Button UpgradeIapButton;
	public Button UpgradeAppButton;

	public Button RunIapButton;
	public Button RunAppButton;

	public InputField FilePathInputField;

	public Text BootModeText;
	public Text WorkStatusText;

	public Text IAPProgressText;
	public Text APPProgressText;

	private string filePath;

	// Use this for initialization
	void Start ()
	{
		ReturnButton.onClick.AddListener (OnClickReturnButton);

		SelectButton.onClick.AddListener (onClickSelectButton);

		UpgradeIapButton.onClick.AddListener (onClickUpgradeIapButton);
		UpgradeAppButton.onClick.AddListener (onClickUpgradeAppButton);

		RunIapButton.onClick.AddListener (onClickRunIapButton);
		RunAppButton.onClick.AddListener (onClickRunAppButton);
	}

	// Update is called once per frame
	void Update ()
	{
		IAPProgressText.text = DynaLinkHS.StatusIAP.IAPUpgradeIapProgress.ToString ();
		APPProgressText.text = DynaLinkHS.StatusIAP.IAPUpgradeAppProgress.ToString ();

		BootModeText.text = DynaLinkHS.StatusIAP.IAPBootMode.ToString ();
		WorkStatusText.text = DynaLinkHS.StatusIAP.IAPWorkStatus.ToString ();
	}

	void OnClickReturnButton()
	{
		MainPanel.gameObject.SetActive(true);
		upgradePanelManager.gameObject.SetActive(false);
	}

	void onClickSelectButton()
	{
		/*
		System.Windows.Forms.OpenFileDialog openFileDiaglog = new System.Windows.Forms.OpenFileDialog();

		openFileDiaglog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		openFileDiaglog.Title = "Select bin file.";
		openFileDiaglog.RestoreDirectory = true;
		openFileDiaglog.AutoUpgradeEnabled = true;

		openFileDiaglog.ShowDialog ();

		FilePathInputField.text = "";
		*/
	}

	void onClickUpgradeIapButton()
	{
		filePath = FilePathInputField.text;
		
		DynaLinkHS.UpgradeMMUIap (filePath);
	}

	void onClickUpgradeAppButton()
	{
		filePath = FilePathInputField.text;

		DynaLinkHS.UpgradeMMUApp (filePath);
	}

	void onClickRunIapButton()
	{
		DynaLinkHS.CmdSetBootMode (DynaLinkHSPara.IAPBootMode.IAP);
	}

	void onClickRunAppButton()
	{
		DynaLinkHS.CmdSetBootMode (DynaLinkHSPara.IAPBootMode.APP);
	}
}

