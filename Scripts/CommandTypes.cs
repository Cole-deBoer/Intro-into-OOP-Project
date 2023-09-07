using System;
using UnityEngine;



public class CommandTypes : MonoBehaviour
{
    
    internal class Button : CmdController.Command
    {
        private int _alliesSpawned;
        
        private void Active()
        {
            SpawnAllys();
        }
        

        private void SpawnAllys()
        {
            

            var killCountRequired = 1 + _alliesSpawned;
            Mathf.RoundToInt(killCountRequired);
            
            print(killCountRequired);
            
            var allySpawnAmount = GameController.enemiesDefeated.Count / (killCountRequired * 8);
            Mathf.RoundToInt(allySpawnAmount);
            
            print(allySpawnAmount);

            for (var i = 0; i < allySpawnAmount; i++)
            {
                _alliesSpawned++;
                Instantiate(Resources.Load("Knight"));
            }
        }

        public Button(CmdController.Receiver button) : base(button)
        {
        }

        public override void Execute()
        {
            Active();
            Receiver.Action(this);
        }

        public override void Undo(){}
    }
    
    internal class BodySwap : CmdController.Command
    {
        private Vector3 _storedPos;
        private GameObject _controlledBody;
        private bool _inBody = true;

        public BodySwap(CmdController.Receiver receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            SetNewBody();
            Receiver.Action(this);
        }

        public override void Undo()
        {
            ReturnToBody();
            Receiver.Action(this);
        }

        private void ReturnToBody()
        {
            if (_inBody) return;
            if (_controlledBody.activeInHierarchy) return;
            _controlledBody.transform.position = PlayerAttributes.PlayerPos.position;
            PlayerAttributes.PlayerPos.position = _storedPos;
            //this would give the player their stored attributes back, but that is only necessary once other features are implemented to change the player attributes
            _controlledBody.SetActive(true);
            _inBody = true;
        }
        
        private void SetNewBody()
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);

            if (hit.collider.CompareTag("Ally"))
            {
                _storedPos = PlayerAttributes.PlayerPos.position;
                PlayerAttributes.PlayerPos.position = hit.collider.gameObject.transform.position;
                //this is going to give the player all the attributes of the hit Ally, but that will come with states in the next task
                _controlledBody = hit.collider.gameObject;
                _controlledBody.SetActive(false);
                _inBody = false;
            }
        }
    }
}