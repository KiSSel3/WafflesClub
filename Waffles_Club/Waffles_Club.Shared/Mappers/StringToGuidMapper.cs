namespace Waffles_Club.Shared.Mappers;

public class StringToGuidMapper
{
    public Guid MapTo(string id)
    {
        if (!Guid.TryParse(id,out Guid guidId))
        {
            throw new Exception( "Invalid id");
        }
        return guidId;
    }
}