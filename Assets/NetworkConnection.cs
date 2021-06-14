using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// namespaceを宣言
using MonobitEngine;
// MonobitEngine.Monobehaviourを継承する
public class NetworkConnection : MonobitEngine.MonoBehaviour
{
    /** ルーム名. */
    private string roomName = "";

    public GameManager gm;

  
    private bool Begin = false;
    bool set = false;

    MonobitEngine.RoomSettings settings;

    MonobitEngine.LobbyInfo lobby; 

    // Start is called before the first frame update
    void Start()
    {
        // デフォルトロビーへ自動入室を許可する
        MonobitNetwork.autoJoinLobby = true;

        // MUNサーバに接続する
        MonobitNetwork.ConnectServer("Pong2_v1.0");

        settings = new MonobitEngine.RoomSettings();
        settings.maxPlayers = 2;
        settings.isVisible = true;
        settings.isOpen = true;

        lobby = new MonobitEngine.LobbyInfo();


    }

    void OnGUI() {
        // MUNサーバに接続しているとき
        if (MonobitNetwork.isConnect) {

            // ルームに入室している場合
            if (MonobitNetwork.inRoom&& !Begin) {

                if (!set) {
                    set = true;
                    gm.Paddleinit();
                }

                if (MonobitNetwork.room.playerCount >= MonobitNetwork.room.maxPlayers) {
                    if (MonobitNetwork.inRoom && MonobitNetwork.isHost) {
                        if (GUILayout.Button("スタート")) {
                            MonobitNetwork.room.open = false;

                            monobitView.RPC("seting", MonobitEngine.MonobitTargets.All, null);
                            seting();
                            gm.ballinit();
                        }
                    }
                }

                // ボタン入力でルームから退室
                if (GUILayout.Button("終了", GUILayout.Width(150))) {
                    MonobitNetwork.LeaveRoom();
                }
            }
            

            // ルームに入室している場合
            if (MonobitNetwork.inRoom && Begin) {
               
                // ボタン入力でルームから退室
                if (GUILayout.Button("終了", GUILayout.Width(150))) {
                    MonobitNetwork.LeaveRoom();
                }
            }

            // ルームにまだ入室していないとき
            if (!MonobitNetwork.inRoom) {
             
               
                GUILayout.BeginHorizontal();

                // ルーム名の入力
                GUILayout.Label("ルーム名 : ");
                roomName = GUILayout.TextField(roomName, GUILayout.Width(200));
                
                /*// MUNサーバに接続する
                if (GUILayout.Button("Connect Server", GUILayout.Width(150))) {
                    MonobitNetwork.ConnectServer("Pong2_v1.0");
                }*/
              
                // ルームを作成
                if (GUILayout.Button("ルームを作成", GUILayout.Width(100))) {
                    MonobitNetwork.CreateRoom(roomName,settings,lobby);
                }

                GUILayout.EndHorizontal();

                // 現在存在するルームからランダムに入室する
                if (GUILayout.Button("ランダムマッチング", GUILayout.Width(150))) {
                    MonobitNetwork.JoinRandomRoom();
                }

                // ルーム一覧から選択式で入室する
                foreach (RoomData room in MonobitNetwork.GetRoomData()) {
                    string strRoomInfo =
                        string.Format("{0}({1}/{2})",
                                      room.name,
                                      room.playerCount,
                                      (room.maxPlayers == 0) ? "-" : room.maxPlayers.ToString());

                    if (GUILayout.Button("部屋に入る : " + strRoomInfo)) {
                        MonobitNetwork.JoinRoom(room.name);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        
       
    }
    [MunRPC]
    void seting() {
        Begin = true;
    }
}
