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

  
    private bool Begin = false;
    bool set = false;

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
            if (MonobitNetwork.inRoom&& !Begin) {

                if (!set) {
                    set = true;
                    gm.Paddleinit();
                }

                if (MonobitNetwork.room.playerCount >= MonobitNetwork.room.maxPlayers) {
                    if (MonobitNetwork.inRoom && MonobitNetwork.isHost) {
                        if (GUILayout.Button("�X�^�[�g")) {
                            MonobitNetwork.room.open = false;

                            monobitView.RPC("seting", MonobitEngine.MonobitTargets.All, null);
                            seting();
                            gm.ballinit();
                        }
                    }
                }

                // �{�^�����͂Ń��[������ގ�
                if (GUILayout.Button("�I��", GUILayout.Width(150))) {
                    MonobitNetwork.LeaveRoom();
                }
            }
            

            // ���[���ɓ������Ă���ꍇ
            if (MonobitNetwork.inRoom && Begin) {
               
                // �{�^�����͂Ń��[������ގ�
                if (GUILayout.Button("�I��", GUILayout.Width(150))) {
                    MonobitNetwork.LeaveRoom();
                }
            }

            // ���[���ɂ܂��������Ă��Ȃ��Ƃ�
            if (!MonobitNetwork.inRoom) {
             
               
                GUILayout.BeginHorizontal();

                // ���[�����̓���
                GUILayout.Label("���[���� : ");
                roomName = GUILayout.TextField(roomName, GUILayout.Width(200));
                
                /*// MUN�T�[�o�ɐڑ�����
                if (GUILayout.Button("Connect Server", GUILayout.Width(150))) {
                    MonobitNetwork.ConnectServer("Pong2_v1.0");
                }*/
              
                // ���[�����쐬
                if (GUILayout.Button("���[�����쐬", GUILayout.Width(100))) {
                    MonobitNetwork.CreateRoom(roomName,settings,lobby);
                }

                GUILayout.EndHorizontal();

                // ���ݑ��݂��郋�[�����烉���_���ɓ�������
                if (GUILayout.Button("�����_���}�b�`���O", GUILayout.Width(150))) {
                    MonobitNetwork.JoinRandomRoom();
                }

                // ���[���ꗗ����I�����œ�������
                foreach (RoomData room in MonobitNetwork.GetRoomData()) {
                    string strRoomInfo =
                        string.Format("{0}({1}/{2})",
                                      room.name,
                                      room.playerCount,
                                      (room.maxPlayers == 0) ? "-" : room.maxPlayers.ToString());

                    if (GUILayout.Button("�����ɓ��� : " + strRoomInfo)) {
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
