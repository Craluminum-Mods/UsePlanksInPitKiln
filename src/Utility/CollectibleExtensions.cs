using Newtonsoft.Json.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;

namespace UsePlanksInPitKiln;

public static class CollectibleExtensions
{
    public static void ChangeAttribute(this CollectibleObject obj, object val, params string[] path)
    {
        obj.Attributes ??= new JsonObject(new JObject());

        switch (path.Length)
        {
            case 1:
                obj.Attributes.Token[path[0]] = JToken.FromObject(val);
                break;
            case 2:
                obj.Attributes.Token[path[0]][path[1]] = JToken.FromObject(val);
                break;
        }
    }
}