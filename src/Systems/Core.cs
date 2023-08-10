using Vintagestory.API.Common;

[assembly: ModInfo(name: "Use Planks In Pit Kiln", modID: "useplanksinpitkiln")]

namespace UsePlanksInPitKiln;

public class Core : ModSystem
{
    public override void Start(ICoreAPI api)
    {
        base.Start(api);
        api.World.Logger.Event("started '{0}' mod", Mod.Info.Name);
    }

    public override void AssetsFinalize(ICoreAPI api)
    {
        Block blockPitKiln = api.World.GetBlock(new AssetLocation("pitkiln"));

        blockPitKiln.PatchBuildMats(api);
        blockPitKiln.PatchModelConfigs();
    }
}