namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.EM.Artistry;

    [Serialized]
    [LocDisplayName("Small Wood Cart Pride")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]    
    [Tag("ColoredSmallWoodCart")]
    public partial class SmallWoodCartPrideItem : WorldObjectItem<SmallWoodCartPrideObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("Small pride cart for hauling small loads."); } }
    }

    public class PaintSmallWoodCartPrideRecipe : RecipeFamily
    {
        public PaintSmallWoodCartPrideRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "Paint Small Wood Cart Pride",
                    Localizer.DoStr("Paint Small  Cart Pride"),
                    new IngredientElement[]
                    {
                        new IngredientElement(typeof(SmallWoodCartItem), 1, true),
                        new IngredientElement(typeof(RedPaintItem), 2, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
                        new IngredientElement(typeof(OrangePaintItem), 2, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
                        new IngredientElement(typeof(YellowPaintItem), 2, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
                        new IngredientElement(typeof(GreenPaintItem), 2, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
                        new IngredientElement(typeof(BlueDyeItem), 2, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
                        new IngredientElement(typeof(PurplePaintItem), 2, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
                    },
                    new CraftingElement<SmallWoodCartPrideItem>()
                )
            };
            this.ExperienceOnCraft = 0.05f;
            this.LaborInCalories = CreateLaborInCaloriesValue(125, typeof(BasicEngineeringSkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(PaintSmallWoodCartPrideRecipe), 2.5f, typeof(BasicEngineeringSkill));

            this.Initialize(Localizer.DoStr("Paint Small Wood Cart Pride"), typeof(PaintSmallWoodCartPrideRecipe));
            CraftingComponent.AddRecipe(typeof(PrimitivePaintingTableObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(MovableLinkComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]
    public partial class SmallWoodCartPrideObject : PhysicsWorldObject, IRepresentsItem
    {
        static SmallWoodCartPrideObject()
        {
            WorldObject.AddOccupancy<SmallWoodCartPrideObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Small Wood Cart Pride"); } }
        public Type RepresentedItemType { get { return typeof(SmallWoodCartPrideItem); } }


        private SmallWoodCartPrideObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<PublicStorageComponent>().Initialize(8, 1400000);           
            this.GetComponent<VehicleComponent>().Initialize(10, 1, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.5f);           
        }
    }
}
