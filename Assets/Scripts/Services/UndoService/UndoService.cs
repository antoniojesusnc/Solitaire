using System.Collections.Generic;
using Solitaire.Utils;
using UnityEngine;

namespace Solitaire
{
    public class UndoService : SingletonUnity<UndoService>
    {
        [SerializeField]
        private UndoServiceConfig _undoConfig;
        
        private List<ICommand> _lastCommands = new ();

        public void AddCommand(MoveCardCommand newCommand)
        {
            _lastCommands.Add(newCommand);
            CapUndo();
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
        }
    }
}
