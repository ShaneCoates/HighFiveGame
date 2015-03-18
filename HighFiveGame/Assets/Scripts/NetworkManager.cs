using UnityEngine;
using System.Collections;



public class NetworkManager : MonoBehaviour {

	//MasterServer.ipAddress =127.0.0.1;
	//Basically the string names for the host and local name
	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";

	public GameObject playerPrefab;

	///Joining Server code
	private HostData[] hostlist;

	//Start up the server
	private void StartServer()
	{
		Network.InitializeServer(4,25000,!Network.HavePublicAddress());
		MasterServer.RegisterHost (typeName, gameName);
	}
	//Callbackif a player joins
	void OnServerInitialized()
	{
		SpawnPlayer();
		Debug.Log ("Server Initiliazed");
	}
	// Creates the Buttons for the server commands
	void OnGUI()
	{
		//IF there is no client or server create the GUI to init it.
		if (!Network.isClient && !Network.isServer) {
			if (GUI.Button (new Rect (100, 100, 250, 100), "Start Server"))
				StartServer ();
			if (GUI.Button (new Rect (100, 250, 250, 100), "Refresh Server"))
				RefreshHostList ();

			if (hostlist != null) {
				for (int i = 0; i < hostlist.Length; i++) {
					if (GUI.Button (new Rect (400, 100 + (110 * i), 300, 100), hostlist[i].gameName))
						JoinServer (hostlist [i]);
				}
			}

		}
		else
	
		Debug.Log ("Server Initiliazed");
	}
	//Checks local Ip address for any host servers
	private void RefreshHostList()
	{
		MasterServer.RequestHostList (typeName);
	}

	//Makes it master server
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
				hostlist = MasterServer.PollHostList ();
	}

	//Connect to the hosted server
	private void JoinServer(HostData hostdata)
	{
		Network.Connect(hostdata);
	}

	//Calls this as connecting to the sever
	void OnConnectToServer()
	{
		SpawnPlayer();
		Debug.Log ("Server Joined");
	}
	//Suppose to spawn a player for testing purposes
	private void SpawnPlayer()
	{
		Network.Instantiate(playerPrefab, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy()
	{
		Network.Disconnect();
		MasterServer.UnregisterHost();
	}

}
