using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

namespace UsePlanksInPitKiln;

public static class Patches
{
    public static void PatchBuildMats(this Block blockPitKiln, ICoreAPI api)
    {
        List<JsonItemStackBuildStage> planks = new();

        foreach (Item item in api.World.Items.Where(x => x.WildCardMatch("plank-*") && !x.IsMissing))
        {
            item.Attributes ??= new JsonObject(new JObject());
            item.Attributes.Token["placeSound"] = JToken.FromObject("block/planks");

            planks.Add(new JsonItemStackBuildStage()
            {
                Type = item.ItemClass,
                Code = item.Code,
                Quantity = 4,
                EleCode = "Plank"
            });
        }

        JsonItemStackBuildStage[] sticks = blockPitKiln.Attributes["buildMats"]["sticks"].AsObject<JsonItemStackBuildStage[]>();
        sticks[0].EleCode = "Stick";
        sticks = sticks.Concat(planks).ToArray();

        blockPitKiln.Attributes ??= new JsonObject(new JObject());
        blockPitKiln.Attributes.Token["buildMats"]["sticks"] = JToken.FromObject(sticks);
    }

    public static void PatchModelConfigs(this Block blockPitKiln)
    {
        Dictionary<string, PitKilnModelConfig> modelConfigs = blockPitKiln.Attributes["modelConfigs"].AsObject<Dictionary<string, PitKilnModelConfig>>();

        foreach (PitKilnModelConfig val in modelConfigs.Values)
        {
            val.BuildStages[5] = "{eleCode}layer1/*";
            val.BuildStages[6] = "{eleCode}layer2/*";
        }

        blockPitKiln.Attributes ??= new JsonObject(new JObject());
        blockPitKiln.Attributes.Token["modelConfigs"] = JToken.FromObject(modelConfigs);
    }
}