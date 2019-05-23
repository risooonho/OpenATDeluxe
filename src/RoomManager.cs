using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class RoomManager : Node2D {
	public static RoomManager instance;
	public static string currentRoom;
	public static Node2D currentRoomNode;

	private static Dictionary<string, PackedScene> rooms;
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		rooms = new Dictionary<string, PackedScene>();

		//Load every Room in preparation
		Directory d = new Directory();
		d.Open("res://scenes/rooms/");
		d.ListDirBegin(true, true);
		string file;
		while ((file = d.GetNext()) != "") {
			rooms.Add(System.IO.Path.GetFileNameWithoutExtension(file), ResourceLoader.Load<PackedScene>("res://scenes/rooms/" + file));
		}
		d.ListDirEnd();

		instance = this;
		ChangeRoom("", isAirport: true);
	}

	public static void ChangeRoom(string newRoomName, bool isAirport) {
		if (isAirport)
			newRoomName = "RoomAirport";


		//string fullPath = "res://scenes/rooms/" + newRoomName + ".tscn";
		//File f = new File();
		Debug.Assert(rooms.ContainsKey(newRoomName), "Room not found! Room: " + newRoomName);

		if (MouseCursor.instance != null)
			MouseCursor.instance.Reset();

		//Remove the current room!
		foreach (Node2D child in instance.GetChildren()) {
			child.QueueFree();
		}

		Node n = rooms[newRoomName].Instance();
		instance.AddChild(n);

		currentRoom = newRoomName;

		//instance.AddChild(n);
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}
