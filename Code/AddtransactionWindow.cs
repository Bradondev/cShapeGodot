using Godot;
using System;
using System.Collections;
using System.Runtime.CompilerServices;

public partial class AddtransactionWindow : Panel
{

	[Signal]
	public delegate void AddTransactionEventHandler(string Name ,string Date,float Amount,string type, bool Income);

	[Export] CheckButton IncomeButton;
	[Export] LineEdit DateInputLine;
	[Export] LineEdit NameInputLine;
	[Export] LineEdit AmountInputLine;
	[Export] LineEdit TypeInputLine;
 	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_cancal_button_down(){
		QueueFree();
	}

	public void _on_add_button_down(){
		EmitSignal(SignalName.AddTransaction,
		 NameInputLine.Text,
		 DateInputLine.Text,
		 AmountInputLine.Text,
		 TypeInputLine.Text,
		 IncomeButton.ButtonPressed

		);
		QueueFree();
	}
}
