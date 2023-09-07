using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CmdController : MonoBehaviour
{

    internal abstract class Command
    {
        protected readonly Receiver Receiver;
        
        protected Command(Receiver receiver)
        {
            Receiver = receiver;
        }
        public abstract void Execute();
        public abstract void Undo();
    }

    internal class ConcreteCommand : Command 
    {
        public ConcreteCommand(Receiver receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.Action(this);
        }

        public override void Undo()
        {
            Receiver.Action(this);
        }
    }

    internal class Receiver
    {
        private Dictionary<int, Command> _actions = new Dictionary<int, Command>();

        public void Action(Command command)
        {
            var i = _actions.Count + 1;
            _actions.Add(i, command);
            Debug.Log("called Receiver.Action");
        }

        public Dictionary<int, Command> SendDictionary()
        {
            return _actions;
        }
    }

    internal class Invoker
    {
        private Command _command;

        public void SetCommand(Command command)
        {
            _command = command;
        }

        public void ExecuteCommand()
        {
            _command.Execute();
        }

        public void UndoCommand()
        {
            _command.Undo();
        }
    }

    internal class Client
    {
        public Receiver Receiver { get; set; }
        public Command Command { get; set; }
        public Invoker Invoker { get; set; }

        public void Launch(Command command)
        {
            Invoker.SetCommand(command);
            Invoker.ExecuteCommand();
        }

        public void Undo(Command command)
        {
            Invoker.SetCommand(command);
            Invoker.UndoCommand();
        }
    }
}