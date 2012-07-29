using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces;
using System.ComponentModel.Composition;
using Phat.Editor.Modules.Editors.Properties;

namespace Phat.Editor.Modules.Editors
{
    public static class EditorMenuItems
    {
        public const String Package_New = "Package.New";
        public const String Package_New_Archetype = "Package.New.Archetype";
        public const String Package_New_FrameSet = "Package.New.FrameSet";
        public const String Package_New_Level = "Package.New.Level";
        public const String Package_New_TerrainTileDefinition = "Package.New.TerrainTileDefition";
        public const String Package_New_Texture2D = "Package.New.Texture2D";
        public const String Package_New_Sprite = "Package.New.Sprite";
        public const String Package_New_Storyboard = "Package.New.Storyboard";
        public const String Package_New_World = "Package.New.World";

        public const String Archetype_Open = "Archetype.Open";

        public const String FrameSet_Open = "FrameSet.Open";

        public const String Level_Open = "Level.Open";
        
        public const String Sprite_Open = "Sprite.Open";

        public const String Storyboard_Open = "Storyboard.Open";
                
        public const String TerrainTileDefinition_Open = "TerrainTileDefinition.Open";

        public const String Texture2D_Open = "Texture2D.Open";

        public const String World_Open = "World.Open";
    }

    /// <summary>
    /// Represents the new menu heading in the package context menu.
    /// </summary>
    [ExportPackageConextMenuItemAttribute(EditorMenuItems.Package_New, MenuPosition.AppendTo, MenuItems.Root)]
    public class PackageNewMenuItem : MenuItem
    {
        [ImportingConstructor]
        public PackageNewMenuItem() : base(Resources.PackageMenu_New) { }
    }
}
