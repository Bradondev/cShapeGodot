using Godot;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography.X509Certificates;
public partial class AppManager : Node
{

	public static Budget currentBudget;
	[Export] public TextureProgressBar PieChart;
	[Export] public PackedScene TransactionList;
	[Export] public VBoxContainer TransactionListHolder;
	[Export] public PackedScene AddTransactionMenu;
	[Export] public RichTextLabel IncomeLabel;
	[Export] public RichTextLabel IncomeAmountLabel;
	[Export] public RichTextLabel ExpensesLabel;
	[Export] public RichTextLabel ExpensesAmountLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentBudget = new Budget(){
			Name = "Current Budget",
			Expenses = new List<Transaction>(),
			Income = new List<Transaction>(),

		};
		
		loadCSV("C:/Users/anime/Desktop/cShapeGodot/Records.csv");
		UpdateTransactionList();
	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_add_transaction_button_down(){
		AddtransactionWindow TransactionMenu = AddTransactionMenu.Instantiate<AddtransactionWindow>();
		AddChild(TransactionMenu);
		TransactionMenu.AddTransaction += AddTransactionToTransction;
	}

    private void AddTransactionToTransction(string Name, string Date, float Amount, string type, bool Income)
    {
		Transaction transaction = new Transaction(){
			Name = Name,
			Date = Date,
			Amount = Amount,
			Type = type
		};


		if(Income){
			currentBudget.Expenses.Add(transaction);
		
		
		}else{
			currentBudget.Income.Add(transaction);
		};
        UpdateTransactionList();

			
    }

    //Updates the Transaction list
    public void UpdateTransactionList(){
		foreach (var child in TransactionListHolder.GetChildren()){
			child.QueueFree();
		}
		float ExpensesAmount =0;
		foreach(var value in currentBudget.Expenses){
			VBoxContainer container = GetNode<VBoxContainer> ("ScrollContainer/TransactionList");
			Node tableRow = TransactionList.Instantiate();
			tableRow.GetNode<RichTextLabel>("Data").Text = value.Date.ToString();
			tableRow.GetNode<RichTextLabel>("Name").Text = value.Name;
			tableRow.GetNode<RichTextLabel>("Amount").Text = value.Amount.ToString();
			tableRow.GetNode<RichTextLabel>("Type").Text = value.Type;
			tableRow.GetNode<RichTextLabel>("DepositOrWithdrawl").Text = "Withdrawl";
			container.AddChild(tableRow);
			ExpensesAmount += value.Amount;
		}
		ExpensesAmountLabel.Text = "[center][b]" +ExpensesAmount.ToString();

		float IncomeAmount =0;
		foreach(var value in currentBudget.Income){
			VBoxContainer container = GetNode<VBoxContainer> ("ScrollContainer/TransactionList");
			Node tableRow = TransactionList.Instantiate();
			tableRow.GetNode<RichTextLabel>("Data").Text = value.Date.ToString();
			tableRow.GetNode<RichTextLabel>("Name").Text = value.Name;
			tableRow.GetNode<RichTextLabel>("Amount").Text = value.Amount.ToString();
			tableRow.GetNode<RichTextLabel>("Type").Text = value.Type;
			tableRow.GetNode<RichTextLabel>("DepositOrWithdrawl").Text = "Deposit";
			container.AddChild(tableRow);
			IncomeAmount += value.Amount;
		}
		IncomeAmountLabel.Text = "[center][b]" +IncomeAmount.ToString();
		PieChart.MaxValue = IncomeAmount;
		PieChart.Value = ExpensesAmount;
	}

	private void loadCSV(string path){
		List<string[]> parsedDate = new List<string[]>();
		using (StreamReader reader = new StreamReader(path)){
			while(!reader.EndOfStream){
				string line = reader.ReadLine();
				string[] fields = line.Split(',');
				parsedDate.Add(fields);
			}
		}

		foreach(var tableRow in parsedDate){
			if (tableRow[0] == "Date"){
				continue;
			}
			
			AddTransactionToTransction(tableRow[1],tableRow[0],
			float.Parse(tableRow[2]) != 0.0 ? float.Parse(tableRow[2]) : float.Parse(tableRow[3]),
			"home", float.Parse(tableRow[2]) != 0.0);
		};
		

		
		UpdateTransactionList();

	}
}
