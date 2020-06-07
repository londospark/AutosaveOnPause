using ICities;
using UnityEngine;

namespace AutosaveOnPause
{
    public class AutosaveOnPause: IUserMod {
		public string Name => "Autosave on Pause";
		public string Description => "Save your game everytime that you pause it.";
	}
}
