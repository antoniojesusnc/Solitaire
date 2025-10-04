using System.Collections.Generic;
using deVoid.Utils;
using Solitaire.Utils;
using UnityEngine;

namespace Solitaire
{
    public class UndoService : SingletonUnity<UndoService>
    {
        [SerializeField]
        private UndoServiceConfig _undoConfig;
        
        private List<ICommand> _lastCommands = new ();
        public bool IsUndoAvailable => _lastCommands.Count > 0;

        public void AddCommand(MoveCardCommand newCommand)
        {
            _lastCommands.Add(newCommand);
            CapUndo();
            EventBusService.Instance.GetEventMessage<OnAddUndoCommandEvent>().Dispatch();
        }

        private void CapUndo()
        {
            if (_lastCommands.Count > _undoConfig.MaxUndos)
            {
                _lastCommands.RemoveAt(0);
            }
        }

        public void Undo()
        { 
            _lastCommands[^1].Undo();
            _lastCommands.RemoveAt(_lastCommands.Count - 1);
            EventBusService.Instance.GetEventMessage<OnMakeUndoCommandEvent>().Dispatch();
        }
    }
}
