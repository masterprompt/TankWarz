using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    /// <summary>
    /// Player represents each connected player
    /// </summary>
    public class Player : Actor
    {
        #region Fields
        public static List<Player> list = new List<Player>();
        /// <summary>
        /// Input type that player is currently using
        /// </summary>
        public PlayerInput input;
        /// <summary>
        /// Current pawn player has control over
        /// </summary>
        public Pawn pawn;
        public Tank tank;
        public float moveSpeed = 1;
        public float turnSped = 1;
        public int score;
        #endregion

        #region Monobehaviour
        public virtual void OnEnable()
        {
            list.Add(this);
        }
        public virtual void OnDisable()
        {
            list.Remove(this);
        }
        public override void Awake()
        {
            base.Awake();
            name = (string)view.instantiationData[0];
            input = new PlayerKeyboardInput();
            input.enabled = true;
        }
        public void Start()
        {
            if (!view.isMine) return;
            pawn = Pawn.Create(Space.Self, Vector3.zero, Quaternion.identity);
            pawn.viewport.enabled = true;
            pawn.SetAvatar(tank);
        }
        public virtual void Update()
        {
            if (!view.isMine) return;
            if (pawn == null) return;
            //Debug.Log("Moving pawn:" + input.move);
            pawn.Move(input.move * moveSpeed);
            pawn.Rotate(input.look.x * turnSped);
            if (input.attackPrimary) pawn.Attack(this);
        }
        public void OnGUI()
        {
            if (pawn != null) return;
            if (!GUI.Button(new Rect(0, 0, 100, 20), "Spawn Pawn")) return;
        }
        #endregion

        #region Spawn
        public static Player Create()
        {
            return Create("");
        }
        public static Player Create(string name)
        {
            if (name.Length == 0) name = RandomName();
            var data = new System.Object[] { name };
            return PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0, data).GetComponent<Player>();
        }
        #endregion

        #region Names
        private static string RandomName()
        {
            string[] names = new string[]{"Robin","Rickey","Horace","Charlene","Derek","Denise","Paul","Eileen","Carlos","Eula"};
            return names[Random.Range(0, names.Length-1)];
        }
        #endregion

        protected override void OnBroadcast(PhotonStream stream, PhotonMessageInfo messageInfo)
        {
            base.OnBroadcast(stream, messageInfo);
            stream.SendNext(score);
        }
        protected override void OnReceive(PhotonStream stream, PhotonMessageInfo messageInfo)
        {
            base.OnReceive(stream, messageInfo);
            score = (int)stream.ReceiveNext();
        }
    }
}
