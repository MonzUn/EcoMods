namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.EM.Artistry;
    using Eco.EM.Framework.Resolvers;

    [Serialized]
    [LocDisplayName("Truck White")]
    [Weight(25000)]  
    [AirPollution(0.5f)] 
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]                  
    [Tag("ColoredTruck")]
    public partial class TruckWhiteItem : WorldObjectItem<TruckWhiteObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Modern white truck for hauling sizable loads.");
    }
    
      
    public class PaintTruckWhiteRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintTruckWhiteRecipe).Name,
            Assembly = typeof(PaintTruckWhiteRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Truck White",
            LocalizableName = Localizer.DoStr("Paint Truck White"),
            IngredientList = new()
            {
                new EMIngredient("TruckItem", false, 1, true),
                new EMIngredient("WhitePaintItem", false, 2, true),
                new EMIngredient("PaintBrushItem", false, 1, true),
                new EMIngredient("PaintPaletteItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("TruckWhiteItem"),
                new EMCraftable("PaintBrushItem"),
                new EMCraftable("PaintPaletteItem"),
            },
            BaseExperienceOnCraft = 0.1f,
            BaseLabor = 750,
            LaborIsStatic = false,
            BaseCraftTime = 15,
            CraftTimeIsStatic = false,
            CraftingStation = "AdvancedPaintingTableItem",
            RequiredSkillType = typeof(IndustrySkill),
            RequiredSkillLevel = 0,
        };

        static PaintTruckWhiteRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintTruckWhiteRecipe()
        {
            this.Recipes = EMRecipeResolver.Obj.ResolveRecipe(this);
            this.LaborInCalories = EMRecipeResolver.Obj.ResolveLabor(this);
            this.CraftMinutes = EMRecipeResolver.Obj.ResolveCraftMinutes(this);
            this.ExperienceOnCraft = EMRecipeResolver.Obj.ResolveExperience(this);
            this.Initialize(Defaults.LocalizableName, GetType());
            CraftingComponent.AddRecipe(EMRecipeResolver.Obj.ResolveStation(this), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]              
    [RequireComponent(typeof(FuelConsumptionComponent))]         
    [RequireComponent(typeof(PublicStorageComponent))]      
    [RequireComponent(typeof(MovableLinkComponent))]        
    [RequireComponent(typeof(AirPollutionComponent))]       
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]   
    [RequireComponent(typeof(TailingsReportComponent))]     
    public partial class TruckWhiteObject : PhysicsWorldObject, IRepresentsItem, IStorageSlotObject
    {
        public override LocString DisplayName => Localizer.DoStr("Truck White");
        public Type RepresentedItemType => typeof(TruckWhiteItem);

        private static readonly StorageSlotModel SlotDefaults = new(typeof(TruckWhiteObject)) { StorageSlots = 36, };

        static TruckWhiteObject()
        {
            WorldObject.AddOccupancy<TruckWhiteObject>(new List<BlockOccupancy>(0));
            EMStorageSlotResolver.AddDefaults(SlotDefaults);
        }

        private static readonly string[] fuelTagList = new string[]
        {
            "Liquid Fuel"
        };

        private TruckWhiteObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<PublicStorageComponent>().Initialize(EMStorageSlotResolver.Obj.ResolveSlots(this), 8000000);
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTagList);
            this.GetComponent<FuelConsumptionComponent>().Initialize(25);
            this.GetComponent<AirPollutionComponent>().Initialize(0.5f);
            this.GetComponent<VehicleComponent>().Initialize(20, 2, 2);
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2, 2, 3));
        }
    }
}
