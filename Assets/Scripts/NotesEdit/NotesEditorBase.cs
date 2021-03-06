using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class NotesEditorBase : MonoBehaviour, IManager
{
    [SerializeField] NotesEditorData _notesEditorData;
    [SerializeField] SoundDataBase _soundDataBase;

    // IManager
    public PhotonView ManagerPhotonView { get; set; }
    public GameObject ManagerObject() => gameObject;
    public string ManagerPath() => GetType().Name;
}
