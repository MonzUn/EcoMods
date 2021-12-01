namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.EM.Artistry;
    using Eco.EM.Framework.Resolvers;

    [Serialized]
    [LocDisplayName("Powered Cart Pride")]
    [Weight(15000)]  
    [AirPollution(0.1f)] 
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]  
    [Tag("ColoredPoweredCart")]
    public partial class PoweredCartPrideItem : WorldObjectItem<PoweredCartPrideObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Large pride cart for hauling sizable loads.");
    }

    public class PaintPoweredCartPrideRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintPoweredCartPrideRecipe).Name,
            Assembly = typeof(PaintPoweredCartPrideRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Powered Cart Pride",
            LocalizableName = Localizer.DoStr("Paint Powered Cart Pride"),
            IngredientList = new()
            {
                new EMIngredient("PoweredCartItem", true, 1, true),
                new EMIngredient("RedPaintItem", true, 1, true),
                new EMIngredient("OrangePaintItem", true, 1, true),
                new EMIngredient("BluePaintItem", true, 1, true),
                new EMIngredient("GreenPaintItem", true, 1, true),
                new EMIngredient("PurplePaintItem", true, 1, true),
                new EMIngredient("YellowPaintItem", true, 1, true),
                new EMIngredient("PaintBrushItem", false, 1, true),
                new EMIngredient("PaintPaletteItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("PoweredCartPrideItem"),
                new EMCraftable("PaintBrushItem"),
                new EMCraftable("PaintPaletteItem"),
            },
            BaseExperienceOnCraft = 0.1f,
            BaseLabor = 250,
            LaborIsStatic = false,
            BaseCraftTime = 5,
            CraftTimeIsStatic = false,
            CraftingStation = "PrimitivePaintingTableItem",
            RequiredSkillType = typeof(BasicEngineeringSkill),
            RequiredSkillLevel = 0,
        };

        static PaintPoweredCartPrideRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintPoweredCartPrideRecipe()
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
    [RequireComponent(typeof(TailingsReportComponent))]     
    public partial class PoweredCartPrideObject : PhysicsWorldObject, IRepresentsItem
    {
        static PoweredCartPrideObject()
        {
            WorldObject.AddOccupancy<PoweredCartPrideObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Powered Cart Pride"); } }
        public Type RepresentedItemType { get { return typeof(PoweredCartPrideItem); } }

        private static string[] fuelTagList = new string[]
        {
            "Burnable Fuel",
        };

        private PoweredCartPrideObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(18, 3500000);           
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTagList);           
            this.GetComponent<FuelConsumptionComponent>().Initialize(35);    
            this.GetComponent<AirPollutionComponent>().Initialize(0.1f);            
            this.GetComponent<VehicleComponent>().Initialize(12, 1.5f, 1);
        }
    }
}
