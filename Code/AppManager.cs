using Godot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
public partial class AppManager : Node
{

	Budget currentBudget;
	[Export] public PackedScene TransactionList;
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
		
		currentBudget.Expenses.Add(new Transaction(){
			Name = "test",
			Date = DateTime.Now,
			Amount = 50,
			Type = "Home"
		});
			
		currentBudget.Expenses.Add(new Transaction(){
			Name = "test2",
			Date = DateTime.Now,
			Amount = 501,
			Type = "Home"
		});

			currentBudget.Income.Add(new Transaction(){
			Name = "Income",
			Date = DateTime.Now,
			Amount = 2000,
			Type = "Home"
		});
		float ExpensesAmount =0;
		foreach(var value in currentBudget.Expenses){
			VBoxContainer container = GetNode<VBoxContainer> ("ScrollContainer/TransactionList");
			Node tableRow = TransactionList.Instantiate();
			tableRow.GetNode<RichTextLabel>("Data").Text = value.Date.ToString();
			tableRow.GetNode<RichTextLabel>("Name").Text = value.Name;
			tableRow.GetNode<RichTextLabel>("Amount").Text = value.Amount.ToString();
			tableRow.GetNode<RichTextLabel>("Type").Text = value.Type;
			container.AddChild(tableRow);
			ExpensesAmount += value.Amount;
		}
		ExpensesAmountLabel.Text = "[center][b]" +ExpensesAmount.ToString();
	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
