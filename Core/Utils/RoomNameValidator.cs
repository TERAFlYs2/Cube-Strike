using System.Text.RegularExpressions;

public class SessionNameValidator
{
	public const byte MinimumLengthName = 3;
	public const byte MaximumLengthName = 16;
	
	public bool IsValidSessionName(string roomName) 
	{
		bool validLength = roomName.Length >= MinimumLengthName && roomName.Length <= MaximumLengthName;
		
		return !Regex.IsMatch(roomName, @"^\d|[^a-zA-Z0-9_-]") && !string.IsNullOrEmpty(roomName) && validLength;
	}
}
