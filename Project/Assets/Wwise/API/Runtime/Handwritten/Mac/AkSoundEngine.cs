using System;
using UnityEngine;

public partial class AkSoundEngine
{
#if UNITY_EDITOR_OSX || (UNITY_STANDALONE_OSX && !UNITY_EDITOR)
	/// <summary>
	///     Converts "AkOSChar*" C-strings to C# strings.
	/// </summary>
	/// <param name="ptr">"AkOSChar*" memory pointer passed to C# as an IntPtr.</param>
	/// <returns>Converted string.</returns>
	public static string StringFromIntPtrOSString(System.IntPtr ptr)
	{
		return StringFromIntPtrString(ptr);
	}
#endif
    public static void LoadBank(string v, out object bankID)
    {
        throw new NotImplementedException();
    }

    public static void PostEvent(string v, GameObject gameObject)
    {
        throw new NotImplementedException();
    }
}
