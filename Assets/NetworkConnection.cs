using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// namespace��錾
using MonobitEngine;
// MonobitEngine.Monobehaviour���p������
public class NetworkConnection : MonobitEngine.MonoBehaviour
{
    /** ���[����. */
    private string roomName = "";

    public GameManager gm;

    /** paddle.*/
    private bool paddle = false;

    MonobitEngine.RoomSettings settings;

    MonobitEngine.LobbyInfo lobby; 

    // Start is called before the first frame update
    void Start()
    {
        // �f�t�H���g���r�[�֎���������������
        MonobitNetwork.autoJoinLobby = true;

        // MUN�T�[�o�ɐڑ�����
        MonobitNetwork.ConnectServer("Pong2_v1.0");

        settings = new MonobitEngine.RoomSettings();
        settings.maxPlayers = 2;
        settings.isVisible = true;
        settings.isOpen = true;

        lobby = new MonobitEngine.LobbyInfo();


    }

    void OnGUI() {
        // MUN�T�[�o�ɐڑ����Ă���Ƃ�
        if (MonobitNetwork.isConnect) {

            // ���[���ɓ������Ă���ꍇ
            if (MonobitNetwork.inRoom) {
                // �{�^�����͂Ń��[������ގ�
                if (GUILayout.Button("Leave Room", GUILayout.Width(150))) {
                    MonobitNetwork.LeaveRoom();
                }
            }

            // ���[���ɂ܂��������Ă��Ȃ��Ƃ�
            if (!MonobitNetwork.inRoom) {
             
               
                GUILayout.BeginHorizontal();

                // ���[�����̓���
                GUILayout.Label("RoomName : ");
                roomName = GUILayout.TextField(roomName, GUILayout.Width(200));
                
                // MUN�T�[�o�ɐڑ�����
                if (GUILayout.Button("Connect Server", GUILayout.Width(150))) {
                    MonobitNetwork.ConnectServer("Pong2_v1.0");
                }
              
                // ���[�����쐬
                if (GUILayout.Button("Create Room", GUILayout.Width(100))) {
                    MonobitNetwork.CreateRoom(roomName,settings,lobby);
                }

                GUILayout.EndHorizontal();

                // ���ݑ��݂��郋�[�����烉���_���ɓ�������
                if (GUILayout.Button("Join Random Room", GUILayout.Width(200))) {
                    MonobitNetwork.JoinRandomRoom();
                }

                // ���[���ꗗ����I�����œ�������
                foreach (RoomData room in MonobitNetwork.GetRoomData()) {
                    string strRoomInfo =
                        string.Format("{0}({1}/{2})",
                                      room.name,
                                      room.playerCount,
                                      (room.maxPlayers == 0) ? "-" : room.maxPlayers.ToString());

                    if (GUILayout.Button("Enter Room : " + strRoomInfo)) {
                        MonobitNetwork.JoinRoom(room.name);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
            // MUN�T�[�o�ɐڑ����Ă���A�����[���ɓ������Ă���ꍇ
            if (MonobitNetwork.isConnect && MonobitNetwork.inRoom) {
                // �v���C���[�L�����N�^�����o��̏ꍇ�ɓo�ꂳ����
                if (!paddle) {
                  paddle = true;
                  gm.Gameinit();
                }
            }
        
       
    }
}
